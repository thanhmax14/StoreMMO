using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.ProductTypes
{
    public interface IProductTypeService
    {
        IEnumerable<ProductType> GetAllProduct();
        ProductViewModels getByIDProduct(string id);
        ProductViewModels AddProduct(ProductViewModels productViewModels);
        ProductViewModels Update(ProductViewModels productViewModels);
        void DeleteProduct(string id);
    }
}
