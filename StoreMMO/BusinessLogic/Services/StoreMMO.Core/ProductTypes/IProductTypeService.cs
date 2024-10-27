using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.ProductTypes
{
    public interface IProductTypeService
    {
        IEnumerable<ProductType> GetAllProduct();
        ProductTypesViewModels getByIDProduct(string id);
        ProductTypesViewModels AddProduct(ProductTypesViewModels productViewModels);
        ProductTypesViewModels Update(ProductTypesViewModels productViewModels);
        void DeleteProduct(string id);
        IEnumerable<GetInfoByProductypeID> GetInfoByProductid(string id);
    }
}
