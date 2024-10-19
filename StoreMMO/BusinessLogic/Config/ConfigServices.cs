using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using BusinessLogic.Services.StoreMMO.Core.Categorys;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.RegisteredSeller;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using BusinessLogic.Services.StoreMMO.Core.User;
using BusinessLogic.Services.StoreMMO.Core.WishLists;
using Microsoft.Extensions.DependencyInjection;
using StoreMMO.Core.Repositories.Carts;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.Repositories.ProductsTypes;
using StoreMMO.Core.Repositories.RegisteredSeller;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.Repositories.StoreTypes;
using StoreMMO.Core.Repositories.User;
using StoreMMO.Core.Repositories.WishLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using BusinessLogic.Services.AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using StoreMMO.Core.Repositories.StoreDetails;

using StoreMMO.Core.Repositories.Disputes;
using BusinessLogic.Services.StoreMMO.Core.Disputes;

using BusinessLogic.Services.StoreMMO.Core.Purchases;
using StoreMMO.Core.Repositories.Purchase;

namespace BusinessLogic.Config
{
    public class ConfigServices
    {

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<AutoMappers>());

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
            services.AddScoped<IDisputeRepository, DisputeRepository>();


            // Đăng ký StoreTypeRepository với DI container
            services.AddScoped<IStoreTypeRepository, StoreTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStoreTypeRepository, StoreTypeRepository>();
            services.AddScoped<IRegisteredSellerRepository, RegisteredSellerRepository>();
            services.AddScoped<IStoreDetailsService, StoreDetailsService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();




            //Services for Services
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IWishListsService, WishListsService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IStoreTypesService, StoreTypesService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStoreTypeService, StoreTypeService>();
            services.AddScoped<IRegisteredSellerService, RegisteredSellerService>();
            services.AddScoped<IStoreDetailRepository, StoreDetailRepository>();

            services.AddScoped<IDisputeService, DisputeService>();


            services.AddScoped<IPurchaseService , PurchaseService>();




        }
    }
}

