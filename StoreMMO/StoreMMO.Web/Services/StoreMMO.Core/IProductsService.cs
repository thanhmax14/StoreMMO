using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Services.StoreMMO.Core
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAllProduct();
        ProductViewModels getByIDProduct(string id);
        ProductViewModels AddProduct(ProductViewModels productViewModels);
        ProductViewModels Update(ProductViewModels productViewModels);
        void DeleteProduct(string id);
    }
}
