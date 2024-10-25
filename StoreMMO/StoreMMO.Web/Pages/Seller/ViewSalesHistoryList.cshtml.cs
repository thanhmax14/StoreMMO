using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Seller
{
    public class ViewSalesHistoryListModel : PageModel
    {
        private readonly IOderDetailsService _orderDetailService;
        public ViewSalesHistoryListModel(IOderDetailsService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        public IEnumerable<SaleHistoryViewModels> list = new List<SaleHistoryViewModels>();
        public IEnumerable<OrderBuyViewModels> listOrderBuy = new List<OrderBuyViewModels>();


        [BindProperty]
        public string id { get; set; }
        [TempData]
        public string success { get; set; }
        [TempData]
        public string fail { get; set; }
        public void OnGet()
        {
            list = this._orderDetailService.getAll();
        }

        public void UpdateStoreIsAccept(SaleHistoryViewModels storeView)
        {
            
            list = this._orderDetailService.getAll();
        }



    }
}
