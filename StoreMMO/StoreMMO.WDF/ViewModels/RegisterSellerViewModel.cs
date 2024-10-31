using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.WDF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StoreMMO.WDF.ViewModels
{
    public class RegisterSellerViewModel : BaseViewModel
    {
        private readonly UserManager<AppUser> _userManager;
        public ObservableCollection<AppUserViewModel> UserViewModels { get; set; }

        public ICommand Update { get; }
        public ICommand Reject1 { get; }


        public RegisterSellerViewModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            UserViewModels = new ObservableCollection<AppUserViewModel>();
            InitializeAsync();
            Update = new RelayCommand(UpdateSeller);
            Reject1 = new RelayCommand(Reject);

        }
        private async Task InitializeAsync()
        {
            await LoadData();
        }
        public async Task LoadData()
        {
            var user = await _userManager.Users.ToArrayAsync();
            UserViewModels.Clear();
            foreach (var item in user)
            {
                var roles = await _userManager.GetRolesAsync(item);
                var roleName = roles.FirstOrDefault();

                if (roleName != null && (roleName.Equals("User", StringComparison.OrdinalIgnoreCase)))
                {
                    if (item.IsSeller == true)
                    {

                        var userWithRole = new AppUserViewModel
                        {
                            Id = item.Id,
                            UserName = item.UserName,
                            FullName = item.FullName,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                            CreatedDate = item.CreatedDate,
                            RoleName = roleName,
                        };
                        UserViewModels.Add(userWithRole);
                    }
                }
            }
            OnPropertyChanged(nameof(UserViewModels));


        }
        private AppUserViewModel _AppUserViewModel;
        public AppUserViewModel AppUserViewModel
        {
            get { return _AppUserViewModel; }
            set
            {
                _AppUserViewModel = value;
                OnPropertyChanged(nameof(AppUserViewModel));
                if(_AppUserViewModel != null)
                {
                    FullNameInfo = _AppUserViewModel.FullName;
                    UserNameInfo = _AppUserViewModel.UserName;
                    EmailInfo = _AppUserViewModel.Email;
                    PhoneNumberInfo = _AppUserViewModel.PhoneNumber;
                    CreateDateInfo = (DateTime)_AppUserViewModel.CreatedDate;
                    RoleInfo = _AppUserViewModel.RoleName;
                }
            }
        }
        private async void UpdateSeller(object parameter)
        {
            if (_AppUserViewModel == null) {
                MessageBox.Show("Please select a User to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var update = await _userManager.FindByIdAsync(AppUserViewModel.Id);
            var currentRoles = await _userManager.GetRolesAsync(update);
            if(currentRoles.Contains("User"))
            {
                await _userManager.RemoveFromRoleAsync(update, "User"); // Xóa vai trò User
                await _userManager.AddToRoleAsync(update, "Seller"); // Thêm vai trò Seller
            }
            var result = await _userManager.UpdateAsync(update);
            {
                await LoadData(); // Tải lại danh sách người dùng
                MessageBox.Show("Update successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private async void Reject(object parameter)
        {
            if(_AppUserViewModel == null)
            {
                MessageBox.Show("Please select a User to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var update = await _userManager.FindByIdAsync(AppUserViewModel.Id);
            if(update != null)
            {
                update.IsSeller = false;
            }
            var result = await _userManager.UpdateAsync(update);
            {
                await LoadData(); // Tải lại danh sách người dùng
                MessageBox.Show("Update successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private string _FullNameInfo;
        public string FullNameInfo
        {
            get
            {
                return _FullNameInfo;
            }
            set
            {
                _FullNameInfo = value;
                OnPropertyChanged(nameof(FullNameInfo));
            }
        }
        private string _EmailInfo;
        public string EmailInfo
        {
            get
            {
                return _EmailInfo;
            }
            set
            {
                value = _EmailInfo; 
                OnPropertyChanged(nameof(EmailInfo));
            }

        }
        private string _PhoneNumberInfo;
        public string PhoneNumberInfo
        {
            get
            {
                return _PhoneNumberInfo;
            }
            set
            {
                value = _PhoneNumberInfo;
                OnPropertyChanged(nameof(PhoneNumberInfo));
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
        private string _RoleInfo;
        public string RoleInfo
        {
            get => _RoleInfo;
            set
            {
                value = _RoleInfo;
                OnPropertyChanged(nameof(RoleInfo));
            }
        }


    }
}
