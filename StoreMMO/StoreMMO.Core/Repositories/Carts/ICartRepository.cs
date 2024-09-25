using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Carts
{
    public interface ICartRepository 
    {
        IEnumerable<Cart> getAll();
        CartViewModels Add(CartViewModels cart);
        CartViewModels Update(CartViewModels cart);
        void Delete(string id);
        CartViewModels getById(string id);
    }
}
