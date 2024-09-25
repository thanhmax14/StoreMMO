using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public interface ICartService
    {
        IEnumerable<Cart> getAllCart();
        CartViewModels getByIdCart(string id);
        CartViewModels Add(CartViewModels cart);
        CartViewModels UpdateCart(CartViewModels cart);
        void DeleteCart(string id);


    }
}
