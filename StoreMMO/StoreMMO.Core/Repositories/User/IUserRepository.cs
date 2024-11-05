using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.User
{
    public interface IUserRepository
    {
        IEnumerable<UserViewModel> GetAllUser(bool isDelete);
        IEnumerable<UserViewModel> GetlUserById(string userId);
        IEnumerable<getTotalSeller>  getNumberBuy(string userId);
       
    }
}
