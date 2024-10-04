using StoreMMO.Core.Repositories.User;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Services.StoreMMO.Core
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public IEnumerable<UserViewModel> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }
    }
}
