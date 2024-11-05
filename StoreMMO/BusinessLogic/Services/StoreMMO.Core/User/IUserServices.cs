using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.Core.User
{
    public interface IUserServices
    {
        IEnumerable<UserViewModel> GetAllUser(bool isDelete);
        IEnumerable<UserViewModel> GetUserById(string userId);
        IEnumerable<getTotalSeller> getNumberBuy(string userId);
    }
}
