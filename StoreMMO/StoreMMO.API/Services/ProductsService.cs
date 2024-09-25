using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productRepository;

        public ProductsService(IProductRepository productRepository)
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

        public IEnumerable<Product> GetAllProduct()
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
