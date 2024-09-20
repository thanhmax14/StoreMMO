using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMMO.Services.Store;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = this._storeService.getAll();
            return Ok(list);
        }















    }

    
}
