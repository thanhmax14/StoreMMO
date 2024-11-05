using BusinessLogic.Services.AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using BusinessLogic.Services.StoreMMO.Core.Categorys;
using BusinessLogic.Services.StoreMMO.Core.Disputes;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Utilities.Collections;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Balances;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.Repositories.Disputes;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.Repositories.StoreTypes;
using StoreMMO.WDF.ViewModels;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace StoreMMO.WDF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            string jsonFilePath = @"D:\connectionConfig.json";

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(jsonFilePath))
                .AddJsonFile(Path.GetFileName(jsonFilePath))
                .Build();

            string connectionString = config.GetConnectionString("df");
            services.AddLogging(builder =>
            {
                builder.AddConsole(); // Tùy chọn: Ghi log ra console
                builder.AddDebug();   // Tùy chọn: Ghi log cho môi trường debug
            });

            // Đăng ký AppDbContext với DbContextOptions
            services.AddDbContext<AppDbContext>(options =>
            {
                // Sử dụng SQL Server hoặc loại cơ sở dữ liệu khác tùy vào nhu cầu của bạn
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>()
      .AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders();

            // Đăng ký các service, repository, DAO mà bạn đã có sẵn
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IStoreTypeService, StoreTypeService>();
            services.AddTransient<IStoreTypeRepository, StoreTypeRepository>();
            services.AddTransient<IDisputeRepository, DisputeRepository>();
            services.AddTransient<IDisputeService, DisputeService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IBalanceService, BalanceService>();
            services.AddTransient<IBalanceRepository, BalanceRepository>();
            //services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<HiddenCategoriesListModel>();
            services.AddTransient<StoreTypeListViewModel>();
            services.AddTransient<HiddenStoreTypeListViewModel>();
            services.AddTransient<UserAccountViewModel>();
            services.AddTransient<HidentUserAccountViewModel>();
            services.AddTransient<RegisterSellerViewModel>();
            services.AddTransient<ManageDisputesViewModel>();
            services.AddTransient<AllStoreListViewModel>();
            services.AddTransient<HiddenStoreListViewModel>();
            services.AddTransient<ManageWithdrawlRequestViewModel>();
            services.AddTransient<AppDbContext>();
            //services.AddTransient<StudentViewModel>();
            //services.AddTransient<ClassViewModel>();

            services.AddAutoMapper(typeof(AutoMappers));

            // Đăng ký MainWindow   
            services.AddSingleton<MainWindow>();
        }
    }

}
