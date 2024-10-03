using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Carts;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public CartViewModels Add(CartViewModels cart)
        {
            return _cartRepository.Add(cart);
        }

        public void DeleteCart(string id)
        {
            _cartRepository.Delete(id);
        }

        public IEnumerable<Cart> getAllCart()
        {
            return _cartRepository.getAll();
        }

        public CartViewModels getByIdCart(string id)
        {
            return _cartRepository.getById(id);
        }

        public List<CartItem> GetCartFromSession()
        {
           return this._cartRepository.GetCartFromSession();
        }

        public IEnumerable<CartItem> getProductAddByID(string proid)
        {
            return this._cartRepository.getProductAddByID(proid);
        }

        public void SaveCartToSession(List<CartItem> cart)
        {
             this._cartRepository.SaveCartToSession(cart);
        }

        public int sum(int a, int b)
        {
           return this._cartRepository.sum(a, b);
        }

        public CartViewModels UpdateCart(CartViewModels cart)
        {
            return _cartRepository.Update(cart);
        }

    }
}
