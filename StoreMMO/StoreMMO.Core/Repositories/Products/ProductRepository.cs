using Microsoft.AspNetCore.Http.Timeouts;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public ProductViewModels AddProduct(ProductViewModels productViewModels)
        {
            var viewModel = new Product
            {
                Id = productViewModels.Id,
                Name = productViewModels.Name,
                Stock = productViewModels.Stock,
                Price = productViewModels.Price,
                CreatedDate = DateTime.Now,
                ModifiedDate = productViewModels.ModifiedDate,
                IsActive = productViewModels.IsActive,
            };
            _context.Products.Add(viewModel);
            _context.SaveChanges();
            return productViewModels;

        }

        public void DeleteProduct(string id)
        {
           var findId = _context.Products.SingleOrDefault(p => p.Id == id);
            if(findId == null)
            {
                throw new Exception("Not found ID");
            }
            _context.Products.Remove(findId);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProduct()
        {
            var list = _context.Products.ToList();
            return list;
        }

        public ProductViewModels getByIDProduct(string id)
        {
          var findId = _context.Products.SingleOrDefault(x => x.Id == id);
            if (findId == null) {
                return null;
            }
            var viewModel = new ProductViewModels
            {
                Id = findId.Id,
                Name = findId.Name,
                Stock = findId.Stock,
                Price = findId.Price,
                CreatedDate = findId.CreatedDate,
                ModifiedDate = findId.CreatedDate,
                IsActive = findId.IsActive,
            };
            return viewModel;
        }

        public ProductViewModels Update(ProductViewModels productViewModels)
        {
            var viewModel = new Product
            {
                Id = productViewModels.Id,
                Name = productViewModels.Name,
                Stock = productViewModels.Stock,
                Price = productViewModels.Price,
                CreatedDate = productViewModels.CreatedDate,
                ModifiedDate = productViewModels.ModifiedDate,
                IsActive = productViewModels.IsActive,
            };
            _context.Products.Update(viewModel);
            _context.SaveChanges();
            return productViewModels;
        }
    }
}
