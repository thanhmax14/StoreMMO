using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using BusinessLogic.Services.StoreMMO.Core.Categorys;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.User;
using Microsoft.Extensions.DependencyInjection;
using StoreMMO.Core.Repositories.Carts;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Config
{
    public class ConfigServices
    {
       
        public static void ConfigureServices(IServiceCollection services)
        {
            
            static void ConfigureHttpClient(HttpClient client)
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            //Service for api
            services.AddHttpClient<StoreApiService>(ConfigureHttpClient);
            services.AddHttpClient<ProductApiService>(ConfigureHttpClient);

            //Services for Core
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserServices, UserService>();



        }
    }
}

