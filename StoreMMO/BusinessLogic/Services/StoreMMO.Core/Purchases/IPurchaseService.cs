using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.Purchases
{
    public interface IPurchaseService
    {
        List<PurchaseItem> GetProductFromSession();
        void SaveProductToSession(List<PurchaseItem> product);
    }
}
