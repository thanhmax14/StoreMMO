using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Core.ViewModels.SellerDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StoreMMO.Core.Repositories.SellerDashboard
{
    public class SellerDashBoardRepository : ISellerDashBoardRepository
    {
        private readonly AppDbContext _context;
        public SellerDashBoardRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<List<TransactionSummary>> GetDailyTransactionSummary(string id)  // Thống kê giao dịch trong ngày
        {
            string sqlQuery = @"SELECT 
        CONVERT(datetime, DATEADD(MINUTE, DATEDIFF(MINUTE, 0, od.Dates), 0), 120) AS TransactionDate,
   COUNT(DISTINCT ob.ID) AS TotalTransactions,    
        SUM(CAST(od.Price AS decimal(18, 2)) * CAST(od.Quantity AS int)) AS TotalRevenue
    FROM 
        OrderBuys ob
    JOIN 
        OrderDetails od ON ob.ID = od.OrderBuyID
    JOIN 
        Stores s ON ob.StoreID = s.Id
    WHERE 
        s.UserId = @id AND
        ob.Status = 'PAID' AND
        od.Dates >= CAST(GETDATE() AS DATE) AND
        od.Dates < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
    GROUP BY 
        CONVERT(datetime, DATEADD(MINUTE, DATEDIFF(MINUTE, 0, od.Dates), 0), 120)
    ORDER BY 
        TransactionDate;";

            var result = await this._context.Database
                .SqlQueryRaw<TransactionSummary>(sqlQuery, new SqlParameter("@id", id))
                .ToListAsync();

            return result;
        }
        public async Task<List<TransactionSummary>> GetMonth(string id)
        {
            string sqlQuery = @"SELECT 
        CONVERT(DATE, od.Dates) AS TransactionDate,                     
        COUNT(DISTINCT ob.ID) AS TotalTransactions,                     
        SUM(CAST(od.Price AS decimal(18, 2)) * CAST(od.quantity AS int)) AS TotalRevenue  
    FROM 
        OrderBuys ob
    JOIN 
        OrderDetails od ON ob.ID = od.OrderBuyID
    JOIN 
        Stores s ON ob.StoreID = s.Id
    WHERE 
        s.UserId = @id AND
        ob.Status = 'PAID'  
        AND MONTH(od.Dates) = MONTH(GETDATE())       -- Kiểm tra tháng hiện tại
        AND YEAR(od.Dates) = YEAR(GETDATE())         -- Kiểm tra năm hiện tại
    GROUP BY 
        CONVERT(DATE, od.Dates)                                         
    ORDER BY 
        TransactionDate;";

            // Khai báo tham số
            var result = await this._context.Database
                   .SqlQueryRaw<TransactionSummary>(sqlQuery, new SqlParameter("@id", id))
                   .ToListAsync();

            return result;
        }

        public TodayOrderSummary GetTotalSoldOrdersAndRevenueForToday(string sellerUserId)
        {
            string sqlQuery = @"
        SELECT 
            COUNT(DISTINCT od.ID) AS TotalSoldOrders,
         SUM(CAST(od.Price AS DECIMAL(18, 2))) AS TotalRevenue

        FROM 
            Stores s
        JOIN 
            OrderBuys ob ON s.ID = ob.StoreID
        JOIN 
            OrderDetails od ON ob.ID = od.OrderBuyID
        WHERE 
            s.UserID = @sellerUserId
            AND CAST(od.Dates AS DATE) = CAST(GETDATE() AS DATE)
            AND LOWER(ob.Status) = LOWER('Paid')
    ";

            var parameters = new[] {
        new SqlParameter("@sellerUserId", sellerUserId)
    };

            var result = _context.Database.SqlQueryRaw<TodayOrderSummary>(sqlQuery, parameters).FirstOrDefault();
            return result;
        }

        public async Task<List<TransactionSummary>> GetMonthlyTransactionSummary(string userId)
        {
            string sqlQuery = @"
    SELECT 
        CAST(DATEADD(MONTH, DATEDIFF(MONTH, 0, od.Dates), 0) AS DATETIME) AS TransactionDate,  -- Ngày đầu tháng
        COUNT(DISTINCT ob.ID) AS TotalTransactions,                                            -- Số lượng đơn hàng
        SUM(CAST(od.Price AS DECIMAL(18, 2)) * CAST(od.Quantity AS int)) AS TotalRevenue       -- Tổng doanh thu
    FROM 
        Stores s
    JOIN 
        OrderBuys ob ON s.ID = ob.StoreID
    JOIN 
        OrderDetails od ON ob.ID = od.OrderBuyID
    WHERE 
        s.UserID = @userId
        AND LOWER(ob.Status) = 'paid'                                                         -- Kiểm tra trạng thái đơn hàng
        AND YEAR(od.Dates) = YEAR(GETDATE())                                                  -- Lọc theo năm hiện tại
    GROUP BY 
        DATEADD(MONTH, DATEDIFF(MONTH, 0, od.Dates), 0)                                       -- Nhóm theo tháng
    ORDER BY 
        TransactionDate;";

            return await this._context.Database
                .SqlQueryRaw<TransactionSummary>(sqlQuery, new SqlParameter("@userId", userId))
                .ToListAsync();
        }
        public async Task<List<TransactionSummary>> GetYearlyTransactionSummary(string userId)
        {
            string sqlQuery = @"
    SELECT 
        CAST(DATEFROMPARTS(YEAR(od.Dates), 1, 1) AS DATETIME) AS TransactionDate,  -- Ngày đầu năm
        COUNT(od.ID) AS TotalTransactions,                                          -- Số lượng đơn hàng trong năm
        SUM(CAST(od.Price AS DECIMAL(18, 2)) * CAST(od.Quantity AS int)) AS TotalRevenue  -- Tổng doanh thu trong năm
    FROM 
        Stores s
    JOIN 
        OrderBuys ob ON s.ID = ob.StoreID
    JOIN 
        OrderDetails od ON ob.ID = od.OrderBuyID
    WHERE 
        s.UserID = @userId                                                           -- Điều kiện UserID
        AND LOWER(ob.Status) = 'paid'                                                -- Chỉ lấy các đơn hàng có trạng thái 'Paid'
    GROUP BY 
        DATEFROMPARTS(YEAR(od.Dates), 1, 1)                                          -- Nhóm theo ngày đầu năm
    ORDER BY 
        TransactionDate;";

            var parameters = new SqlParameter("@userId", userId);

            return await this._context.Database
                .SqlQueryRaw<TransactionSummary>(sqlQuery, parameters)
                .ToListAsync();
        }


    }

}