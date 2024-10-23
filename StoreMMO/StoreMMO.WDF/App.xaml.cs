using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Categorys;
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

            // Đăng ký AppDbContext với DbContextOptions
            services.AddDbContext<AppDbContext>(options =>
            {
                // Sử dụng SQL Server hoặc loại cơ sở dữ liệu khác tùy vào nhu cầu của bạn
                options.UseSqlServer(connectionString);
            });     // Đăng ký các service, repository, DAO mà bạn đã có sẵn
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            //services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<AppDbContext>();
            //services.AddTransient<StudentViewModel>();
            //services.AddTransient<ClassViewModel>();


            // Đăng ký MainWindow   
            services.AddSingleton<MainWindow>();
        }
    }

}
