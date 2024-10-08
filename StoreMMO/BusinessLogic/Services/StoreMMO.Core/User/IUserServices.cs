using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.User
{
    public interface IUserServices
    {
        IEnumerable<UserViewModel> GetAllUser();
    }
}
