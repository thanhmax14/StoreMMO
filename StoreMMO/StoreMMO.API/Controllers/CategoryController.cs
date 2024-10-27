using BusinessLogic.Services.StoreMMO.Core.Categorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{id}")]
        public IActionResult getById(string id)
        {
            var obj = _categoryService.getByIdCategory(id);
            if (obj == null)
            {
                return BadRequest("Not found Id");
            }
            return Ok(obj);
        }
        [HttpGet]
        public IActionResult getAllCategory()
        {
            var list = _categoryService.GetAll();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryViewModels cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No cannot add category success!");
            }
            _categoryService.AddCategory(cate);
            return Ok(cate);
        }
        [HttpPut]
        public IActionResult EditCategory(CategoryViewModels cate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not cannot edit category");
            }
            _categoryService.UpdateCategory(cate);
            return Ok(cate);
        }
        [HttpDelete]
        public IActionResult DeleteCategory(string id)
        {
            _categoryService.DeleteCategory(id);
            return Ok(id);
        }
    }
}
