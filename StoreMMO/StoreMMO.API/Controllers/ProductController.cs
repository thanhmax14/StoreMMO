using BusinessLogic.Services.StoreMMO.Core.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductController(IProductsService productsService)
        {
             this._productsService = productsService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var product = this._productsService.getByIDProduct(id);
            if(product == null)
            {
                return BadRequest("Cant not find product with this id!!");

            }
            else
            {
                return Ok((product));
            }
        }


    }
}
