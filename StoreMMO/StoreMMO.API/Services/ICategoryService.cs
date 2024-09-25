using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.API.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> getAllCategory();
        CategoryViewModels getByIdCategory(string id);

        CategoryViewModels AddCategory(CategoryViewModels category);
        CategoryViewModels UpdateCategory(CategoryViewModels category);

        void DeleteCategory(string id);

    }
}
