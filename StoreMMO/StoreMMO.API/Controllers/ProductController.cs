using BusinessLogic.Services.StoreMMO.Core.ProductTypes;
using BusinessLogic.Services.StoreMMO.Core.WishLists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMMO.Core.Models;


namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductTypeService _productsService;
        private readonly IWishListsService _wishListsService;
        public ProductController(IProductTypeService productsService, IWishListsService wishListsService)
        {
             this._productsService = productsService;
            this._wishListsService = wishListsService;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // Kiểm tra xem ID có chứa ký tự '/'
            if (id.Contains("$"))
            {
                var product = this._productsService.getByIDProduct(id.Split("$")[0]);
                var userid = id.Split("$")[1];
                var user = this._wishListsService.getAllByUserID(userid);
                if (user == null)
                {
                    return Ok(new { thanhdeptrai = false, mess="User not found"});
                }
                else
                {
                    var checkexit = user.Any(a => a.ProductId == id.Split("$")[0]);
                    if (checkexit)
                    {
                      
                        if (product == null)
                        {
                            return NotFound("Cannot find product with this ID!");
                        }
                        return Ok(new { product.Price, product.Id, product.CreatedDate, product.ModifiedDate,product.Name,product.Stock,
                            
                            thanhdeptrai = true });

                    }
                }
                return Ok(new
                {
                    product.Price,
                    product.Id,
                    product.CreatedDate,
                    product.ModifiedDate,
                    product.Name,
                    product.Stock,

                    thanhdeptrai = false
                });
            }
            else
            {
                var product = this._productsService.getByIDProduct(id);
                if (product == null)
                {
                    return NotFound("Cannot find product with this ID!");
                }

                return Ok(product);
            }


        }
    }
}
