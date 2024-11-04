using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Home
{
    public class ViewAccountDetailModel : PageModel
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;
        private readonly IProductTypeService _productTypeService;
        private readonly UserManager<AppUser> _userManager;


        public IEnumerable<ManageStoreViewModels> products = new List<ManageStoreViewModels>();
        public IEnumerable<ProductType> ProductTypes { get; set; }
        private readonly AppDbContext _context;
        [BindProperty]
        public List<StoreDetailViewModel> ListDetail { get; set; }
        public ViewAccountDetailModel(IProductService product, IMapper mapper, IProductTypeService productTypeService, UserManager<AppUser> userManager)
        {
            _product = product;
            _mapper = mapper;
            _productTypeService = productTypeService;
            _userManager = userManager;
        }

        public UserProfileViewModels UserProfile { get; set; }
        public AppUser info { get; set; }
        public async Task<IActionResult> OnGet(string username)
        {
            info = await this._userManager.FindByNameAsync(username);
            await LoadUserDataAsync(username);
            if (info != null)
            {
                return Page();
            }
            else return NotFound();

            //products = _product.ManageStore();
            /*     products = _product.ManageStore();*/
            /*   if (username != null)
               {
                    
               }*/
        }
        public async Task LoadUserDataAsync(string username)
        {
            UserProfile = new UserProfileViewModels();

            // Lấy thông tin người dùng theo username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                UserProfile.Account = user.UserName;
                UserProfile.RegisteredDate = user.CreatedDate;

                // Tính tổng sản phẩm đã mua dưới dạng double
                var productsPurchased = await _context.OrderDetails
                    .Where(orderDetail => orderDetail.orderBuy.AppUser.UserName == username)
                    .ToListAsync();

                UserProfile.ProductsPurchased = productsPurchased.Sum(od =>
                {
                    double quantity = 0;
                    double.TryParse(od.quantity, out quantity); // Chuyển đổi từ string sang double
                    return quantity;
                });

                // Tính số lượng cửa hàng
                var stores = await _context.Stores
                    .Where(store => store.User.UserName == username)
                    .ToListAsync();
                UserProfile.NumberOfStores = stores.Count;

                // Tính tổng sản phẩm đã bán dưới dạng double
                var productsSold = await _context.OrderDetails
                    .Where(orderDetail => orderDetail.orderBuy.Store.User.UserName == username)
                    .ToListAsync();

                UserProfile.ProductsSold = productsSold.Sum(od =>
                {
                    double quantity = 0;
                    double.TryParse(od.quantity, out quantity); // Chuyển đổi từ string sang double
                    return quantity;
                });
            }
        }
    }
}

