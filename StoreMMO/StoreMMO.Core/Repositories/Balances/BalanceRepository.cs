using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<AppUser> _userManager;

		public BalanceRepository(AppDbContext context, UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
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
		public async Task<IEnumerable<BalanceViewModels>> GetAllBalanceAsync()
		{
			var obj = await _context.Balances
				.Where(x => x.TransactionType == "withdraw" && x.Status == "PENDING")
				.OrderBy(x => x.TransactionDate)
				.Select(x => new BalanceViewModels
				{
					Id = x.Id,
					UserId = x.UserId,
					Amount = x.Amount,
					TransactionType = x.TransactionType,
					TransactionDate = x.TransactionDate,
					Description = x.Description,
					Status = x.Status,
					OrderCode = x.OrderCode,
					approve = x.ApprovalDate
				})
				.ToListAsync();

			return obj;
		}
		// Phương thức từ chối yêu cầu với transaction
		public async Task<bool> RejectRequestAsync(BalanceViewModels balanceViewModels, string reason)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					// Tìm balance theo Id, không dùng AsNoTracking
					var balance = await _context.Balances.FirstOrDefaultAsync(b => b.Id == balanceViewModels.Id);
					if (balance == null || balance.Status != "PENDING")
					{
						throw new InvalidOperationException("Balance request không tồn tại hoặc không ở trạng thái chờ.");
					}

					// Cập nhật lý do từ chối và trạng thái
					balance.Status = "CANCELLED";
					balance.Description = reason;

					// Cập nhật lại balance trong database
					_context.Balances.Update(balance);

					bool saveFailed;
					do
					{
						saveFailed = false;
						try
						{
							await _context.SaveChangesAsync(); // Ghi các thay đổi
						}
						catch (DbUpdateConcurrencyException ex)
						{
							saveFailed = true;
							var entry = ex.Entries.Single();
							var databaseValues = entry.GetDatabaseValues();
							if (databaseValues == null)
							{
								// The entity was deleted by another user
								throw new InvalidOperationException("The balance request was deleted by another user.");
							}
							else
							{
								// Resolve the concurrency conflict by keeping the database values
								entry.OriginalValues.SetValues(databaseValues);
							}
						}
					} while (saveFailed);

					// Lấy user và cập nhật CurrentBalance
					var user = await _userManager.FindByIdAsync(balance.UserId);
					if (user == null)
					{
						throw new InvalidOperationException("User not found.");
					}

					// Tải lại thông tin mới nhất của user từ cơ sở dữ liệu
					await _context.Entry(user).ReloadAsync(); // Reload user

					// Cập nhật CurrentBalance của user với số tiền từ balance
					user.CurrentBalance += Math.Abs(balance.Amount);
					var result = await _userManager.UpdateAsync(user);

					if (result.Succeeded)
					{
						// Commit transaction nếu thành công
						await transaction.CommitAsync();
						return true;
					}
					else
					{
						// Rollback nếu có lỗi
						await transaction.RollbackAsync();
						return false;
					}
				}
				catch (DbUpdateConcurrencyException ex)
				{
					// Xử lý xung đột nếu xảy ra
					await transaction.RollbackAsync();
					// Có thể cần ghi lại lỗi hoặc thông báo cho người dùng
					return false;
				}
				catch (Exception ex)
				{
					// Rollback transaction nếu có lỗi khác
					await transaction.RollbackAsync();
					return false;
				}
			}
		}
	}
}
	

