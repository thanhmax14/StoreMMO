using BusinessLogic.Services.StoreMMO.Core.WishLists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMMO.Core.Repositories.WishLists;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishListsService _sever;
        public WishlistController(IWishListsService s)
        {
            this._sever = s;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = this._sever.getAllWishList();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddWishList(WishListViewModels s )
        {

            if (s == null)
            {
                return BadRequest("No add store");
            }
            else
            {

                this._sever.AddWishList(s);
            }
            return Ok(s);

        }
        [HttpPut]
        public IActionResult Edit(string id, [FromBody] WishListViewModels store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            // Tìm đối tượng wishlist theo id
            var wishList = this._sever.getByIDWishList(id);
            if (wishList == null)
            {
                return NotFound("Wishlist not found");
            }

            // Cập nhật wishlist với dữ liệu mới
            this._sever.UpdateWishList(store);
            return Ok(store);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var check = this._sever.getByIDWishList(id);
            if (check == null)
            {
                return NotFound("No wishlists found for ID");
            }
            this._sever.DeleteWishList(id);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var store = this._sever.getByIDWishList(id);

            if (store == null)
            {
                return NotFound("Store not found with the given ID");
            }
            // Trả về đối tượng StoreAddViewModels dưới dạng JSON
            return Ok(store);
        }
        [HttpGet("GetByUserID/{userId}", Name = "GetByUserID")]
        public IActionResult GetByUser(string userId)
        {
            var find = this._sever.getAllByUserID(userId);
            if (find == null || !find.Any())
            {
                // Trả về một danh sách rỗng với mã 200 OK thay vì 404
                return Ok(new List<WishListViewModels>());
            }

            return Ok(find); // Trả về danh sách wishlist nếu có
        }

    }



}

