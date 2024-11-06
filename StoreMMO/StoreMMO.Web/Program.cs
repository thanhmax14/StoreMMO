using System.Net.Http.Headers;
using BusinessLogic.Config;
using BusinessLogic.Services.Email;
using BusinessLogic.Services.StoreMMO.Core.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Products;
using StoreMMO.Web.Mapper;
using StoreMMO.Web.Middleware;



var builder = WebApplication.CreateBuilder(args);

//Addseverce
ConfigServices.ConfigureServices(builder.Services);


string jsonFilePath = @"D:\connectionConfig.json";


IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(jsonFilePath))
    .AddJsonFile(Path.GetFileName(jsonFilePath))
    .Build();

string connectionString = config.GetConnectionString("df");

builder.Services.AddControllersWithViews();





builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StoreMMO.Core")));


builder.Services.AddAuthentication()
	.AddGoogle(options =>
	{
		options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
		options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        options .SignInScheme = IdentityConstants.ExternalScheme;
		options.CallbackPath = "/signin-google";
        // Thêm prompt=select_account để hiển thị trang chọn tài khoản
        options.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            context.Response.Redirect(context.RedirectUri + "&prompt=select_account");
			return Task.CompletedTask;
        };
		options.Events.OnRemoteFailure = u =>
		{
			u.Response.Redirect("/Account/Login");
			u.HandleResponse();
			return System.Threading.Tasks.Task.CompletedTask;
		};
	});







builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});




builder.Services.AddHttpContextAccessor();


//IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //setting for user
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;

});

// Cấu hình Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Đường dẫn đến trang đăng nhập
    options.LogoutPath = "/Account/Logout"; // Đường dẫn đến trang đăng xuất
    options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn khi truy cập bị từ chối
    options.ExpireTimeSpan = TimeSpan.FromDays(14); // Thời gian lưu cookie
    options.SlidingExpiration = true; // Làm mới thời gian hết hạn cookie
});



// Mail Service
var mailsettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsettings);
builder.Services.AddTransient<IEmailSender, SendMailService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.AddMvc();
/*builder.Services.AddAutoMapper(typeof(Program));
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
await SeedDataAsync(app);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseMiddleware<CheckingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages(); // Đảm bảo rằng bạn đã có dòng này
});
app.Run();
static async Task SeedDataAsync(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        // Create roles if they don't exist
        string[] roles = { "User", "Seller", "Admin" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Create a default admin user if it doesn't exist
        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        if (adminUser == null)
        {
            adminUser = new AppUser { UserName = "admin", Email = "admin@gmail.com" };
            var result = await userManager.CreateAsync(adminUser, "Password123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}