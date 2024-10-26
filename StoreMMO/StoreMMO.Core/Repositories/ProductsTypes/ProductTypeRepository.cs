using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.ProductsTypes
{
    public class ProductTypeRepository: IProductTypeRepository
    {
        private readonly AppDbContext _context;
        public ProductTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public ProductTypesViewModels AddProduct(ProductTypesViewModels productViewModels)
        {
            var viewModel = new Models.ProductType
            {
                Id = productViewModels.Id,
                Name = productViewModels.Name,
                Stock = productViewModels.Stock,
                Price = productViewModels.Price,
                CreatedDate = DateTime.Now,
                ModifiedDate = productViewModels.ModifiedDate,
                IsActive = productViewModels.IsActive,
            };
            _context.ProductTypes.Add(viewModel);
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

        public IEnumerable<ProductType> GetAllProduct()
        {
            var list = _context.ProductTypes.ToList();
            return list;
        }

        public ProductTypesViewModels getByIDProduct(string id)
        {
          var findId = _context.ProductTypes.SingleOrDefault(x => x.Id == id);
            if (findId == null) {
                return null;
            }
            var viewModel = new ProductTypesViewModels
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

        public IEnumerable<GetInfoByProductypeID> GetInfoByProductid(string id)
        {
            var sql = @"
        SELECT Stores.Id AS StoreID, ProductTypes.Id AS ProductTypeID
        FROM Stores 
        INNER JOIN StoreDetails ON Stores.Id = StoreDetails.StoreId
        INNER JOIN ProductConnects ON StoreDetails.Id = ProductConnects.StoreDetailId
        INNER JOIN ProductTypes ON ProductConnects.ProductTypeId = ProductTypes.Id
        WHERE ProductTypes.Id = @ProductTypeID";

            // Tạo SqlParameter cho tham số ProductTypeID
            var parameter = new SqlParameter("@ProductTypeID", id);

            // Sử dụng SqlParameter trong SqlQueryRaw
            var list = _context.Database.SqlQueryRaw<GetInfoByProductypeID>(sql, parameter).ToList();

            return list;
        }


        public ProductTypesViewModels Update(ProductTypesViewModels productViewModels)
        {
            var existingProduct = _context.ProductTypes.FirstOrDefault(p => p.Id == productViewModels.Id);
            if (existingProduct != null)
            {
                // Tách thực thể đã theo dõi
                _context.Entry(existingProduct).State = EntityState.Detached;
            }
            var viewModel = new Models.ProductType
            {
                Id = productViewModels.Id,
                Name = productViewModels.Name,
                Stock = productViewModels.Stock,
                Price = productViewModels.Price,
                CreatedDate = productViewModels.CreatedDate,
                ModifiedDate = productViewModels.ModifiedDate,
                IsActive = productViewModels.IsActive,
            };
            _context.ProductTypes.Update(viewModel);
            _context.SaveChanges();
            return productViewModels;
        }
    }
}
