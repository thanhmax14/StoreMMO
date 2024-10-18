using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.Categorys;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.Categorys
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryViewModels AddCategory(CategoryViewModels category)
        {
            return _categoryRepository.Add(category);
        }

        public void DeleteCategory(string id)
        {
            _categoryRepository.Delete(id);
        }

        public IEnumerable<CategoryViewModels> GetAll()
        {
           return _categoryRepository.GetAll();
        }

        public CategoryViewModels getByIdCategory(string id)
        {
            return _categoryRepository.getById(id);
        }

        public IEnumerable<CategoryViewModels> GetCategoryIsActive()
        {
          return _categoryRepository.GetCategoryIsActive();
        }


        public IEnumerable<CategoryViewModels> GetCategoryIsHidden()
        {
            return _categoryRepository.GetCategoryIsHidden();
        }

        public CategoryViewModels UpdateCategory(CategoryViewModels category)
        {
            return _categoryRepository.Update(category);
        }
    }
}
