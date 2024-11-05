using StoreMMO.Core.Repositories.User;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.User
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public IEnumerable<UserViewModel> GetAllUser(bool isDelete)
        {
            return _userRepository.GetAllUser(isDelete);
        }

        public IEnumerable<UserViewModel> GetUserById(string userId)
        {
            return _userRepository.GetlUserById(userId);
        }

        public IEnumerable<getTotalSeller> getNumberBuy(string userId)
        {
          return this._userRepository.getNumberBuy(userId);
        }

    }
}
