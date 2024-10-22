using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.StoreDetails
{
    public interface IStoreDetailRepository
    {
        IEnumerable<StoreDetail> GetAllStoreDetails();
        StoreDetailViewModels GetByIdStoDetails(string id);
        StoreDetailViewModels AddStoDetails(StoreDetailViewModels storeDetailViewModels);
        SaleHistoryViewModels UpdateStoDetails(SaleHistoryViewModels idstoreDetailViewModels);
        void DeleteStoDetails(string id);
        StoreDetailViewModels GetByIdStoDetails1(string id);
    }
}
