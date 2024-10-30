using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.WDF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StoreMMO.WDF.ViewModels
{
    public class HidentUserAccountViewModel : BaseViewModel
    {
        private readonly UserManager<AppUser> _userManager;
        public ObservableCollection<AppUserViewModel> Hidden { get; set; }
    
        public ICommand Hide { get; }


        public HidentUserAccountViewModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            Hidden = new ObservableCollection<AppUserViewModel>();
            InitializeAsync();
            Hide = new RelayCommand(Show1);

        }
        private async Task InitializeAsync()
        {
            await LoadDataAsync1();
        }
        private AppUserViewModel _SelectUser;
        public AppUserViewModel SelectUser
        {
            get { return _SelectUser; }
            set
            {
                _SelectUser = value;
                OnPropertyChanged(nameof(SelectUser));
                if (_SelectUser != null)
                {
                    FullNameInfo = _SelectUser.FullName;
                    CreateDateInfo = (DateTime)_SelectUser.CreatedDate.Value;
                    EmailInfo = _SelectUser.Email;
                    UserNameInfo = _SelectUser.UserName;
                    PhoneInfo = _SelectUser.PhoneNumber;
                    RoleInfo = _SelectUser.RoleName;

                }
            }
        }
        private string _roleInfo;
        public string RoleInfo
        {
            get => _roleInfo;
            set
            {
                if (_roleInfo != value)
                {
                    _roleInfo = value;
                    OnPropertyChanged(nameof(RoleInfo)); // Gọi sự kiện để thông báo rằng thuộc tính đã thay đổi
                    Debug.WriteLine($"RoleInfo updated to: {RoleInfo}"); // Ghi ra giá trị mới của RoleInfo
                }
            }
        }
        private string _FullNameInfo;
        public string FullNameInfo
        {
            get { return _FullNameInfo; }
            set
            {
                _FullNameInfo = value;
                OnPropertyChanged(nameof(FullNameInfo));
            }
        }
        private string _UserNameInfo;
        public string UserNameInfo
        {
            get { return _UserNameInfo; }
            set
            {
                _UserNameInfo = value;
                OnPropertyChanged(nameof(UserNameInfo));
            }
        }
        private string _EmailInfo;
        public string EmailInfo
        {
            get { return _EmailInfo; }
            set
            {
                _EmailInfo = value;
                OnPropertyChanged(nameof(EmailInfo));
            }
        }
        private DateTime _CreateDateInfo;
        public DateTime CreateDateInfo
        {
            get { return _CreateDateInfo; }
            set
            {
                _CreateDateInfo = value;
                OnPropertyChanged(nameof(CreateDateInfo));
            }
        }
        private string _PhoneInfo;
        public string PhoneInfo
        {
            get { return _PhoneInfo; }
            set
            {
                _PhoneInfo = value;
                OnPropertyChanged(nameof(PhoneInfo));
            }
        }

        public async Task LoadDataAsync1() // Phương thức bất đồng bộ để tải dữ liệu
        {
            var users = await _userManager.Users.ToListAsync(); // Sử dụng bất đồng bộ để lấy danh sách người dùng
            Hidden.Clear();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Lấy vai trò của người dùng
                var roleName = roles.FirstOrDefault(); // Lấy vai trò đầu tiên (nếu có)

                //Kiểm tra nếu vai trò là "Seller" và không phải là "Admin"
                if (roleName != null && (roleName.Equals("Seller", StringComparison.OrdinalIgnoreCase) || roleName.Equals("User", StringComparison.OrdinalIgnoreCase)))
                {
                    if (user.IsDelete == false)
                    {
                        var userWithRole = new AppUserViewModel
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            FullName = user.FullName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            CreatedDate = user.CreatedDate,
                            RoleName = roleName // Gán tên vai trò cho thuộc tính RoleName
                        };

                        Hidden.Add(userWithRole); // Thêm người dùng với vai trò Seller vào danh sách
                    }

                }
            }
        }
        
        private async void Show1(object parameter)
        {
            if (SelectUser == null)
            {
                MessageBox.Show("Please select a User to hide.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tìm người dùng theo ID
            var userToUpdate = await _userManager.FindByIdAsync(SelectUser.Id);
            if (userToUpdate != null)
            {
                // Đánh dấu người dùng là ẩn
                userToUpdate.IsDelete = true;

                // Lưu thay đổi vào cơ sở dữ liệu
                var result = await _userManager.UpdateAsync(userToUpdate);
                if (result.Succeeded)
                {
                    LoadDataAsync1(); // Tải lại danh sách người dùng
                    MessageBox.Show("User hidden successfully!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Hiển thị thông báo lỗi nếu không thành công
                    var errorMessages = string.Join("\n", result.Errors.Select(e => e.Description));
                    MessageBox.Show($"Update failed:\n{errorMessages}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("User not found.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
