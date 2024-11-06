
using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;
using System.Linq;

namespace StoreMMO.Web.Pages.Purchase
{
    [Authorize(Roles = "User,Seller")]
    public class checkoutModel : PageModel
    {
        private readonly IPurchaseService _purchase;
        private readonly IProductTypeService _productType;
        private readonly IStoreTypeService _storeType;
        private readonly IProductService _product;
        private readonly IStoreDetailsService _storeDetails;
        private readonly IOderDetailsService _Detail;
        private readonly UserManager<AppUser> _manager;
        private readonly IBalanceService _balance;
 


        public checkoutModel(IPurchaseService purchase, IProductTypeService productService, IStoreTypeService typeService,
            IProductService product,IStoreDetailsService storeDetails, IOderDetailsService oderDetails,
            UserManager<AppUser> manager, IBalanceService balance



            )
        {
            this._purchase = purchase;
            this._productType = productService;
            this._storeType = typeService;
            this._product = product;
            this._storeDetails = storeDetails;
            this._Detail = oderDetails;
            this._manager = manager;
            this._balance= balance;
        }

        public List<PurchaseItem> purchaseItems { get; set; } = new List<PurchaseItem>();
        [TempData]
        public string TotalPrice { get; set; } = "0";
    

        public IActionResult OnGetAsync()
        {
            purchaseItems = this._purchase.GetProductFromSession();
           if(purchaseItems.Count< 0 || purchaseItems.IsNullOrEmpty() )
            {
                return NotFound();
            }
            else
            {
                TotalPrice =""+ purchaseItems.Sum(u => decimal.Parse(u.total));
                return Page();
            }
        }
        public async Task<IActionResult> OnPostBuyAsync()
        {
            var getProductItem = this._purchase.GetProductFromSession();
            if (getProductItem == null || getProductItem.Count == 0)
            {
                return NotFound();
            }
            var count = 0;
            if (getProductItem.Count > 1)
            {
                foreach (var item in getProductItem)
                {
                    count++;
                    var singleItemList = new List<PurchaseItem> { item };
                    if (count == getProductItem.Count)
                    {
                        return await Buyne(singleItemList);
                    }

                    await Buyne(singleItemList);
                }
                return RedirectToPage("/Purchase/checkout"); 
            }
            else 
            {
                return await Buyne(getProductItem);
            }
        }



        private async Task<IActionResult> Buyne(List<PurchaseItem> purchaseItems)
        {
			var checkUser = HttpContext.Session.GetString("UserID");
			if (checkUser != null)
			{
				
				if (!purchaseItems.IsNullOrEmpty())
				{
					var commission = 0.0;
					var orderCode = EncryptSupport.GenerateRandomString(10);
					var totalBuy = purchaseItems
						   .Where(item => decimal.TryParse(item.total, out _))
						   .Sum(item => decimal.Parse(item.total));
					var user = await this._manager.FindByIdAsync(checkUser);
					if (totalBuy > user.CurrentBalance)
					{
						return new JsonResult(new { success = false, message = "Ban Khong Du Tien De Mua Hang" });
					}
					foreach (var item in purchaseItems)
					{
						var addbalane = await this._balance.AddAsync(new BalanceViewModels
						{
							Id = Guid.NewGuid().ToString(),
							Amount = totalBuy,
							TransactionDate = DateTime.Now,
							TransactionType = "Buy",
							Description = "Buy Order: " + orderCode,
							approve = DateTime.Now,
							UserId = checkUser,
							Status = "PAID",
						});
						if (addbalane)
						{
							var finduse = await this._manager.FindByIdAsync(checkUser);
							if (finduse != null)
							{
								finduse.CurrentBalance -= totalBuy;
								await this._manager.UpdateAsync(finduse);
							}
						}
						var getInfo = this._productType.GetInfoByProductid(item.ProductID);

						if (getInfo.Count() > 0 || !getInfo.IsNullOrEmpty())
						{

							var tempid = Guid.NewGuid().ToString();
							bool Buy = false;
							var productTypeTem = "";
							foreach (var intemPro in getInfo)
							{
								productTypeTem = item.ProductID;
								var tem = new OrderBuyViewModels
								{
									ID = tempid,
									OrderCode = orderCode,
									ProductTypeId = productTypeTem,
									StoreID = intemPro.StoreID,
									UserID = checkUser,
									Status = "PAID/no",
									totalMoney = "" + purchaseItems.Sum(u => decimal.Parse(u.total))
								};
								commission = this._storeType.GetCommitssionByStoreID(intemPro.StoreID);
								Buy = this._purchase.add(tem);
								break;
							}
							var getInfoByProductType = this._productType.getByIDProduct(productTypeTem);
							if (getInfoByProductType != null)
							{
								if (int.Parse(item.quantity) <= int.Parse(getInfoByProductType.Stock))
								{
									var getProduct = this._product.getProductsByTypeID(getInfoByProductType.Id);
									var cout = int.Parse(item.quantity);
									foreach (var productItem in getProduct)
									{
										if (cout > 0)
										{
											var tempDetail = new OrderDetailsViewModels
											{
												ID = Guid.NewGuid().ToString(),
												AdminMoney = getInfoByProductType.Price * (commission / 100) + "",
												Price = getInfoByProductType.Price.ToString(),
												Dates = DateTime.Now,
												OrderBuyID = tempid,
												ProductID = productItem.Id,
												quantity = "1",
												SellerMoney = getInfoByProductType.Price - getInfoByProductType.Price * (commission / 100) + "",
												stasusPayment = "PAID",
												status = "ok"
											};
											var addDetail = await this._Detail.AddAsync(tempDetail);
											var temProductPaid = new ProductViewModels
											{
												Id = productItem.Id,
												Account = productItem.Account,
												CreatedDate = productItem.CreatedDate,
												ProductTypeId = productItem.ProductTypeId,
												Pwd = productItem.Pwd,
												Status = "PAID",
												StatusUpload = DateTime.Now.ToString(),
											};
											var updatePaidProduct = this._product.UpdateProduct(temProductPaid);
											cout--;
											await this._productType.UpdateQuantity(1, getInfoByProductType.Id);
										}
									}
									return new JsonResult(new { success = true, message = "/Purchase/OrderComplete" });
								}
								else
								{
									return new JsonResult(new { success = false, message = "Don Hang Hien Tai Khong Du" });
								}
							}
						}
					}
				}
				else
				{
					return new JsonResult(new { success = false, message = "loiiiiiiiiiiiii" });
				}
			}
			return new JsonResult(new { success = false, message = "Ban Phai Dang Nhap De Thu hien chuc nang nay " });
		}
		




	}
}
