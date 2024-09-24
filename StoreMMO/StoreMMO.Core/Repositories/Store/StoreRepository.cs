using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Store
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext dbContext)
        {
            this._context = dbContext;
        }
        public IEnumerable<StoreViewModels> getAll()
        {
            string sql = "SELECT  s.Id as storeID, us.id as userid, sd.[Name] as nameStore," +
                " ca.[Name] as catename,\r\n us.UserName , sd.Img as imgStore\r\nFROM           " +
                "       Users us INNER JOIN\r\n           " +
                "           Stores s ON us.Id = s.UserId INNER JOIN\r\n     " +
                "                 StoreDetails sd  ON s.Id = sd.StoreId INNER JOIN\r\n    " +
                "                  StoreTypes st  ON sd.StoreTypeId = st.Id INNER JOIN\r\n    " +
                "                  Categories ca ON sd.CategoryId = ca.Id";


            var list = this._context.Database.SqlQueryRaw<StoreViewModels>(sql).ToList();
            return list;

        }
        public IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id)
        {
            string sql = $"SELECT p.[Name] AS ProductName, p.Stock AS ProductStock FROM Stores s JOIN Users u ON s.UserId = u.Id JOIN StoreDetails sd ON s.ID = sd.StoreID JOIN ProductConnects pc ON sd.Id = pc.StoreDetailID JOIN Products p ON pc.ProductID = p.ID LEFT JOIN FeedBacks f ON f.StoreDetailId = sd.Id AND f.UserId = u.Id JOIN Categories ca ON ca.Id = sd.CategoryId WHERE s.ID = '{id}';\r\n";
            var list = this._context.Database.SqlQueryRaw<getProducInStoreViewModels>(sql).ToList();
            return list;
        }
      public  IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id)
        {

            string sql = $"SELECT u.FullName AS OwnerUserName, sd.[Name] AS StoreName, sd.SubDescription AS " +
                $"ShortDescription, sd.DescriptionDetail AS LongDescription, ca.[Name] AS CategoryName," +
                $"  COUNT(f.StoreDetailId) AS QuantityComment" +
                $" FROM Stores s JOIN Users u ON s.UserId = u.Id JOIN StoreDetails sd ON s.ID = sd.StoreID JOIN" +
                $" ProductConnects pc ON sd.Id = pc.StoreDetailID JOIN Products p ON pc.ProductID = p.ID LEFT JOIN" +
                $" FeedBacks f ON f.StoreDetailId = sd.Id AND f.UserId = u.Id JOIN Categories ca" +
                $" ON ca.Id = sd.CategoryId WHERE s.ID = '{id}' GROUP BY u.FullName, sd.[Name]," +
                $" sd.SubDescription, sd.DescriptionDetail, ca.[Name], f.Comments, f.Relay;\r\n";
            var list = this._context.Database.SqlQueryRaw<StoreDetailViewModel>(sql).ToList();

            foreach(var item in list)
            {
                var temPro = getAllProductInStore(id);
                 foreach(var itemPro in temPro)
                {
                    item.ProductStock.Add(itemPro.ProductName, itemPro.ProductStock);
                }            
            }
            return list;
        }

    }
}

