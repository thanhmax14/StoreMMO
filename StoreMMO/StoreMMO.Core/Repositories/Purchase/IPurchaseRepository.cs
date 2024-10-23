using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Purchase
{
    public interface IPurchaseRepository
    {
        List<PurchaseItem> GetProductFromSession();
        void SaveProductToSession(List<PurchaseItem>  product);
        bool add(OrderBuyViewModels orderBuyViewModels);
		bool Delete(OrderBuyViewModels orderBuyViewModels);
		bool Edit(OrderBuyViewModels orderBuyViewModels);
        OrderBuyViewModels GetByID(string  id);
        IEnumerable<OrderBuyViewModels> GetAll();
        IEnumerable<OrderBuyViewModels> GetByUserID(string userID);

    }
}
