using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.StoreMMO.API;
using BusinessLogic.Services.StoreMMO.Core.FeedBacks;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;


namespace StoreMMO.Web.Pages.Home
{
    public class StoredetailModel : PageModel
    {
        private readonly StoreApiService _storeApi;
        private readonly ProductApiService _productApi;
        private readonly WishListApiService _wishListApi;
        private readonly IPurchaseService _puchae;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFeedBackService _feed;
        private readonly IStoreDetailsService _storeDetailSe;

        public AppUser info { get; set; }

        public StoredetailModel(AppDbContext context, StoreApiService storeApiService, ProductApiService productApi,
            WishListApiService wish, IPurchaseService purchaseService, IFeedBackService feed, IStoreDetailsService storeDetailSe)
        {
            _context = context;
            this._storeApi = storeApiService;
            this._productApi = productApi;
            this._wishListApi = wish;
            this._puchae = purchaseService;
            _feed = feed;
            _storeDetailSe = storeDetailSe;
        }

        [BindProperty]
        public List<StoreDetailViewModel> ListDetail { get; set; }
        public List<WishListViewModels> wishnew = new List<WishListViewModels>();
        public IEnumerable<FeedBack> feedBacks = new List<FeedBack>();

        [TempData]
        public string DefauPrice { get; set; }

        [TempData]
        public string DefauStock { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {

            this._puchae.SaveProductToSession(null);
            var useriD = HttpContext.Session.GetString("UserID");
            if (useriD != null)
            {
                TempData["defauUSerid"] ="$"+useriD;
                wishnew = await this._wishListApi.getByUserID(useriD);
            }
            else
            {
                TempData["defauUSerid"] = "";
            }
            
            try
            {
                ListDetail = await _storeApi.GetStoreDetail(id);
                if (ListDetail == null || ListDetail.Count == 0)
                {
                    return NotFound();
                }
                var getStoreDtail = this._storeDetailSe.GetByIdStoDetails1(id);
                feedBacks = this._feed.GetFeebackByStoreID(getStoreDtail.Id);
                var aas = feedBacks;
                foreach (var item in ListDetail)
                {
                    if (item.ProductStock.Count >= 1)
                    {
                        var defauPrce = await _productApi.GetProductById(item.ProductStock.FirstOrDefault().Value);
                        DefauPrice = "$" + defauPrce.Price;
                        DefauStock = defauPrce.Stock;
                        TempData["defauProid"] = defauPrce.Id;
                        return Page();
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
