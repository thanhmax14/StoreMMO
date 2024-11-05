using Microsoft.Data.SqlClient;
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
        public IEnumerable<UserViewModel> GetAllUser(bool isDelete)
        {


            string sql = $"SELECT       Users.Id as UserID, Users.FullName, Users.DateOfBirth, Users.Address, Users.IsSeller, Users.UserName, Users.Email, Users.PhoneNumber, Roles.Name as RoleName,Users.PasswordHash, Users.CreatedDate FROM            Users INNER JOIN                         UserRoles ON Users.Id = UserRoles.UserId INNER JOIN                     Roles ON UserRoles.RoleId = Roles.Id where Users.IsDelete = '{isDelete}'  and  Roles.Name != 'admin'";

            var list = this._context.Database.SqlQueryRaw<UserViewModel>(sql).ToList();
            return list;
        }

        public IEnumerable<UserViewModel> GetlUserById(string userId)
        {
            string sql = $"SELECT       Users.Id as UserID, Users.FullName, Users.DateOfBirth, Users.Address, Users.IsSeller, Users.UserName, Users.Email, Users.PhoneNumber, Roles.Name as RoleName,Users.PasswordHash, Users.CreatedDate FROM            Users INNER JOIN                         UserRoles ON Users.Id = UserRoles.UserId INNER JOIN                     Roles ON UserRoles.RoleId = Roles.Id where users.Id ='{userId}'";


            var list = this._context.Database.SqlQueryRaw<UserViewModel>(sql).ToList();
            return list;
        }

        public IEnumerable<getTotalSeller> getNumberBuy(string userId)
        {
            // Truy vấn SQL với tham số
            string sql = @"
        SELECT 
            (SELECT SUM(CAST(od.quantity AS int)) 
             FROM OrderBuys ob 
             JOIN OrderDetails od ON ob.ID = od.OrderBuyID
             WHERE ob.UserID = @UserId) AS totalBuy,

            (SELECT COUNT(*) 
             FROM Stores 
             WHERE Stores.IsAccept='1' and UserId = @UserId) AS totalStore,

            (SELECT SUM(CAST(od.quantity AS int)) 
             FROM OrderBuys ob 
             JOIN OrderDetails od ON ob.ID = od.OrderBuyID
             JOIN Stores s ON s.Id = ob.StoreID
             WHERE s.UserId = @UserId) AS totalSold;";

            // Thực thi truy vấn và truyền tham số
            var list = this._context.Database.SqlQueryRaw<getTotalSeller>(sql, new SqlParameter("@UserId", userId)).ToList();
            return list;
        }

    }
}
