using Microsoft.Data.SqlClient;
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
    SELECT 
        s.IsAccept, 
        s.CreatedDate,
        s.Id,
        sd.[Name] AS StoreName, 
        ca.[Name] AS CategoryName, 
        SUM(CAST(p.Stock AS int)) AS TotalStock,
        st.Commission, 
        
        -- Tạo khoảng giá từ giá thấp nhất đến cao nhất với điều kiện khi min = max thì min sẽ là 0
        CASE 
            WHEN MIN(p.Price) = MAX(p.Price) 
            THEN CONCAT('0 - ', MAX(p.Price)) 
            ELSE CONCAT(MIN(p.Price), ' - ', MAX(p.Price)) 
        END AS PriceRange
    FROM 
        Stores s
    JOIN 
        Users u ON s.UserId = u.Id
    JOIN 
        StoreDetails sd ON s.ID = sd.StoreID
    JOIN 
        ProductConnects pc ON sd.Id = pc.StoreDetailID
    JOIN 
        ProductTypes p ON pc.ProductTypeId = p.ID
    LEFT JOIN 
        FeedBacks f ON f.StoreDetailId = sd.Id
    JOIN 
        Categories ca ON ca.Id = sd.CategoryId
    JOIN 
        StoreTypes st ON st.Id = sd.StoreTypeId
    GROUP BY 
        sd.[Name], 
        st.Commission,
        s.IsAccept,
        s.CreatedDate,
        s.Id,
        ca.[Name];
    ";

            // Sử dụng FromSqlRaw để thực hiện câu lệnh SQL và map kết quả vào ManageStoreViewModels
            var list = this._context.Database.SqlQueryRaw<ManageStoreViewModels>(sql).ToList();
            return list;
        }
        public IEnumerable<ViewProductModels> GetProductsByStoreId(string storeId)
        {
            // Using string interpolation to include storeId directly in the query
            string sql = $@"
SELECT  
    s1.Id AS StoreDetailId, 
    s2.Id AS StoreId, 
    p2.Name AS ProductName, 
    p2.Price, 
    p2.Stock, 
    p2.Id as ProductTypeID,
    p2.IsActive
FROM     
    StoreDetails s1 
LEFT JOIN
    Stores s2 ON s1.StoreId = s2.Id 
LEFT JOIN
    ProductConnects p1 ON s1.Id = p1.StoreDetailId 
LEFT JOIN
    ProductTypes p2 ON p1.ProductTypeId = p2.Id
WHERE 
    s2.Id = '{storeId}'  -- Using string interpolation for storeId
    GROUP BY 
    s1.Id,
    s2.Id,
    p2.Name,
    p2.Stock,
    p2.IsActive,
    p2.Id,
    p2.Price;";

            // Use FromSqlRaw with no parameters
            var list = this._context.Database.SqlQueryRaw<ViewProductModels>(sql).ToList();
            return list;
        }
        public IEnumerable<Product> getProductsByTypeID(string id)
        {
            return _context.Products
                 .Where(p => p.ProductTypeId == id && !p.Status.ToLower().Equals("paid"))
                 .OrderByDescending(x => x.CreatedDate)
                 .ToList();
        }
        public IEnumerable<Product> getProductsByTypeID1(string id)
        {
            return _context.Products
                 .Where(p => p.ProductTypeId == id)
                 .OrderByDescending(x => x.CreatedDate)
                 .ToList();
        }
    }
}
