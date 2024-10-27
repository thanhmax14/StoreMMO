using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.Repositories.ProductsTypes;
using StoreMMO.Core.Repositories.StoreDetails;
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
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
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
        public IEnumerable<Product> GetHidden(string productId)
        {
            // Lấy tất cả sản phẩm và loại sản phẩm
            var products = _productRepository.getAllProduct();
            var productTypes = _productTypeRepository.GetAllProduct(); // Giả sử bạn có phương thức này

            // Lọc sản phẩm với điều kiện ProductType.IsActive == false và Product.Id == productId
            List<Product> result = new List<Product>();

            foreach (var product in products)
            {
                foreach (var productType in productTypes)
                {
                    if (product.ProductTypeId == productType.Id && productType.IsActive == false && product.Id == productId)
                    {
                        result.Add(product);
                    }
                }
            }

            return result;
        }

        public IEnumerable<ManageStoreViewModels> ManageStore()
        {
           return _productRepository.ManageStore();
        }

        public IEnumerable<ViewProductModels> GetProductsByStoreId(string storeId)
        {
           return _productRepository.GetProductsByStoreId(storeId); 
        }

        public IEnumerable<Product> getProductsByTypeID(string id)
        {
           return _productRepository.getProductsByTypeID(id);
        }

        public IEnumerable<Product> getProductsByTypeID1(string id)
        {
          
            return _productRepository.getProductsByTypeID1(id);
        }
    }
}
