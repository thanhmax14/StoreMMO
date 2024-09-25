using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.StoreTypes
{
    public class StoreTypeRepository : IStoreTypeRepository
    {
        private readonly AppDbContext _context;

        public StoreTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public StoreTypeViewModels AddStoreType(StoreTypeViewModels storeViewModels)
        {
            var viewModel = new StoreType
            {
                Id = storeViewModels.Id,
                Name = storeViewModels.Name,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = storeViewModels.ModifiedDate,
            };
            _context.StoreTypes.Add(viewModel);
            _context.SaveChanges();
            return storeViewModels;
        }

        public void deleteByIdStoreType(string id)
        {
           var findId = _context.StoreTypes.SingleOrDefault(x => x.Id == id);
            if(findId == null)
            {
                throw new Exception("Not found ID");
            }
            _context.StoreTypes.Remove(findId);
            _context.SaveChanges();
        }

        public IEnumerable<StoreType> getAllStoreType()
        {
           var list = _context.StoreTypes.ToList();
            return list;
        }

        public StoreTypeViewModels getByIdStoreType(string id)
        {
            var findID = _context.StoreTypes.SingleOrDefault(x => x.Id == id);
            if(findID == null)
            {
                throw new Exception("Not found ID");

            }
            var viewModel = new StoreTypeViewModels
            {
                Id = findID.Id,
                Name = findID.Name,
                CreatedDate = findID.CreatedDate,
                ModifiedDate = findID.ModifiedDate,
            };
            return viewModel;
        }

        public StoreTypeViewModels UpdateStoreType(StoreTypeViewModels storeViewModels)
        {
            var viewModel = new StoreType
            {
                Id = storeViewModels.Id,
                Name = storeViewModels.Name,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = storeViewModels.ModifiedDate,
            };
            _context.StoreTypes.Update(viewModel);
            _context.SaveChanges();
            return storeViewModels;
        }
    }
}
