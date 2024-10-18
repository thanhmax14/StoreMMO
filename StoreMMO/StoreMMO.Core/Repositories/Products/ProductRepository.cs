using Microsoft.EntityFrameworkCore;
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
              Id = Guid.NewGuid().ToString(),
                ProductTypeId = inforAddViewModels.ProductTypeId,
                Account = inforAddViewModels.Account,
                StatusUpload = "",
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
            var findId = _context.Products.FirstOrDefault(x => x.Id == id);
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
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == inforAddViewModels.Id);
            if (existingProduct != null)
            {
                // Tách thực thể đã theo dõi
                _context.Entry(existingProduct).State = EntityState.Detached;
            }
            var viewModel = new Models.Product
            {
                Id = inforAddViewModels.Id,
                ProductTypeId = inforAddViewModels.ProductTypeId,
                Account = inforAddViewModels.Account,
                Pwd = inforAddViewModels.Pwd,
                StatusUpload = "",
                Status = inforAddViewModels.Status,
                CreatedDate = inforAddViewModels.CreatedDate,
            };
            _context.Products.Update(viewModel);
            _context.SaveChanges();
            return inforAddViewModels;
        }
        public IEnumerable<ManageStoreViewModels> ManageStore()
        {
            string sql = @"
    SELECT s2.[Name] AS NameStore, 
           c1.[Name] AS CateName, 
           p2.Price, 
           s3.Commission, 
           p2.Stock, 
           s1.CreatedDate, 
           s1.IsAccept,
           p2.Id
    FROM Stores s1
    INNER JOIN StoreDetails s2 ON s1.Id = s2.StoreId
    INNER JOIN StoreTypes s3 ON s2.StoreTypeId = s3.Id
    INNER JOIN ProductConnects p1 ON s2.Id = p1.StoreDetailId
    INNER JOIN ProductTypes p2 ON p1.ProductTypeId = p2.Id
    INNER JOIN Categories c1 ON s2.CategoryId = c1.Id";

            var list = this._context.Database.SqlQueryRaw<ManageStoreViewModels>(sql).ToList();
            return list;
        }
    }
}
