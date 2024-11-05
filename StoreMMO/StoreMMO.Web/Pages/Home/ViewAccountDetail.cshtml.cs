using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.User;
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
        private readonly IUserServices _severce;


        public IEnumerable<ManageStoreViewModels> products = new List<ManageStoreViewModels>();
        public IEnumerable<ProductType> ProductTypes { get; set; }
        private readonly AppDbContext _context;
        [BindProperty]
        public List<StoreDetailViewModel> ListDetail { get; set; }
        public ViewAccountDetailModel(IProductService product, IMapper mapper, IProductTypeService productTypeService,
            UserManager<AppUser> userManager, IUserServices severce)
        {
            _product = product;
            _mapper = mapper;
            _productTypeService = productTypeService;
            _userManager = userManager;
            this._severce = severce;
        }

        public UserProfileViewModels UserProfile { get; set; }
     public getInfoSeller infoSeller { get; set; }
        public async Task<IActionResult> OnGet(string username)
        {
            var find = await this._userManager.FindByNameAsync(username);
         
            if (find != null)
            {

                var gettotal = this._severce.getNumberBuy(find.Id);
                if (gettotal != null)
                {
                    var tem = new getInfoSeller {
                    join =  find.CreatedDate,
                    totalBuy = gettotal.First().totalBuy,
                    totalStore = gettotal.First().totalStore,
                    totalSol = gettotal.First().totalSold,
                    username = find.UserName,
                    };

                    infoSeller = tem;
                                
                }







               

                return Page();
            }
            else return NotFound();

        }
      
    }
}

