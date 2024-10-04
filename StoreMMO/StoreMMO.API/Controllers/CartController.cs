using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMMO.API.Services;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{id}")]
        public IActionResult getByIDCart(string id)
        {
            try
            {
                var p = _cartService.getByIdCart(id);
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Not found IDhhhhhhhhhhhhhhhhhhhh",
                    error = ex.Message,
                });
            }
        }
        [HttpGet]
        public IActionResult getAllCart()
        {
            var list = _cartService.getAllCart();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddCart(CartViewModels cart)
        {
            try
            {
                // Kiểm tra ModelState hợp lệ
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Kiểm tra nếu đã tồn tại giỏ hàng với Id này
                var obj = _cartService.getByIdCart(cart.Id);
                if (obj != null && obj.Id == cart.Id)
                {
                    return BadRequest(new
                    {
                        message = "Error occurred: Cart with this Id already exists."
                    });
                }

                // Thêm giỏ hàng mới
                _cartService.Add(cart);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                // Bắt lỗi và trả về phản hồi dạng JSON
                return StatusCode(500, new
                {
                    message = "Please enter full infomration.",
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        public IActionResult UpdateCart(CartViewModels cart)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not cannot Update");
                }
                _cartService.UpdateCart(cart);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Please enter full infomration.",
                    error = ex.Message
                });
            }
        }
        [HttpDelete]
        public IActionResult DeleteCart(string id)
        {
            _cartService.DeleteCart(id);
            return Ok(id);
        }
    }
}
