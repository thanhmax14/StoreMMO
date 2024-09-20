using StoreMMO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Carts
{
    public interface ICartRepository 
    {
        IEnumerable<Cart> getAll();
        Cart Add(Cart cart);
        Cart Update(Cart cart);
        Cart Delete(string id);
        Cart getById(string id);
    }
}
