using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Products
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
