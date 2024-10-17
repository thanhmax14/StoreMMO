using StoreMMO.Core.Repositories.Products;
using StoreMMO.Core.Repositories.RegisteredSeller;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.Core.RegisteredSeller
{
    public class RegisteredSellerService : IRegisteredSellerService
    {
        private readonly IRegisteredSellerRepository _registeredSellerRepository;

        public RegisteredSellerService(IRegisteredSellerRepository registeredSellerRepository)
        {
            _registeredSellerRepository = registeredSellerRepository;
        }
        public IEnumerable<UserViewModel> GetAllSellerUsersWithUserRole()
        {
            return _registeredSellerRepository.GetAllSellerUsersWithUserRole();
        }

        public UserViewModel UpdateSeller(UserViewModel inforAddViewModels)
        {
            return _registeredSellerRepository.UpdateSeller(inforAddViewModels);
        }
        public UserViewModel RejectSeller(UserViewModel inforAddViewModels)
        {
            return _registeredSellerRepository.RejectSeller(inforAddViewModels);
        }
    }
}
