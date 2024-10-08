using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Carts
{
    public interface ICartService
    {
        IEnumerable<Cart> getAllCart();
        CartViewModels getByIdCart(string id);
        CartViewModels Add(CartViewModels cart);
        CartViewModels UpdateCart(CartViewModels cart);
        void DeleteCart(string id);
        IEnumerable<CartItem> getProductAddByID(string proid);
        List<CartItem> GetCartFromSession();
        void SaveCartToSession(List<CartItem> cart);
        int sum(int a, int b);
    }
}
