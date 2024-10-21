using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Balances;

namespace StoreMMO.Web.Middleware
{
    public class CheckingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly PurchaseApiService _purApi;
        private readonly IBalanceService _balance;
        public CheckingMiddleware(RequestDelegate next, PurchaseApiService apiService, IBalanceService balance)
        {
            this._requestDelegate = next;
            this._purApi = apiService;
            this._balance = balance;
        }
        public async Task Invoke(HttpContext context)
        {
            var check = context.Session.GetString("UserID");
            if (check != null)
            {
                
            }
           
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            
           


            await _requestDelegate(context);
        }
    }
}
