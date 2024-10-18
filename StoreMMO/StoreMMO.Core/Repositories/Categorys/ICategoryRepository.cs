using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Categorys
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryViewModels> GetAll();
        CategoryViewModels getById(string id);
        CategoryViewModels Add(CategoryViewModels a);
        CategoryViewModels Update(CategoryViewModels a);

        void Delete(string id);

        IEnumerable<CategoryViewModels> GetCategoryIsActive();
        IEnumerable<CategoryViewModels> GetCategoryIsHidden();
        //   CategoryViewModels UpdateName(CategoryViewModels category);

    }
}
