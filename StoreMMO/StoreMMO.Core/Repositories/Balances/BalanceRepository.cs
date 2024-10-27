using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Nhớ import namespace này cho Task

namespace StoreMMO.Core.Repositories.Balances
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly AppDbContext _context;

        public BalanceRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm mới Balance
        public async Task<bool> AddAsync(BalanceViewModels balanceViewModels)
        {
            try
            {
                var balance = new Balance
                {
                    UserId = balanceViewModels.UserId,
                    Amount = balanceViewModels.Amount,
                    TransactionType = balanceViewModels.TransactionType,
                    TransactionDate = balanceViewModels.TransactionDate,
                    Description = balanceViewModels.Description,
                    Status = balanceViewModels.Status,
                    OrderCode = balanceViewModels.OrderCode,
                    Id = balanceViewModels.Id,
                };

                await _context.Balances.AddAsync(balance); // Sử dụng AddAsync để thêm
                await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync để lưu
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa Balance
        public async Task<bool> DeleteAsync(BalanceViewModels balanceViewModels)
        {
            try
            {
                var balance = await _context.Balances.FirstOrDefaultAsync(b => b.Id == balanceViewModels.Id);
                if (balance != null)
                {
                    _context.Balances.Remove(balance);
                    await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Sửa Balance
        public async Task<bool> EditAsync(BalanceViewModels balanceViewModels)
        {
            try
            {
                var balance = await _context.Balances.FirstOrDefaultAsync(b => b.Id == balanceViewModels.Id);
                if (balance != null)
                {
                    balance.Amount = balanceViewModels.Amount;
                    balance.TransactionType = balanceViewModels.TransactionType;
                    balance.TransactionDate = balanceViewModels.TransactionDate;
                    balance.Description = balanceViewModels.Description;
                    balance.Status = balanceViewModels.Status;
                    balance.OrderCode = balanceViewModels.OrderCode;
                    balance.ApprovalDate = balanceViewModels.approve;
                    _context.Balances.Update(balance);
                    await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Cập nhật Balance (tương tự Edit)
        public Task<bool> UpdateAsync(BalanceViewModels balanceViewModels)
        {
            return EditAsync(balanceViewModels);
        }

        // Lấy Balance theo OrderCode
        public async Task<BalanceViewModels> GetBalanceByOrderCodeAsync(long orderCode)
        {
            var balance = await _context.Balances.FirstOrDefaultAsync(b => b.OrderCode == orderCode.ToString());
            if (balance != null)
            {
                return new BalanceViewModels
                {
                    Id = balance.Id,
                    UserId = balance.UserId,
                    Amount = balance.Amount,
                    TransactionType = balance.TransactionType,
                    TransactionDate = balance.TransactionDate,
                    Description = balance.Description,
                    Status = balance.Status,
                    OrderCode = balance.OrderCode
                };
            }
            return null;
        }

        public async Task<BalanceViewModels> GetBalanceByIDAsync(string id)
        {
            var balance = await _context.Balances.FindAsync(id);
            if (balance != null)
            {
                return new BalanceViewModels
                {
                    Id = balance.Id,
                    UserId = balance.UserId,
                    Amount = balance.Amount,
                    TransactionType = balance.TransactionType,
                    TransactionDate = balance.TransactionDate,
                    Description = balance.Description,
                    Status = balance.Status,
                    OrderCode = balance.OrderCode,
                    approve = balance.ApprovalDate
                };
            }
            return null;
        }

        public async Task<IEnumerable<BalanceViewModels>> GetBalanceByUserIDAsync(string userId)
        {
            return await _context.Balances
                .Where(b => b.UserId == userId)
                .Select(b => new BalanceViewModels
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    Amount = b.Amount,
                    TransactionType = b.TransactionType,
                    TransactionDate = b.TransactionDate,
                    Description = b.Description,
                    Status = b.Status,
                    OrderCode = b.OrderCode,
                    approve = b.ApprovalDate
                }).OrderByDescending(b => b.TransactionDate)
                .ToListAsync(); // Sử dụng ToListAsync
        }
    }
}
