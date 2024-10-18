using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using BusinessLogic.Services.StoreMMO.Core.Categorys;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.User;
using BusinessLogic.Services.StoreMMO.Core.WishLists;
using Microsoft.Extensions.DependencyInjection;
using StoreMMO.Core.Repositories.Carts;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.Repositories.ProductsTypes;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.Repositories.User;
using StoreMMO.Core.Repositories.WishLists;
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
            services.AddHttpClient<WishListApiService>(ConfigureHttpClient);
            services.AddHttpClient<CategoryApiService>(ConfigureHttpClient);



            //Services for Repositoty
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();



            //Services for Services
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IWishListsService, WishListsService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();



        }
    }
}

