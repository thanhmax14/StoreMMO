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
    }
}
