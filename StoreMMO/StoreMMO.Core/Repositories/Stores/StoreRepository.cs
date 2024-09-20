using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Stores
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

        public StoreAddViewModels AddStore(StoreAddViewModels store)
        {
            var store1 = new Store
            {
                Id = store.Id,
                UserId = store.UserId,
                CreatedDate = DateTime.Now,
                ModifiedDate = store.ModifiedDate,
                IsAccept = false,
                Price = store.Price,
            };
            _context.Stores.Add(store1);
            _context.SaveChanges();
            return store;
        }

        public StoreAddViewModels Update(StoreAddViewModels store)
        {
            var store1 = new Store
            {
                Id = store.Id,
                UserId = store.UserId,
                CreatedDate = store.CreatedDate,
                ModifiedDate = DateTime.Now,
                IsAccept = store.IsAccept,
                Price = store.Price,
            };
            _context.Stores.Update(store1);
            _context.SaveChanges();
            return store;
        }

        public void Delete(string? id)
        {
            var p = _context.Stores.FirstOrDefault(x => x.Id == id);

            if (p == null)
            {
                throw new Exception("Store not found");
            }

            _context.Stores.Remove(p);
            _context.SaveChanges();
        }

        public StoreAddViewModels getById(string id)
        {

            var store = _context.Stores.FirstOrDefault(x => x.Id == id);

            if (store == null)
            {
                return null; // Hoặc xử lý ngoại lệ nếu bạn muốn
            }
            var viewModel = new StoreAddViewModels
            {
                Id = store.Id,
               
            };

            return viewModel;
        }
    }
}
