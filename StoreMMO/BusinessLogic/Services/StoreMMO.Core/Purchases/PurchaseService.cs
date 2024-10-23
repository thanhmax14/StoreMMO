using StoreMMO.Core.Repositories.Purchase;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Purchases
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchase;
        public PurchaseService(IPurchaseRepository purchase)

        {
            this._purchase = purchase;
        }

        public bool add(OrderBuyViewModels orderBuyViewModels)
        {
           return this._purchase.add(orderBuyViewModels);
        }

        public bool Delete(OrderBuyViewModels orderBuyViewModels)
        {
            return this._purchase.Delete(orderBuyViewModels);
        }

        public bool Edit(OrderBuyViewModels orderBuyViewModels)
        {
            return this._purchase.Edit(orderBuyViewModels);
        }

		public IEnumerable<OrderBuyViewModels> GetAll()
		{
			return this._purchase.GetAll();
		}

		public OrderBuyViewModels GetByID(string id)
        {
            return this._purchase.GetByID(id);
        }

		public IEnumerable<OrderBuyViewModels> GetByUserID(string userID)
		{
			return this._purchase.GetByUserID(userID);
		}

		public List<PurchaseItem> GetProductFromSession()
        {
            return this._purchase.GetProductFromSession();
        }

        public void SaveProductToSession(List<PurchaseItem> product)
        {
            this._purchase.SaveProductToSession(product);
        }
    }
}
