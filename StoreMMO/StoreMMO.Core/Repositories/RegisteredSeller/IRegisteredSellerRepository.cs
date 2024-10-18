using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.RegisteredSeller
{
    public interface IRegisteredSellerRepository
    {
        IEnumerable<UserViewModel> GetAllSellerUsersWithUserRole();
        UserViewModel UpdateSeller(UserViewModel inforAddViewModels);
        UserViewModel RejectSeller(UserViewModel inforAddViewModels);
    }
}
