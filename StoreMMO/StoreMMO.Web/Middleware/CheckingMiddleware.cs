using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Threading.Tasks;

namespace StoreMMO.Web.Middleware
{
	public class CheckingMiddleware
	{
		private readonly RequestDelegate _requestDelegate;
		private readonly PurchaseApiService _purApi;
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public CheckingMiddleware(RequestDelegate next, PurchaseApiService apiService, IServiceScopeFactory serviceScopeFactory)
		{
			_requestDelegate = next;
			_purApi = apiService;
			_serviceScopeFactory = serviceScopeFactory;
		}

		public async Task Invoke(HttpContext context)
		{
		/*	context.Session.SetString("UserID", "1f0dbbe2-2a81-43e9-8272-117507ac9c45");
			context.Session.SetString("Email", "thanhpqce171732@fpt.edu.vn");*/
			var checkUserID = context.Session.GetString("UserID");
			if (checkUserID != null)
			{
				using (var scope = _serviceScopeFactory.CreateScope())
				{
					var balanceService = scope.ServiceProvider.GetRequiredService<IBalanceService>();
					var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
					var getListBalance = await balanceService.GetBalanceByUserIDAsync(checkUserID);

					if (getListBalance != null)
					{
						foreach (var item in getListBalance)
						{
							if (item.TransactionType.Equals("Deposit", StringComparison.OrdinalIgnoreCase)
								&& item.Status.Equals("PENDING", StringComparison.OrdinalIgnoreCase))
							{
								await ProcessBalanceUpdate(item, balanceService, userManager);
							}
						}
					}
				}
			}
			Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
			await _requestDelegate(context);
		}

		private async Task ProcessBalanceUpdate(BalanceViewModels item, IBalanceService balanceService, UserManager<AppUser> userManager)
		{
			if (long.TryParse(item.OrderCode, out long orderCode))
			{
				var checkStatusDepo = await _purApi.CheckOrder(orderCode);

				switch (checkStatusDepo?.status.ToUpperInvariant())
				{
					case "PAID":
						await UpdateBalanceStatus(item.Id, "PAID", item.UserId, item.Amount, balanceService, userManager);
						break;
					case "EXPIRED":
						await UpdateBalanceStatus(item.Id, "EXPIRED", item.UserId, item.Amount, balanceService, userManager);
						break;
					case "CANCELLED":
						await UpdateBalanceStatus(item.Id, "CANCELLED", item.UserId, item.Amount, balanceService, userManager);
						break;
					default:
						Console.WriteLine($"Unknown status for Balance ID: {item.Id} - Status: {checkStatusDepo?.status}");
						break;
				}
			}
			else
			{
				Console.WriteLine($"OrderCode is not valid for Balance ID: {item.Id}");
			}
		}

		private async Task UpdateBalanceStatus(string balanceId, string status, string userId, decimal amount, IBalanceService balanceService, UserManager<AppUser> userManager)
		{
			var balance = await balanceService.GetBalanceByIDAsync(balanceId);
			if (balance != null)
			{
				balance.Status = status;
				balance.approve = DateTime.Now;
				bool updateBalance = await balanceService.UpdateAsync(balance);
				if (updateBalance)
				{
					if(status.ToLower()== "PAID".ToLower())
					{
						var user = await userManager.FindByIdAsync(userId);
						if (user != null)
						{
							user.CurrentBalance += amount;
							await userManager.UpdateAsync(user);
						}
					}

					Console.WriteLine($"Successfully updated balance ID: {balanceId} to status: {status}");
				}
				else
				{
					Console.WriteLine($"Failed to update balance ID: {balanceId}");
				}
			}
		}
	}
}
