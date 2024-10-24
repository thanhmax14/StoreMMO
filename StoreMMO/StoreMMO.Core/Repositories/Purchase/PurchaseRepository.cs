using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Purchase
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public PurchaseRepository(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            this._contextAccessor = contextAccessor;
        }

        public bool add(OrderBuyViewModels orderBuyViewModels)
        {
            if(orderBuyViewModels == null) return false;
            try
            {
                var tem = new OrderBuy {

                    ID = orderBuyViewModels.ID,
                    OrderCode = orderBuyViewModels.OrderCode,
                    ProductTypeId = orderBuyViewModels.ProductTypeId,
                    Status = orderBuyViewModels.Status,
                    StoreID = orderBuyViewModels.StoreID,
                    totalMoney = orderBuyViewModels.totalMoney,
                    UserID = orderBuyViewModels.UserID,
                };

                this._context.OrderBuys.Add(tem);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(OrderBuyViewModels orderBuyViewModels)
        {
            var find = this._context.OrderBuys.Find(orderBuyViewModels.ID);
              if(find == null)
            {
                return false;
            }
            return Save();
        }

        public bool Edit(OrderBuyViewModels orderBuyViewModels)
        {
            var find = _context.OrderBuys.Find(orderBuyViewModels.ID);
            if (find != null)
            {
                find.totalMoney = orderBuyViewModels.totalMoney;
                find.OrderCode = orderBuyViewModels.OrderCode;
                find.Status = orderBuyViewModels.Status;
                find.ProductTypeId = orderBuyViewModels.ProductTypeId;
                find.StoreID = orderBuyViewModels.StoreID;
                find.UserID = orderBuyViewModels.UserID;
                return Save();
            }
            return false;
        }
        public OrderBuyViewModels GetByID(string id)
        {
            var obj = this._context.OrderBuys.Find(id);
            var tem = new OrderBuyViewModels
            {
                ID = obj.ID,
                OrderCode = obj.OrderCode,
                ProductTypeId = obj.ProductTypeId,
                Status = obj.Status,
                StoreID = obj.StoreID,
                totalMoney = obj.totalMoney,
                UserID = obj.UserID,
            };
            return tem;
        }
      
        public List<PurchaseItem> GetProductFromSession()
        {

            var cart = this._contextAccessor.HttpContext.Session.GetString("PurchaseItem");
            return string.IsNullOrEmpty(cart) ? new List<PurchaseItem>() : JsonConvert.DeserializeObject<List<PurchaseItem>>(cart);

        }
        public void SaveProductToSession(List<PurchaseItem> cart)
        {
            if (cart == null)
            {
                _contextAccessor.HttpContext.Session.Remove("PurchaseItem");
            }
            else
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                _contextAccessor.HttpContext.Session.SetString("PurchaseItem", JsonConvert.SerializeObject(cart, jsonSettings));
            }
            
        }
        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }

		public IEnumerable<OrderBuyViewModels> GetAll()
		{
			var find = _context.OrderBuys.ToList();
			List<OrderBuyViewModels> temp = find.Select(b => new OrderBuyViewModels
			{
				UserID = b.UserID,
				ID = b.ID,
				OrderCode = b.OrderCode,
				ProductTypeId = b.ProductTypeId,
				Status = b.Status,
				StoreID = b.StoreID,
				totalMoney = b.totalMoney
			}).ToList();
			return temp;
		}

		public IEnumerable<OrderBuyViewModels> GetByUserID(string userID)
		{
			var find = _context.OrderBuys.Where(w => w.UserID == userID).ToList();
			List<OrderBuyViewModels> temp = find.Select(b => new OrderBuyViewModels
			{
				UserID = b.UserID,
				ID = b.ID,
				OrderCode = b.OrderCode,
				ProductTypeId = b.ProductTypeId,
				Status = b.Status,
				StoreID = b.StoreID,
				totalMoney = b.totalMoney
			}).ToList();
			return temp;
		}

		public IEnumerable<GetOrderByUserViewModel> GetAllByUserID(string userID)
		{
			string sql = @"SELECT 
                        OrderBuys.OrderCode,
                        OrderBuys.ID as OrderID, 
                        OrderDetails.Dates as OrderDate, 
                        StoreDetails.Name as StoreName, 
                        ProductTypes.Name as ProName,
                        [Users].UserName as Seller, 
                        COUNT(OrderDetails.ID) as Quantity, 
                        OrderBuys.totalMoney as TotalPrice, 
                        OrderBuys.Status 
                   FROM 
                        OrderBuys 
                   INNER JOIN 
                        OrderDetails ON OrderBuys.ID = OrderDetails.OrderBuyID 
                   INNER JOIN 
                        Products ON OrderDetails.ProductID = Products.Id 
                   INNER JOIN 
                        ProductTypes ON OrderBuys.ProductTypeId = ProductTypes.Id 
                        AND Products.ProductTypeId = ProductTypes.Id 
                   INNER JOIN 
                        Stores ON OrderBuys.StoreID = Stores.Id 
                   INNER JOIN 
                        StoreDetails ON Stores.Id = StoreDetails.StoreId 
                   INNER JOIN 
                        Users ON OrderBuys.UserID = Users.Id 
                        AND Stores.UserId = Users.Id 
                   WHERE 
                        OrderBuys.UserID = @userID 
                   GROUP BY 
                        OrderBuys.ID, 
                        OrderDetails.Dates, 
                        StoreDetails.Name, 
                        ProductTypes.Name, 
                        [Users].UserName, 
                        OrderBuys.totalMoney, 
                        OrderBuys.Status,
                        OrderBuys.OrderCode;";

			var result = _context.Database.SqlQueryRaw<GetOrderByUserViewModel>(sql, new SqlParameter("@userID", userID)).ToList();
			if (!result.Any())
			{
				Console.WriteLine("Không tìm thấy đơn hàng cho người dùng với ID: " + userID);
			}

			return result;
		}



		public IEnumerable<GetOrderDetailsViewModel> getOrderDetails(string orderID)
		{
			string sql = @"SELECT 
                        Products.Account, 
                        Products.Pwd AS Password, 
                        OrderDetails.Quantity, 
                        OrderDetails.Price, 
                        OrderDetails.Dates, 
                        OrderDetails.stasusPayment, 
                        OrderDetails.Status
                   FROM 
                        OrderDetails 
                   INNER JOIN 
                        Products ON OrderDetails.ProductID = Products.Id 
                   WHERE 
                        OrderDetails.OrderBuyID = @orderID;";

			var parameters = new[] { new SqlParameter("@orderID", orderID) };
			var result = _context.Database.SqlQueryRaw<GetOrderDetailsViewModel>(sql, parameters).ToList();

			return result;
		}

	}
}
