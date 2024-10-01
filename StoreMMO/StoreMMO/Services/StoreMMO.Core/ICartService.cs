using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Services.StoreMMO.Core
{
    public interface ICartService
    {
        IEnumerable<Cart> getAllCart();
        CartViewModels getByIdCart(string id);
        CartViewModels Add(CartViewModels cart);
        CartViewModels UpdateCart(CartViewModels cart);
        void DeleteCart(string id);

      /*  CartItem getProductAddByID(string proid);*/
        List<CartItem> GetCartFromSession();
        void SaveCartToSession(List<CartItem> cart);

    }
}
