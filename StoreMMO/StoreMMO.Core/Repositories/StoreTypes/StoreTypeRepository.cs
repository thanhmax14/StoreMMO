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
                Commission = storeViewModels.Commission,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = storeViewModels.ModifiedDate,
                IsActive = true,
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
                Commission = findID.Commission,
                CreatedDate = findID.CreatedDate,
                ModifiedDate = findID.ModifiedDate,
            };
            return viewModel;
        }

        public StoreTypeViewModels UpdateStoreType(StoreTypeViewModels storeViewModels)
        {
            // Tìm đối tượng StoreType bằng Id
            var findStoreType = _context.StoreTypes.FirstOrDefault(x => x.Id == storeViewModels.Id);

            if (findStoreType != null)
            {
                // Cập nhật các trường dữ liệu từ ViewModel
                findStoreType.Name = storeViewModels.Name;
                findStoreType.Commission = storeViewModels.Commission;  // Nếu có trường này
                findStoreType.ModifiedDate = DateTime.UtcNow;
                findStoreType.IsActive = storeViewModels.IsActive;

                // Lưu thay đổi vào database
                _context.SaveChanges();

                // Cập nhật lại thông tin vào ViewModel để trả về
                storeViewModels.Id = findStoreType.Id;
                storeViewModels.CreatedDate = findStoreType.CreatedDate;
                storeViewModels.ModifiedDate = findStoreType.ModifiedDate;
                storeViewModels.IsActive = findStoreType.IsActive;
            }

            return storeViewModels;
        }

        public IEnumerable<StoreTypeViewModels> GetStoreTypeIsActive()
        {
            var list = _context.StoreTypes.Where(x => x.IsActive == true).Select(x => new StoreTypeViewModels
            {
                Id = x.Id,
                Name = x.Name,
                Commission = x.Commission,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
            }).ToList();
            return list;
        }
    }
}
