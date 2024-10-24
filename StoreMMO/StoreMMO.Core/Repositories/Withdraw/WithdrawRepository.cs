using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.AutoMapper.ViewModelAutoMapper;
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
        private readonly IMapper _mapper;
        public WithdrawRepository(AppDbContext dbContext, IMapper mapper)
        {
            this._context = dbContext;
            this._mapper = mapper;
        }

        public IEnumerable<BalanceMapper> getAllBalance()
        {
            var balances = _context.Balances
                .Where(b => b.Status == "PENDING") // Filter by Status (or customize)
                .Include(b => b.User) // Include User entity for each Balance
                .ToList();

            // Use AutoMapper to map Balance entities to BalanceMapper DTOs
            var mappedBalances = _mapper.Map<List<BalanceMapper>>(balances);

            return mappedBalances;
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
        Status = 'PENDING'
        AND TransactionType = 'withdraw'";  // Điều kiện lọc theo trạng thái (hoặc tuỳ chỉnh)

            // Thực hiện truy vấn SQL thô và ánh xạ kết quả vào danh sách BalanceViewModel
            var list = this._context.Database.SqlQueryRaw<WithdrawViewModels>(sql).ToList();
            return list;
        }

    }
}
