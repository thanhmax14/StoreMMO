namespace StoreMMO.Web.Middleware
{
    public class CheckingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public CheckingMiddleware(RequestDelegate next)
        {
            this._requestDelegate = next;
        }
        public async Task Invoke(HttpContext context)
        {

            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            



            await _requestDelegate(context);
        }
    }
}
