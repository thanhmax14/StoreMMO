using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.StoreDetails
{
    public interface IStoreDetailsService
    {
        IEnumerable<StoreDetail> GetAllStoreDetails();
        StoreDetailViewModels GetByIdStoDetails(string id);
        StoreDetailViewModels AddStoDetails(StoreDetailViewModels storeDetailViewModels);
        StoreDetailViewModels UpdateStoDetails(StoreDetailViewModels idstoreDetailViewModels);
        void DeleteStoDetails(string id);
        StoreDetailViewModels GetByIdStoDetails1(string id);

    }
}
