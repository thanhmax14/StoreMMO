using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.Store
{
    public interface IStoreRepository
    {
        IEnumerable<StoreViewModels> getAll();
    }
}
