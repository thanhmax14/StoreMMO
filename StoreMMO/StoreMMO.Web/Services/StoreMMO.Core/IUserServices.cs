using StoreMMO.Core.ViewModels;

namespace StoreMMO.Web.Services.StoreMMO.Core
{
    public interface IUserServices
    {
        IEnumerable<UserViewModel> GetAllUser();
    }
}
