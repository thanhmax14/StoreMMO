using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
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
using static QRCoder.PayloadGenerator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StoreMMO.WDF.ViewModels
{
	public class UserAccountViewModel : BaseViewModel
	{

		private readonly UserManager<AppUser> _userManager;
		public ObservableCollection<AppUserViewModel> UserList { get; set; }
		public ICommand Update { get; }
		public ICommand Hide { get; }
		public ICommand Show { get; }


		public UserAccountViewModel(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
			UserList = new ObservableCollection<AppUserViewModel>();
			InitializeAsync();
			Update = new RelayCommand(UpdateUser);
			Hide = new RelayCommand(Hiddent);
			Show = new RelayCommand(Show1);
		}
		private async Task InitializeAsync()
		{
			await LoadDataAsync();
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
					CreateDateInfo = _SelectUser.CreatedDate;
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
		private DateTime? _CreateDateInfo;
		public DateTime? CreateDateInfo
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


		public async Task LoadDataAsync() // Phương thức bất đồng bộ để tải dữ liệu
		{
			var users = await _userManager.Users.ToListAsync(); // Sử dụng bất đồng bộ để lấy danh sách người dùng
			UserList.Clear();

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
												// Gán giá trị bool cho RoleName: true nếu là "Seller", false nếu là "User"
												//   RoleName = roleName.Equals("Seller", StringComparison.OrdinalIgnoreCase)

						};

						UserList.Add(userWithRole); // Thêm người dùng với vai trò Seller vào danh sách
					}

				}
			}
		}

		private async void UpdateUser(object parameter)
		{
			//if (SelectUser == null)
			//{
			//	MessageBox.Show("Please select a User to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
			//	return;
			//}
			if (string.IsNullOrWhiteSpace(FullNameInfo) ||
	string.IsNullOrWhiteSpace(EmailInfo) ||
	string.IsNullOrWhiteSpace(PhoneInfo) ||
	string.IsNullOrWhiteSpace(UserNameInfo))
			{
				MessageBox.Show("Please fill in all the fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
            // Kiểm tra số điện thoại có không vượt quá 10 ký tự
            if (PhoneInfo.Length > 10 || !PhoneInfo.All(char.IsDigit))
            {
                MessageBox.Show("Phone number must be 10 digits or less and contain only numbers.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra ngày sinh không vượt quá ngày hiện tại
            if (CreateDateInfo > DateTime.Now)
            {
                MessageBox.Show("Date of birth cannot be in the future.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Cập nhật thông tin người dùng
            var userToUpdate = await _userManager.FindByIdAsync(SelectUser.Id);
			if (userToUpdate != null)
			{
				userToUpdate.FullName = FullNameInfo;
				userToUpdate.Email = EmailInfo;
				userToUpdate.PhoneNumber = PhoneInfo;
				userToUpdate.UserName = UserNameInfo;
				//userToUpdate.IsSeller = false;
				// Lấy các vai trò hiện tại của người dùng
				var currentRoles = await _userManager.GetRolesAsync(userToUpdate);
				Debug.WriteLine($"Current Roles: {string.Join(", ", currentRoles)}"); // Kiểm tra vai trò hiện tại
				Debug.WriteLine($"RoleInfo: {RoleInfo}"); // Kiểm tra giá trị RoleInfo
														  // Kiểm tra nếu vai trò cần thay đổi

				if (currentRoles.Contains("User"))
				{
					await _userManager.RemoveFromRoleAsync(userToUpdate, "User"); // Xóa vai trò User
					await _userManager.AddToRoleAsync(userToUpdate, "Seller"); // Thêm vai trò Seller
					userToUpdate.IsSeller = true;                                // Đặt IsSeller thành true

				}
				else if (currentRoles.Contains("Seller"))
				{
					await _userManager.RemoveFromRoleAsync(userToUpdate, "Seller"); // Xóa vai trò Seller
					await _userManager.AddToRoleAsync(userToUpdate, "User"); // Thêm vai trò User
					userToUpdate.IsSeller = false;                                 // Đặt IsSeller thành false

				}


				// Lưu thay đổi vào cơ sở dữ liệu
				var result = await _userManager.UpdateAsync(userToUpdate);
				if (result.Succeeded)
				{
					LoadDataAsync(); // Tải lại danh sách người dùng
					MessageBox.Show("Update successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
					ClearForm();
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
		private void ClearForm()
		{
			FullNameInfo = string.Empty;
			EmailInfo = string.Empty;
			PhoneInfo = string.Empty;
			UserNameInfo = string.Empty;
			RoleInfo = null;
			SelectUser = null;
			CreateDateInfo = null;
		}

		private async void Hiddent(object parameter)
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
					await LoadDataAsync(); // Tải lại danh sách người dùng
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
					LoadDataAsync(); // Tải lại danh sách người dùng
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
