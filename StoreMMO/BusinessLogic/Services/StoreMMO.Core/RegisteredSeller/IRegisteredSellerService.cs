using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.RegisteredSeller
{
    public interface IRegisteredSellerService
    {
        IEnumerable<UserViewModel> GetAllSellerUsersWithUserRole();
        UserViewModel UpdateSeller(UserViewModel inforAddViewModels);
    }
}
