using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.User
{
    public interface IUserRepository
    {
        IEnumerable<UserViewModel> GetAllUser();
    }
}
