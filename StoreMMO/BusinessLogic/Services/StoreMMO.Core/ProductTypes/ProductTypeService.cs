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

        public ProductViewModels AddProduct(ProductViewModels productViewModels)
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

        public ProductViewModels getByIDProduct(string id)
        {
           return _productRepository.getByIDProduct(id);
        }

        public ProductViewModels Update(ProductViewModels productViewModels)
        {
           return _productRepository.Update(productViewModels);
        }
    }
}
