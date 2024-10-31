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
			string sql = @"SELECT * FROM (
    SELECT 
        OrderBuys.OrderCode,
        OrderBuys.ID as OrderID, 
        MIN(OrderDetails.Dates) as OrderDate, 
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
        StoreDetails.Name, 
        ProductTypes.Name, 
        [Users].UserName, 
        OrderBuys.totalMoney, 
        OrderBuys.Status,
        OrderBuys.OrderCode
) AS Orders
ORDER BY OrderDate DESC;
";

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
                        OrderDetails.Status,
						OrderDetails.id  as DetailID
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

        public async Task<List<TransactionSummary>> GetDailyTransactionSummary()
        {
            string sqlQuery = @"SELECT 
    CONVERT(datetime, DATEADD(MINUTE, DATEDIFF(MINUTE, 0, od.Dates), 0), 120) AS TransactionDate,  -- Lấy ngày giờ với định dạng YYYY-MM-DD HH:MM:SS, loại bỏ phần mili giây
    COUNT(DISTINCT ob.ID) AS TotalTransactions,               -- Đếm số lượng giao dịch duy nhất
    SUM(CAST(od.Price AS decimal(18, 2)) * CAST(od.Quantity AS int)) AS TotalRevenue  -- Tính tổng doanh thu
FROM 
    OrderBuys ob
JOIN 
    OrderDetails od ON ob.ID = od.OrderBuyID
WHERE 
    ob.Status = 'PAID' AND                                   -- Chỉ thống kê các giao dịch đã thanh toán
    od.Dates >= CAST(GETDATE() AS DATE) AND                -- Lọc giao dịch trong ngày hôm nay
    od.Dates < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))    -- Đến 24 giờ tiếp theo
GROUP BY 
    CONVERT(datetime, DATEADD(MINUTE, DATEDIFF(MINUTE, 0, od.Dates), 0), 120)  -- Nhóm theo định dạng ngày giờ, loại bỏ mili giây
ORDER BY 
    TransactionDate;                                    -- Sắp xếp theo ngày giờ
";


            var result = await this._context.Database.SqlQueryRaw<TransactionSummary>(sqlQuery).ToListAsync();
            var b = result;
            return result;
        }

        public async Task<List<TransactionSummary>> GetMonth()
        {
            string sqlQuery = @"SELECT 
    CONVERT(DATE, od.Dates) AS TransactionDate,                     
    COUNT(DISTINCT ob.ID) AS TotalTransactions,                     
    SUM(CAST(od.Price AS decimal(18, 2)) * CAST(od.quantity AS int)) AS TotalRevenue  
FROM 
    OrderBuys ob
JOIN 
    OrderDetails od ON ob.ID = od.OrderBuyID
WHERE 
    ob.Status = 'PAID'  
    AND MONTH(od.Dates) = MONTH(GETDATE())       -- Kiểm tra tháng hiện tại
    AND YEAR(od.Dates) = YEAR(GETDATE())         -- Kiểm tra năm hiện tại
GROUP BY 
    CONVERT(DATE, od.Dates)                                         
ORDER BY 
    TransactionDate;
";


            var result = await this._context.Database.SqlQueryRaw<TransactionSummary>(sqlQuery).ToListAsync();
            var b = result;
            return result;
        }

        public async Task<List<TransactionSummary>> GetMonthInYear()
        {
            string sqlQuery = @"SELECT 
    CAST(DATEADD(MONTH, DATEDIFF(MONTH, 0, Dates), 0) AS DATETIME) AS TransactionDate,  -- Lấy ngày đầu tháng
    COUNT(*) AS TotalTransactions,                                                         -- Số lượng đơn hàng
    SUM(CAST(Price AS DECIMAL(18, 2))) AS TotalRevenue                                   -- Tổng doanh thu
FROM 
    OrderDetails
WHERE 
    YEAR(Dates) = YEAR(GETDATE())                                                          -- Lọc theo năm hiện tại
GROUP BY 
    DATEADD(MONTH, DATEDIFF(MONTH, 0, Dates), 0)                                          -- Nhóm theo tháng
ORDER BY 
    TransactionDate;                 ";


            var result = await this._context.Database.SqlQueryRaw<TransactionSummary>(sqlQuery).ToListAsync();
            var b = result;
            return result;
        }

        public async Task<List<TransactionSummary>> GetAllYear()
        {
            string sqlQuery = @"SELECT 
    CAST(DATEFROMPARTS(YEAR(Dates), 1, 1) AS DATETIME) AS TransactionDate, -- Lấy ngày đầu năm dưới dạng DATETIME
    COUNT(*) AS TotalTransactions,                                         -- Số lượng đơn hàng trong năm
    SUM(CAST(Price AS DECIMAL(18, 2))) AS TotalRevenue                     -- Tổng doanh thu trong năm
FROM 
    OrderDetails
GROUP BY 
    DATEFROMPARTS(YEAR(Dates), 1, 1)                                       -- Nhóm theo ngày đầu năm
ORDER BY 
    TransactionDate;                                                       -- Sắp xếp theo TransactionDate
";


            var result = await this._context.Database.SqlQueryRaw<TransactionSummary>(sqlQuery).ToListAsync();
            var b = result;
            return result;
        }

        public async Task<List<TopStoreViewModels>> TopStore()
        {
            string sqlQuery = @"SELECT 
    S.Id AS StoreID,
    SD.Name,
	SD.Img,
    COUNT(OD.ProductID) AS TotalProductsSold,
    SUM(CAST(OD.Price AS DECIMAL(18, 2)) * CAST(OD.quantity AS DECIMAL(18, 2))) AS TotalRevenue
FROM 
    Stores S
JOIN 
    StoreDetails SD ON S.Id = SD.StoreId
JOIN 
    OrderBuys OB ON S.Id = OB.StoreID
JOIN 
    OrderDetails OD ON OB.ID = OD.OrderBuyID
GROUP BY 
    S.Id, S.UserId, SD.Name,SD.Img
ORDER BY 
    TotalRevenue DESC; -- sắp xếp theo doanh thu giảm dần
";


            var result = await this._context.Database.SqlQueryRaw<TopStoreViewModels>(sqlQuery).ToListAsync();
            var b = result;
            return result;
        }
    }
}
