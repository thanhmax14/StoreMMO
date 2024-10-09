using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;

namespace StoreMMO.Core.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<UserViewModel> GetAllUser()
        {
            string sql = "SELECT       Users.Id as UserID, Users.FullName, Users.DateOfBirth, Users.Address, Users.IsSeller, Users.UserName, Users.Email, Users.PhoneNumber, Roles.Name as RoleName,Users.PasswordHash, Users.CreatedDate FROM            Users INNER JOIN                         UserRoles ON Users.Id = UserRoles.UserId INNER JOIN                     Roles ON UserRoles.RoleId = Roles.Id";


            var list = this._context.Database.SqlQueryRaw<UserViewModel>(sql).ToList();
            return list;
        }
    }
}
