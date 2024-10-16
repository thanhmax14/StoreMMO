using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.InfoAdds
{
    public interface IInfoAddRepository
    {
        IEnumerable<Models.Product> getAllInforAdd();
        InfoAddViewModels getByIdInforAdd(string id);
        InfoAddViewModels AddInforAdd(InfoAddViewModels inforAddViewModels);
        InfoAddViewModels UpdateInforAdd(InfoAddViewModels inforAddViewModels);
        void DeleteInforAdd(string id);
    }
}
