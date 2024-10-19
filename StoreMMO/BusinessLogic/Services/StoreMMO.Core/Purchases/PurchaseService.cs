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
