using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductViewModels = StoreMMO.Core.ViewModels.ProductViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Products
{

    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductViewModels AddProduct(ProductViewModels inforAddViewModels)
        {
            return _productRepository.AddProduct(inforAddViewModels);
        }

        public void DeleteProduct(string id)
        {
            _productRepository.DeleteProduct(id);
        }

        public IEnumerable<Product> getAllProduct()
        {
           return _productRepository.getAllProduct();
        }

        public ProductViewModels getByIdProduct(string id)
        {
            return _productRepository.getByIdProduct(id);
        }

        public ProductViewModels UpdateProduct(ProductViewModels inforAddViewModels)
        {
            return _productRepository.UpdateProduct(inforAddViewModels);
        }
    }
}
