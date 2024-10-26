
using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.StoreMMO.Core.OrderDetails;
using BusinessLogic.Services.StoreMMO.Core.Products;
using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using BusinessLogic.Services.StoreMMO.Core.StoreDetails;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Pages.Purchase
{
    public class checkoutModel : PageModel
    {
        private readonly IPurchaseService _purchase;
        private readonly IProductTypeService _productType;
        private readonly IStoreTypeService _storeType;
        private readonly IProductService _product;
        private readonly IStoreDetailsService _storeDetails;
        private readonly IOderDetailsService _Detail;
 


        public checkoutModel(IPurchaseService purchase, IProductTypeService productService, IStoreTypeService typeService,
            IProductService product,IStoreDetailsService storeDetails, IOderDetailsService oderDetails
            )
        {
            this._purchase = purchase;
            this._productType = productService;
            this._storeType = typeService;
            this._product = product;
            this._storeDetails = storeDetails;
            this._Detail = oderDetails;
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
        public async Task<IActionResult> OnPostAsync()
        {

            var checkUser = HttpContext.Session.GetString("UserID");
            checkUser = "5921c651-d855-408a-9f37-e10405250f63";
            if (checkUser != null )
            {
                purchaseItems = this._purchase.GetProductFromSession();
                if (purchaseItems.Count > 0 || !purchaseItems.IsNullOrEmpty())
                {
                    var commission = 0.0;
                    foreach(var item in purchaseItems)
                    {

                        var getInfo = this._productType.GetInfoByProductid(item.ProductID);
                          if(getInfo.Count()> 0 ||  !getInfo.IsNullOrEmpty())
                        {

                            var tempid = Guid.NewGuid().ToString();
                            bool Buy = false;
                            var productTypeTem = "";
                            foreach (var intemPro in getInfo)
                            {
                                productTypeTem= item.ProductID;
                                 var tem = new OrderBuyViewModels
                                {
                                    ID = tempid,
                                    OrderCode = EncryptSupport.GenerateRandomString(10),
                                    ProductTypeId = productTypeTem,
                                    StoreID = intemPro.StoreID,
                                    UserID = checkUser,
                                    Status = "PAID",
                                    totalMoney = "" + purchaseItems.Sum(u => decimal.Parse(u.total))
                                };
                                commission = this._storeType.GetCommitssionByStoreID(intemPro.StoreID);
                                Buy = this._purchase.add(tem);
                                break;
                            }
                            var getInfoByProductType = this._productType.getByIDProduct(productTypeTem);
                            if (getInfoByProductType != null)
                            {
                                if(int.Parse(item.quantity) < int.Parse(getInfoByProductType.Stock))
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
                                                AdminMoney = getInfoByProductType.Price * (commission/100) +"",
                                                Price = getInfoByProductType.Price.ToString(),
                                                Dates = DateTime.Now,
                                                OrderBuyID = tempid,
                                                ProductID = productItem.Id,
                                                quantity = "1",
                                                SellerMoney = getInfoByProductType.Price - getInfoByProductType.Price * (commission / 100)+"",
                                                stasusPayment = "PAID",
                                                status = "ok"
                                            };
                                            var addDetail = await this._Detail.AddAsync(tempDetail);
                                            var temProductPaid = new ProductViewModels
                                            {
                                                Id = productItem.Id,
                                                Account =productItem.Account,
                                                CreatedDate= productItem.CreatedDate,
                                                ProductTypeId = productItem.ProductTypeId,
                                                Pwd =productItem.Pwd,
                                                Status = "PAID",
                                                StatusUpload = DateTime.Now.ToString()
                                            };
                                            var updatePaidProduct = this._product.UpdateProduct(temProductPaid);
                                            cout--; 
                                        }
                                    }
                                    return Redirect("/Purchase/OrderComplete");
                                }
                            }








                        }
                    }
                }
            }
            return NotFound();
        }
    }
}
