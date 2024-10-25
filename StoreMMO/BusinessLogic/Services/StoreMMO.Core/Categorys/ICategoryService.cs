using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Categorys
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModels> GetAll();
        CategoryViewModels getById(string id);

        CategoryViewModels AddCategory(CategoryViewModels category);
        CategoryViewModels UpdateCategory(CategoryViewModels category);

        void DeleteCategory(string id);
        IEnumerable<CategoryViewModels> GetCategoryIsActive();

        IEnumerable<CategoryViewModels> GetCategoryIsHidden();


    }
}
