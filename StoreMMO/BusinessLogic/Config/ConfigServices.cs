
using System.Net.Http.Headers;
using BusinessLogic.Services.AutoMapper;
using BusinessLogic.Services.CreateQR;
using BusinessLogic.Services.Payments;
using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.Carts;
using BusinessLogic.Services.StoreMMO.Core.Categorys;
using BusinessLogic.Services.StoreMMO.Core.Disputes;
using BusinessLogic.Services.StoreMMO.Core.FeedBacks;
using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using BusinessLogic.Services.StoreMMO.Core.RegisteredSeller;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using BusinessLogic.Services.StoreMMO.Core.User;
using BusinessLogic.Services.StoreMMO.Core.WishLists;
using BusinessLogic.Services.StoreMMO.Core.Withdraws;
using Microsoft.Extensions.DependencyInjection;
using Net.payOS;
using StoreMMO.Core.Repositories.Balances;
using StoreMMO.Core.Repositories.Carts;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.Repositories.Disputes;
using StoreMMO.Core.Repositories.FeedBacks;
using StoreMMO.Core.Repositories.OrderDetails;
using StoreMMO.Core.Repositories.orderDetailViewModels;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.Repositories.ProductsTypes;
using StoreMMO.Core.Repositories.Purchase;
using StoreMMO.Core.Repositories.RegisteredSeller;
using StoreMMO.Core.Repositories.StoreDetails;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.Repositories.StoreTypes;
using StoreMMO.Core.Repositories.User;
using StoreMMO.Core.Repositories.WishLists;
using StoreMMO.Core.Repositories.Withdraw;
using BusinessLogic.Services.StoreMMO.Core.Withdraws;
using Net.payOS;
using BusinessLogic.Services.Payments;
using BusinessLogic.Services.CreateQR;
using StoreMMO.Core.Repositories.Balances;
using BusinessLogic.Services.StoreMMO.Core.Balances;

using StoreMMO.Core.Repositories.ComplaintsN;
using BusinessLogic.Services.StoreMMO.Core.ComplaintsN;

using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using StoreMMO.Core.Repositories.OrderDetails;
using StoreMMO.Core.Repositories.orderDetailViewModels;
using CloudinaryDotNet;
using StoreMMO.Core.Repositories.ProductsConnect;
using BusinessLogic.Services.StoreMMO.Core.ProductConnects;
using StoreMMO.Core.Repositories.SellerDashboard;
using BusinessLogic.Services.StoreMMO.Core.SellerDashBoard;
using Microsoft.AspNetCore.Identity;
using StoreMMO.Core.Models;


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
            services.AddHttpClient<PurchaseApiService>(ConfigureHttpClient);



            //Services for Repositoty
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IDisputeRepository, DisputeRepository>();

            services.AddScoped<IComplaintsRepository, ComplaintsRepository>();

            services.AddScoped<IWithdrawRepository, WithdrawRepository>();
            services.AddScoped<ISellerDashBoardRepository, SellerDashBoardRepository>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IOrderDeailsRepository, OrderDeailsRepository>();

            // Đăng ký StoreTypeRepository với DI container
            services.AddScoped<IStoreTypeRepository, StoreTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStoreTypeRepository, StoreTypeRepository>();
            services.AddScoped<IRegisteredSellerRepository, RegisteredSellerRepository>();
            services.AddScoped<IStoreDetailsService, StoreDetailsService>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();
            services.AddScoped<IProductConnectRepository, ProductConnectRepository>();


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
            services.AddScoped<IWithdrawService, WithdrawService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IComplaintsService, ComplaintsService>();
            services.AddScoped<IDisputeService, DisputeService>();
            services.AddScoped<IOderDetailsService, OrderDetailsService>();
            services.AddScoped<IFeedBackService, FeedBackService>();
            services.AddScoped<ISellerDashBoardService, SellerDashBoardService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IProductConnectService, ProductConnectService>();

			services.AddIdentity<AppUser, IdentityRole>(options =>
			{

				options.SignIn.RequireConfirmedAccount = true;
			}
)
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();



			services.AddTransient<PaymentLIb>();
            PayOS payOS = new PayOS("fa2021f3-d725-4587-a48f-8b55bccf7744" ?? throw new Exception("Cannot find environment"),
                    "143f45b5-d1d7-40e4-82e9-00ea8217ab33" ?? throw new Exception("Cannot find environment"),
                   "7861335ef9257ac91143d4de7b9f6ce64c864608defe1e31906510e95b345ee5" ?? throw new Exception("Cannot find environment"));
            services.AddSingleton(payOS);
            services.AddTransient<CreateQR>();

            var account = new Account(
                "do9bojdku", // Cloud Name
                "949824695166918", // API Key
                "cXZp8UJwFvIQi5D0HtK94MqOASA" // API Secret
            );

            Cloudinary cloudinary = new Cloudinary(account);

         

            // Đăng ký Cloudinary vào DI container
            services.AddSingleton(cloudinary);


        }
    }
}

