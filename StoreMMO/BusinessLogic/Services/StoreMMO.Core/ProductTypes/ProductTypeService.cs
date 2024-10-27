using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.ProductsTypes;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.ProductTypes
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productRepository;

        public ProductTypeService(IProductTypeRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductTypesViewModels AddProduct(ProductTypesViewModels productViewModels)
        {
            return _productRepository.AddProduct(productViewModels);
        }

        public void DeleteProduct(string id)
        {
           _productRepository.DeleteProduct(id);
        }

        public IEnumerable<ProductType> GetAllProduct()
        {
            return _productRepository.GetAllProduct();
        }

        public ProductTypesViewModels getByIDProduct(string id)
        {
           return _productRepository.getByIDProduct(id);
        }

        public IEnumerable<GetInfoByProductypeID> GetInfoByProductid(string id)
        {
          return this._productRepository.GetInfoByProductid(id);
        }

        public ProductTypesViewModels Update(ProductTypesViewModels productViewModels)
        {
           return _productRepository.Update(productViewModels);
        }

        public async Task<bool> UpdateQuantity(int quantity, string ProductTyoeID)
        {
          return await this._productRepository.UpdateQuantity(quantity, ProductTyoeID);
        }
    }
}
