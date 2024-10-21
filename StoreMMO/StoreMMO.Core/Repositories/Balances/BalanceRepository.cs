
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public bool add(BalanceViewModels balanceViewModels)
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
                    OrderCode = balanceViewModels.OrderCode
                };

                _context.Balances.Add(balance);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Xóa Balance
        public bool Delete(BalanceViewModels balanceViewModels)
        {
            try
            {
                var balance = _context.Balances.FirstOrDefault(b => b.Id == balanceViewModels.Id);
                if (balance != null)
                {
                    _context.Balances.Remove(balance);
                    _context.SaveChanges();
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
        public bool Edit(BalanceViewModels balanceViewModels)
        {
            try
            {
                var balance = _context.Balances.FirstOrDefault(b => b.Id == balanceViewModels.Id);
                if (balance != null)
                {
                    balance.Amount = balanceViewModels.Amount;
                    balance.TransactionType = balanceViewModels.TransactionType;
                    balance.TransactionDate = balanceViewModels.TransactionDate;
                    balance.Description = balanceViewModels.Description;
                    balance.Status = balanceViewModels.Status;
                    balance.OrderCode = balanceViewModels.OrderCode;
                    _context.Balances.Update(balance);
                    _context.SaveChanges();
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
        public bool Update(BalanceViewModels balanceViewModels)
        {
            return Edit(balanceViewModels);
        }

        // Lấy Balance theo OrderCode
        public BalanceViewModels GetBalanceByOrderCode(long orderCode)
        {
            var balance = _context.Balances.FirstOrDefault(b => b.OrderCode == orderCode.ToString());
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
        public BalanceViewModels GetBalanceByID(string id)
        {
            var balance = _context.Balances.Find(id);
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
        public IEnumerable<BalanceViewModels> getBalaceByUserID(string userId)
        {
            return _context.Balances
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
                    OrderCode = b.OrderCode
                })
                .ToList();
        }


    }
}
