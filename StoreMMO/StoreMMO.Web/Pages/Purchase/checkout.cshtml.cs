
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
                    foreach(var item in purchaseItems)
                    {

                        var getInfo = this._productType.GetInfoByProductid(item.ProductID);
                          if(getInfo.Count()> 0 ||  !getInfo.IsNullOrEmpty())
                        {
                               foreach(var intemPro in getInfo)
                            {
                                var tempid = Guid.NewGuid().ToString();
                                var tem = new OrderBuyViewModels
                                {
                                    ID = tempid,
                                    OrderCode = EncryptSupport.GenerateRandomString(10),
                                    ProductTypeId = item.ProductID,
                                    StoreID = intemPro.StoreID,
                                    UserID = checkUser,
                                    Status = "PAID",
                                    totalMoney = "" + purchaseItems.Sum(u => decimal.Parse(u.total))
                                };
                                var Buy = this._purchase.add(tem);
                                if (Buy)
                                {
                                    var getStoreDetail = this._storeDetails.GetAllStoreDetails();
                                      var detail = getStoreDetail.Where(u => u.StoreId == intemPro.StoreID).FirstOrDefault();
                                    if (detail != null)
                                    {
                                        var getComietsion = this._storeType.getByIdStoreType(detail.StoreTypeId);
                                        if(getComietsion != null)
                                        {
                                            var infoProduct = this._product.GetProductsByStoreId(intemPro.StoreID);
                                            if (infoProduct.Count() > 0 || !infoProduct.IsNullOrEmpty())
                                            {
                                                var getinfoProduct = this._product.getProductsByTypeID(intemPro.ProductTypeID);
                                                        if(getinfoProduct!= null)
                                                {
                                                    foreach (var product in infoProduct)
                                                    {
                                                          if(product.ProductTypeID == intemPro.ProductTypeID)
                                                        {
                                                            if (int.Parse(item.quantity) < int.Parse(product.Stock))
                                                            {

                                                                var getproduct = this._product.getAllProduct().Where(u => !u.Status.ToLower().Equals("PAID".ToLower()));
                                                                var count = int.Parse(item.quantity);
                                                                foreach (var product2 in getproduct)
                                                                {
                                                                    if (count > 0)
                                                                    {
                                                                        if (product.ProductTypeID == intemPro.ProductTypeID)
                                                                        {
                                                                            var temDetail = new OrderDetailsViewModels
                                                                            {
                                                                                ID = Guid.NewGuid().ToString(),
                                                                                AdminMoney = (product.Price * (getComietsion.Commission / 100)).ToString(),
                                                                                Dates = DateTime.Now,
                                                                                Price = product.Price.ToString(),
                                                                                OrderBuyID = tempid,
                                                                                SellerMoney = (product.Price - product.Price * (getComietsion.Commission / 100)).ToString(),
                                                                                ProductID = product2.Id,
                                                                                quantity = "1",
                                                                                stasusPayment = "PAID",
                                                                                status = "ok"
                                                                            };

                                                                            var addDetail = await this._Detail.AddAsync(temDetail);

                                                                            // Cập nhật trạng thái của sản phẩm
                                                                            product2.Status = "PAID";

                                                                            var update = new ProductViewModels
                                                                            {
                                                                                Account = product2.Account,
                                                                                CreatedDate = product2.CreatedDate,
                                                                                Id = product2.Id,
                                                                                ProductTypeId = product2.ProductTypeId,
                                                                                Pwd = product2.Pwd,
                                                                                Status = product2.Status,
                                                                                StatusUpload = product2.StatusUpload,
                                                                            };
                                                                            this._product.UpdateProduct(update); // Xóa [0] ở đây
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                      /*  var gettems = this._productType.getByIDProduct(product.ProductTypeID);
                                                           

                                                        var upDateStock = this._productType.Update(new ProductTypesViewModels { 
                                                        CreatedDate = gettems.CreatedDate,
                                                        Id = gettems.Id,
                                                        IsActive = gettems.IsActive,
                                                        Stock= int.Parse(gettems.Stock)-1 +"",
                                                        ModifiedDate = gettems.ModifiedDate,
                                                        Name = gettems.Name ,
                                                        Price = gettems.Price   
                                                        
                                                        });*/




                                                    }




                                                }
                                            }
                                        }             
                                    }
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
