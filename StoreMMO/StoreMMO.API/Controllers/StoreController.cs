using BusinessLogic.Services.StoreMMO.Core.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System.Runtime.InteropServices;

namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            this._storeService = storeService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(string sicbo)
        {
            var list = this._storeService.getAll(sicbo);
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddStore(StoreAddViewModels store)
        {
            
            if(store == null)
            {
                return BadRequest("No add store");
            } else
            {
                _storeService.AddStore(store);
            }
            return Ok(store);

        }
        [HttpPut] 
        public IActionResult EditStore(StoreAddViewModels store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            _storeService.Update(store);
            return Ok(store);
        }
        [HttpDelete]
        public IActionResult DeleteStore(string id)
        {
            
            _storeService.Delete(id);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id) {
            var store = _storeService.getById(id);

            if (store == null)
            {
                return NotFound("Store not found with the given ID");
            }
            // Trả về đối tượng StoreAddViewModels dưới dạng JSON
            return Ok(store);
        }
        [HttpGet("detail/{id}", Name = "GetStoreDetail")]
        public IActionResult GetStoreDetail(string id)
        {
            var list = this._storeService.getStorDetailFullInfo(id);
            
            return list.IsNullOrEmpty()?BadRequest("Khong tim thay danh sach"):Ok(list);
        }
    }


}
