using StoreMMO.Core.Models;
using StoreMMO.Core.Repositories.StoreDetails;
using StoreMMO.Core.Repositories.Stores;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.StoreDetails
{
    public class StoreDetailRepository : IStoreDetailRepository
    {
        private readonly AppDbContext _context;

        public StoreDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public StoreDetailViewModels AddStoDetails(StoreDetailViewModels storeDetailViewModels)
        {
            var viewModel = new StoreDetail
            {
                Id = storeDetailViewModels.Id,
                StoreId = storeDetailViewModels.StoreId,
                CategoryId = storeDetailViewModels.CategoryId,
                StoreTypeId = storeDetailViewModels.StoreTypeId,
                Name = storeDetailViewModels.Name,
                SubDescription = storeDetailViewModels.SubDescription,
                DescriptionDetail = storeDetailViewModels.DescriptionDetail,
                Img = storeDetailViewModels.Img,
                CreatedDate = DateTime.Now,
                ModifiedDate = storeDetailViewModels.CreatedDate,
                IsActive = storeDetailViewModels.IsActive,
            };
            _context.StoreDetails.Add(viewModel);
            _context.SaveChanges();
            return storeDetailViewModels;
        }

        public void DeleteStoDetails(string id)
        {
            var findID = _context.StoreDetails.SingleOrDefault(x => x.Id == id);
            if(findID == null)
            {
                throw new Exception("Not found ID");
            }
            _context.StoreDetails.Remove(findID);
            _context.SaveChanges();  
        }

        public IEnumerable<StoreDetail> GetAllStoreDetails()
        {
           var list = _context.StoreDetails.ToList();   
            return list;
        }

        public StoreDetailViewModels GetByIdStoDetails(string id)
        {
            var findID = _context.StoreDetails.FirstOrDefault(x => x.Id == id);
            if (findID == null)
            {
                throw new Exception("Not found ID");
            }
            var viewModel = new StoreDetailViewModels
            {
                Id = findID.Id,
                StoreId = findID.StoreId,
                CategoryId = findID.CategoryId,
                StoreTypeId = findID.StoreTypeId,
                Name = findID.Name,
                SubDescription = findID.SubDescription,
                DescriptionDetail = findID.DescriptionDetail,
                Img = findID.Img,
                CreatedDate = DateTime.Now,
                ModifiedDate = findID.CreatedDate,
                IsActive = findID.IsActive,
            };
            return viewModel;
        }

        public StoreDetailViewModels UpdateStoDetails(StoreDetailViewModels storeDetailViewModels)
        {
            var viewModel = new StoreDetail
            {
                Id = storeDetailViewModels.Id,
                StoreId = storeDetailViewModels.StoreId,
                CategoryId = storeDetailViewModels.CategoryId,
                StoreTypeId = storeDetailViewModels.StoreTypeId,
                Name = storeDetailViewModels.Name,
                SubDescription = storeDetailViewModels.SubDescription,
                DescriptionDetail = storeDetailViewModels.DescriptionDetail,
                Img = storeDetailViewModels.Img,
                CreatedDate = storeDetailViewModels.CreatedDate,
                ModifiedDate = DateTime.UtcNow,
                IsActive = storeDetailViewModels.IsActive,
            };
            _context.StoreDetails.Update(viewModel);
            _context.SaveChanges();
            return storeDetailViewModels;
        }
    }
}
