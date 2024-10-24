using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Withdraw
{
    public class WithdrawRepository : IWithdrawRepository
    {
        private readonly AppDbContext _context;
        public WithdrawRepository(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public IEnumerable<WithdrawViewModels> getAllWithdraw()
        {
            string sql = @"
    SELECT 
        Id,
        UserId,
        Amount,
        TransactionType,
        TransactionDate,
        Description,
        Status,
        ApprovalDate
    FROM 
        Balances
    WHERE 
        Status = 'PENDING'";  // Điều kiện lọc theo trạng thái (hoặc tuỳ chỉnh)

            // Thực hiện truy vấn SQL thô và ánh xạ kết quả vào danh sách BalanceViewModel
            var list = this._context.Database.SqlQueryRaw<WithdrawViewModels>(sql).ToList();
            return list;
        }

    }
}
