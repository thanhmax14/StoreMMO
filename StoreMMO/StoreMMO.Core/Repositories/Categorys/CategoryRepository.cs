using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Categorys
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public CategoryViewModels Add(CategoryViewModels category)
        {
            var AddCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
                Commission = category.Commission,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = category.ModifiedDate,
            };
            _context.Categories.Add(AddCategory);
            _context.SaveChanges();

            return category;
        }

        public void Delete(string id)
        {
            var findId = _context.Categories.FirstOrDefault(x => x.Id == id);
            if(findId == null)
            {
                throw new Exception("Id not found");
            }
            _context.Categories.Remove(findId);
            _context.SaveChanges();
        }

        public IEnumerable<Category> getAll()
        {
            var list = _context.Categories.ToList();
            return list;
        }

        public CategoryViewModels getById(string id)
        {
            var findId = _context.Categories.SingleOrDefault(x => x.Id == id);
            if(findId == null)
            {
                throw new Exception("Id not found");
            }
            var category = new CategoryViewModels
            {
                Id = findId.Id,
                Name = findId.Name,
                Commission = findId.Commission,
                CreatedDate = findId.CreatedDate,
                ModifiedDate = findId.ModifiedDate,
            };
            return category;
        }

        public CategoryViewModels Update(CategoryViewModels category)
        {
            var updateCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
                Commission = category.Commission,
                CreatedDate = category.CreatedDate,
                ModifiedDate = DateTime.UtcNow,
            };
            _context.Categories.Update(updateCategory);
            _context.SaveChanges();
            return category;
        }
    }
}
