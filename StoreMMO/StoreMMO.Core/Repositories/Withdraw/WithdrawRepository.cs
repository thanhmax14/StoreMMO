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
        * 
    FROM 
        Complaints
    WHERE 
        Status = 'Pending'";

            var list = this._context.Database.SqlQueryRaw<WithdrawViewModels>(sql).ToList();
            return list;
        }
    }
}
