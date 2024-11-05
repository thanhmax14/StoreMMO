using BusinessLogic.Services.StoreMMO.Core.Balances;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using StoreMMO.Core.Models;
using StoreMMO.Core.ViewModels;
using StoreMMO.WDF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace StoreMMO.WDF.ViewModels
{
	public class ManageWithdrawlRequestViewModel : BaseViewModel
	{
		private readonly IBalanceService _balanceService;
		private readonly UserManager<AppUser> _userManager;
		public ObservableCollection<AppUserViewModel> UserList { get; set; }

		public ObservableCollection<BalanceViewModels> List { get; set; }
		public ICommand Update {  get;}
		public ICommand Reject { get; }


		public ManageWithdrawlRequestViewModel(IBalanceService balanceService, UserManager<AppUser> userManager)

		{
			_balanceService = balanceService;
			List = new ObservableCollection<BalanceViewModels>();
		 _= LoadData();
			NewItem = new BalanceViewModels();
			Update = new RelayCommand(Accept);
			Reject = new RelayCommand(Reject1); // Khởi tạo Reject

			_userManager = userManager;
			UserList = new ObservableCollection<AppUserViewModel>();
		}
		public async Task LoadData()
		{
			var obj = await _balanceService.GetAllBalanceAsync();
			var parsedBalances = new ObservableCollection<BalanceViewModels>();

			foreach (var balance in obj)
			{
				var parsedBalance = new BalanceViewModels
				{
					Id = balance.Id,
					UserId = balance.UserId,
					Amount = balance.Amount,
					TransactionType = balance.TransactionType,
					TransactionDate = balance.TransactionDate,
					Description = balance.Description,
					Status = balance.Status,
					OrderCode = balance.OrderCode,
					approve = balance.approve
				};

				// Kiểm tra và phân tích chuỗi Description
				if (!string.IsNullOrEmpty(parsedBalance.Description))
				{
					// Tìm và cắt phần bắt đầu từ ký tự '&'
					var index = parsedBalance.Description.IndexOf('&');
					if (index != -1)
					{
						var transactionInfo = parsedBalance.Description.Substring(index + 1).Trim(); // Cắt phần sau dấu '&'
						var parts = transactionInfo.Split('/'); // Tách theo dấu '/'

						// Kiểm tra nếu có đủ 4 phần tử
						if (parts.Length == 4)
						{
							parsedBalance.Bank = parts[0].Trim();        // Phần tử đầu tiên là Bank
							parsedBalance.NameBank = parts[1].Trim();    // Phần tử thứ hai là NameBank
							parsedBalance.NumberBank = parts[2].Trim();   // Phần tử thứ ba là NumberBank

							// Gán Amount từ phần tử thứ tư
							//if (decimal.TryParse(parts[3], out decimal amount))
							//{
							//	parsedBalance.Amount = amount; // Gán Amount
							//}
							//else
							//{
							//	Console.WriteLine("Invalid amount format.");
							//}
						}
						else
						{
							// Xử lý nếu chuỗi không có đủ phần tử
							Console.WriteLine("Invalid transaction format. Expected 4 parts.");
						}
					}
					else
					{
						Console.WriteLine("No transaction info found in Description.");
					}
				}
				else
				{
					Console.WriteLine("Description is null or empty for balance with ID: " + balance.Id);
				}

				// Thêm vào danh sách đã phân tích
				parsedBalances.Add(parsedBalance);
			}

			List = parsedBalances; // Cập nhật thuộc tính List
			OnPropertyChanged(nameof(List));
		}
		private BalanceViewModels _SelectItem;
		public BalanceViewModels SelectItem
		{
			get
			{
				return _SelectItem;
			}
			set
			{
				_SelectItem = value;
				OnPropertyChanged(nameof(SelectItem));	
				if(_SelectItem != null)
				{
					NewItem.Amount = _SelectItem.Amount;
					NewItem.TransactionDate = _SelectItem.TransactionDate;
					NewItem.Bank = _SelectItem.Bank;
					NewItem.NameBank = _SelectItem.NameBank;
					NewItem.NumberBank = _SelectItem.NumberBank;
					OnPropertyChanged(nameof(NewItem));
				}
			}
		}
		private BalanceViewModels _NewItem;
		public BalanceViewModels NewItem
		{
			get { return _NewItem; }
			set
			{
				_NewItem = value;	
				OnPropertyChanged(nameof(NewItem));
			}
		}
		private async void Accept(object parameter)
		{
			if(_SelectItem == null)
			{
				MessageBox.Show("Plese choice option!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
				return; // Dừng lại nếu có trường nào đó bị trống
			}
			if(_SelectItem != null)
			{
				_SelectItem.Status = "Paid";
				_SelectItem.approve = DateTime.UtcNow;
				await _balanceService.UpdateAsync(SelectItem);
				MessageBox.Show("Accept successful", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

			}
		}
		private string _reason;
		public string Reason
		{
			get { return _reason; }
			set
			{
				_reason = value;
				OnPropertyChanged(nameof(Reason));
			}
		}
		private async void Reject1(object parameter)
		{
			if (_SelectItem == null)
			{
				MessageBox.Show("Please select a request!", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			var rejectDialog = new RejectReasonDialog();
			if (rejectDialog.ShowDialog() == true)
			{
				Reason = rejectDialog.Reason;

				if (string.IsNullOrEmpty(Reason))
				{
					MessageBox.Show("Rejection reason cannot be empty!", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				// Gọi hàm RejectRequestAsync trong service để từ chối yêu cầu và cập nhật thông tin
				bool isRejected = await _balanceService.RejectRequestAsync(_SelectItem, Reason);
				if(isRejected)
				{
					MessageBox.Show("The request has been rejected, and the balance has been successfully updated.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
							await LoadData();
				}

				//var find = await _balanceService.GetBalanceByIDAsync(_SelectItem.Id);
				//find.Status = "CANCELLED";
				//find.approve = DateTime.UtcNow;
				//var update = await _balanceService.UpdateAsync(find);

				//if (update)
				//{
				//	var findUser = await _userManager.FindByIdAsync(find.UserId);
				//	if (findUser != null) {
				//		findUser.CurrentBalance -= (find.Amount);
				//		var result = await _userManager.UpdateAsync(findUser);
				//		if (result.Succeeded)
				//		{
				//			MessageBox.Show("The request has been rejected, and the balance has been successfully updated.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
				//			await LoadData();
				//		}
				//	}


				//}
				else
				{
					MessageBox.Show("Failed to reject the request!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

	}
}
