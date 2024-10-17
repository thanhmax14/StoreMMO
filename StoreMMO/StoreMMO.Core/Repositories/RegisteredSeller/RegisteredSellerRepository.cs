using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Repositories.RegisteredSeller
{
    public class RegisteredSellerRepository : IRegisteredSellerRepository
    {
        private readonly AppDbContext _context;
        public RegisteredSellerRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<UserViewModel> GetAllSellerUsersWithUserRole()
        {
            string sql = @"SELECT 
                        Users.Id as UserID, 
                        Users.FullName, 
                        Users.DateOfBirth, 
                        Users.Address, 
                        Users.IsSeller, 
                        Users.UserName, 
                        Users.Email, 
                        Users.PhoneNumber, 
                        Roles.Name as RoleName, 
                        Users.PasswordHash, 
                        Users.CreatedDate
                   FROM Users
                   INNER JOIN UserRoles ON Users.Id = UserRoles.UserId
                   INNER JOIN Roles ON UserRoles.RoleId = Roles.Id
                   WHERE Users.IsSeller = 1 AND Roles.Name = 'User'";

            var list = this._context.Database.SqlQueryRaw<UserViewModel>(sql).ToList();
            return list;
        }
        public UserViewModel UpdateSeller(UserViewModel inforAddViewModels)
        {
            // Truy vấn SQL để cập nhật IsSeller = 0 trong bảng Users
            string updateIsSellerSql = @"UPDATE [dbo].[Users] 
                                 SET IsSeller = 0 
                                 WHERE Id = @UserId";

            // Thực thi truy vấn cập nhật IsSeller
            _context.Database.ExecuteSqlRaw(updateIsSellerSql, new SqlParameter("@UserId", inforAddViewModels.UserID));

            // Truy vấn SQL để cập nhật role của user thành "seller"
            string updateUserRoleSql = @"
        IF EXISTS (SELECT 1 FROM [dbo].[UserRoles] WHERE UserId = @UserId)
        BEGIN
            UPDATE [dbo].[UserRoles] 
            SET RoleId = (SELECT Id FROM [dbo].[Roles] WHERE Name = 'Seller') 
            WHERE UserId = @UserId
        END
        ELSE
        BEGIN
            INSERT INTO [dbo].[UserRoles] (UserId, RoleId) 
            VALUES (@UserId, (SELECT Id FROM [dbo].[Roles] WHERE Name = 'Seller'))
        END";

            // Thực thi truy vấn để cập nhật hoặc thêm vai trò "seller" cho người dùng
            _context.Database.ExecuteSqlRaw(updateUserRoleSql, new SqlParameter("@UserId", inforAddViewModels.UserID));

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return inforAddViewModels;
        }
    }
}
