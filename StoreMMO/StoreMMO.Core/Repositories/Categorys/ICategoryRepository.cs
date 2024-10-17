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
        IEnumerable<Category> getAll();
       CategoryViewModels getById(string id);

        CategoryViewModels Add(CategoryViewModels a);
        CategoryViewModels Update(CategoryViewModels a);

        void Delete(string id);

        IEnumerable<CategoryViewModels> GetCategoryIsActive();
     //   CategoryViewModels UpdateName(CategoryViewModels category);

    }
}
