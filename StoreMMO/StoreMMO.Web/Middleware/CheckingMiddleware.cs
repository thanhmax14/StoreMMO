using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using Microsoft.AspNetCore.Http;
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
            var checkUserID = context.Session.GetString("UserID");
            if (checkUserID != null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var balanceService = scope.ServiceProvider.GetRequiredService<IBalanceService>();
                    var getListBalance = await balanceService.GetBalanceByUserIDAsync(checkUserID);

                    if (getListBalance != null)
                    {
                        foreach (var item in getListBalance)
                        {
                            if (item.TransactionType.Equals("Deposit", StringComparison.OrdinalIgnoreCase)
                                && item.Status.Equals("PENDING", StringComparison.OrdinalIgnoreCase)) 
                            {
                                await ProcessBalanceUpdate(item, balanceService);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            await _requestDelegate(context);
        }


        private async Task ProcessBalanceUpdate(BalanceViewModels item, IBalanceService balanceService)
        {
            if (long.TryParse(item.OrderCode, out long orderCode))
            {
                var checkStatusDepo = await _purApi.CheckOrder(orderCode);

                switch (checkStatusDepo?.status.ToUpperInvariant())
                {
                    case "PAID":
                        await UpdateBalanceStatus(item.Id, "PAID", balanceService);
                        break;
                    case "EXPIRED":
                        await UpdateBalanceStatus(item.Id, "EXPIRED", balanceService);
                        break;
                    case "CANCELLED":
                        await UpdateBalanceStatus(item.Id, "CANCELLED", balanceService);
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

        private async Task UpdateBalanceStatus(string balanceId, string status, IBalanceService balanceService)
        {
            var balance = await balanceService.GetBalanceByIDAsync(balanceId); 
            if (balance != null)
            {
                balance.Status = status;
                balance.approve = DateTime.Now;
                bool updateBalance = await balanceService.UpdateAsync(balance); 
                if (updateBalance)
                {
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
