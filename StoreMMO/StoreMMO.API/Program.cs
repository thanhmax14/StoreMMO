using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.API.Services;
using StoreMMO.Core.Repositories.Carts;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.Repositories.Products;

var builder = WebApplication.CreateBuilder(args);


string jsonFilePath = @"D:\connectionConfig.json";

// Đọc file JSON và xây dựng cấu hình
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(jsonFilePath))
    .AddJsonFile(Path.GetFileName(jsonFilePath))
    .Build();

// Lấy chuỗi kết nối từ file JSON
string connectionString = config.GetConnectionString("df");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StoreMMO.Core")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});



//Services for CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:44380")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
            policy.WithOrigins("https://localhost:44320").AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddHttpContextAccessor();




builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
