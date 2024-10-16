using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductViewModels = StoreMMO.Core.ViewModels.ProductViewModels;

namespace StoreMMO.Core.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public ProductViewModels AddProduct(ProductViewModels inforAddViewModels)
        {
            var ViewModel = new Models.Product
            {
                Id = inforAddViewModels.Id,
                ProductTypeId = inforAddViewModels.ProductTypeId,
                Account = inforAddViewModels.Account,
                Pwd = inforAddViewModels.Pwd,
                Status = inforAddViewModels.Status,
                CreatedDate = DateTime.Now,
            };
            _context.Products.Add(ViewModel);
            _context.SaveChanges();
            return inforAddViewModels;
        }

        public void DeleteProduct(string id)
        {
            var p = _context.Products.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                throw new Exception("Not found ID");
            }
            _context.Products.Remove(p);
            _context.SaveChanges();
        }

        public IEnumerable<Product> getAllProduct()
        {
            var list = _context.Products.ToList();
            return list;
        }

        public ProductViewModels getByIdProduct(string id)
        {
            var findId = _context.Products.SingleOrDefault(x => x.Id == id);
            if (findId == null)
            {
                throw new Exception("Not found ID");
            }
            // Mapping to ProductViewModels
            var viewModel = new ProductViewModels
            {
                Id = findId.Id,
                ProductTypeId = findId.ProductTypeId,
                Account = findId.Account,
                Pwd = findId.Pwd,
                StatusUpload = findId.StatusUpload,
                Status = findId.Status,
                CreatedDate = findId.CreatedDate,
            };

            return viewModel;
        }

        public ProductViewModels UpdateProduct(ProductViewModels inforAddViewModels)
        {
            var viewModel = new Models.Product
            {
                Id = inforAddViewModels.Id,
                ProductTypeId = inforAddViewModels.ProductTypeId,
                Account = inforAddViewModels.Account,
                Pwd = inforAddViewModels.Pwd,
                StatusUpload = inforAddViewModels.StatusUpload,
                Status = inforAddViewModels.Status,
                CreatedDate = inforAddViewModels.CreatedDate,
            };
            _context.Products.Update(viewModel);
            _context.SaveChanges();
            return inforAddViewModels;
        }
    }
}
