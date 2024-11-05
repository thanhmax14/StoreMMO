using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.Stores
{

    public class StoreRepository : IStoreRepository

    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public IEnumerable<StoreViewModels> getAll(string sicbo)
        {
            string sql = $"SELECT s.Id AS storeID, us.id AS userid, sd.[Name] AS nameStore, ca.[Name] AS catename, us.UserName, sd.Img AS imgStore FROM Users us INNER JOIN Stores s ON us.Id = s.UserId INNER JOIN StoreDetails sd ON s.Id = sd.StoreId INNER JOIN StoreTypes st ON sd.StoreTypeId = st.Id INNER JOIN Categories ca ON sd.CategoryId = ca.Id WHERE s.IsAccept = '{sicbo}'\r\n";


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
                IsAccept = "",
            };
            _context.Stores.Add(store1);
            _context.SaveChanges();
            return store;
        }

        public StoreAddViewModels Update(StoreAddViewModels store)
        {
            var s = this._context.Stores.Find(store.Id);

            s.Id = store.Id;
            s.UserId = store.UserId;
            s.CreatedDate = store.CreatedDate;
            s.ModifiedDate = DateTime.Now;
            s.IsAccept = store.IsAccept;

            //_context.Stores.Update(s);
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
                throw new Exception("Not found ID");
            }
            var viewModel = new StoreAddViewModels
            {
                Id = store.Id,
                UserId = store.UserId,
                CreatedDate = store.CreatedDate,
                ModifiedDate = DateTime.Now,
                IsAccept = store.IsAccept,
            };

            return viewModel;
        }
        public IEnumerable<getProducInStoreViewModels> getAllProductInStore(string id)
        {
            string sql = $"SELECT p.[Name] AS ProductName,p.Id AS ProductId FROM Stores s JOIN Users u ON s.UserId = u.Id JOIN StoreDetails sd ON s.ID = sd.StoreID JOIN ProductConnects pc ON sd.Id = pc.StoreDetailID JOIN ProductTypes p ON pc.ProductTypeId = p.ID LEFT JOIN FeedBacks f ON f.StoreDetailId = sd.Id AND f.UserId = u.Id JOIN Categories ca ON ca.Id = sd.CategoryId WHERE s.ID = '{id}'and p.IsActive='true';\r\n";
            var list = this._context.Database.SqlQueryRaw<getProducInStoreViewModels>(sql).ToList();
            return list;
        }
        public IEnumerable<StoreDetailViewModel> getStorDetailFullInfo(string id)
        {

            string sql = $"SELECT u.FullName AS OwnerUserName, sd.[Img] AS StoreImg, sd.[Name] AS StoreName, sd.SubDescription AS " +
                $"ShortDescription, sd.DescriptionDetail AS LongDescription, ca.[Name] AS CategoryName," +
                $"  COUNT(f.StoreDetailId) AS QuantityComment" +
                $" FROM Stores s JOIN Users u ON s.UserId = u.Id JOIN StoreDetails sd ON s.ID = sd.StoreID JOIN" +
                $" ProductConnects pc ON sd.Id = pc.StoreDetailID JOIN ProductTypes p ON pc.ProductTypeId = p.ID LEFT JOIN" +
                $" FeedBacks f ON f.StoreDetailId = sd.Id AND f.UserId = u.Id JOIN Categories ca" +
                $" ON ca.Id = sd.CategoryId WHERE s.ID = '{id}' GROUP BY u.FullName, sd.[Name],   sd.[Img]  ," +
                $" sd.SubDescription, sd.DescriptionDetail, ca.[Name], f.Comments, f.Relay;\r\n";
            var list = this._context.Database.SqlQueryRaw<StoreDetailViewModel>(sql).ToList();

            foreach (var item in list)
            {
                var temPro = getAllProductInStore(id);
                foreach (var itemPro in temPro)
                {
                    item.ProductStock.Add(itemPro.ProductName, itemPro.ProductId);
                }
            }
            return list;
        }
        public IEnumerable<StoreManageViewModels> getAllStore()
        {
            string sql = @"
SELECT 
    Users.UserName, 
    ProductTypes.Price, 
    Stores.IsAccept, 
    Stores.Id,
    StoreTypes.Name AS StoreTypeName, 
    Categories.Name AS CategoryName, 
    StoreDetails.Name AS StoreDetailName, 
    ProductTypes.Name AS ProductTypeName
FROM 
    Categories
INNER JOIN 
    StoreDetails ON Categories.Id = StoreDetails.CategoryId
INNER JOIN 
    Stores ON StoreDetails.StoreId = Stores.Id
INNER JOIN 
    StoreTypes ON StoreDetails.StoreTypeId = StoreTypes.Id
INNER JOIN 
    Users ON Stores.UserId = Users.Id
INNER JOIN 
    ProductConnects ON StoreDetails.Id = ProductConnects.StoreDetailId
INNER JOIN 
    ProductTypes ON ProductConnects.ProductTypeId = ProductTypes.Id
WHERE 
    Stores.IsAccept = 'PENDING'";
            var list = this._context.Database.SqlQueryRaw<StoreManageViewModels>(sql).ToList();
            return list;
        }

        public IEnumerable<StoreSellerViewModels> getAllStoreSeller(string currentUserId)
        {
            string sql = @"
    SELECT 
        sd.Id,
        s.Id AS StoreId,
        sd.Name,
        sd.SubDescription,
        s.ModifiedDate,
        sd.DescriptionDetail,
        sd.CreatedDate,
        s.IsAccept,
        sd.Img
    FROM 
        Stores s
    INNER JOIN 
        StoreDetails sd ON s.Id = sd.StoreId
    WHERE 
        s.UserId = @UserId"; // Giả sử Store có trường UserId để xác định người sở hữu.

            var list = this._context.Database.SqlQueryRaw<StoreSellerViewModels>(sql, new SqlParameter("@UserId", currentUserId)).ToList();
            return list;
        }


        //    public IEnumerable<StoreSellerViewModels> getAllStoreSeller()
        //    {
        //        string sql = @"
        //SELECT sd.Id, sd.Name, sd.SubDescription, sd.ModifiedDate, s.IsAccept
        //FROM StoreMMO.dbo.StoreDetails sd
        //JOIN StoreMMO.dbo.Stores s ON sd.StoreId = s.Id";
        //        var list = this._context.Database.SqlQueryRaw<StoreSellerViewModels>(sql).ToList();
        //        return list;
        //    }
        public StoreDetailViewModels UpdateStore(StoreDetailViewModels store)
        {
            var fine = this._context.StoreDetails.FirstOrDefault(x => x.Id == store.Id);
            fine.Name = store.Name;
            fine.SubDescription = store.SubDescription;
            fine.DescriptionDetail = store.DescriptionDetail;
            fine.ModifiedDate = DateTime.UtcNow;
            fine.Img = store.Img;
            this._context.SaveChanges();
            store.Id = fine.Id;
            store.Name = fine.Name;
            store.SubDescription = fine.SubDescription;
            store.DescriptionDetail = fine.DescriptionDetail;
            store.ModifiedDate = fine.ModifiedDate;
            store.Img = fine.Img;
            return store;

        }
        public StoreDetailViewModels getStoreDetailById(string id)
        {
            var findId = _context.StoreDetails.SingleOrDefault(x => x.Id == id);
            if (findId == null)
            {
                throw new Exception("Id not found");
            }
            var storedetail = new StoreDetailViewModels
            {
                Id = findId.Id,
                Name = findId.Name,
                SubDescription = findId.SubDescription,
                DescriptionDetail = findId.DescriptionDetail,
                Img = findId.Img,
                ModifiedDate = findId.ModifiedDate,
                CategoryId = findId.CategoryId,
                StoreTypeId = findId.StoreTypeId,
                StoreId = findId.StoreId,
            };

            return storedetail;
        }
    }
}
