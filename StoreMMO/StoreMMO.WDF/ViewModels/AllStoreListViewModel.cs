using BusinessLogic.Services.StoreMMO.Core.Stores;
using StoreMMO.Core.ViewModels;
using StoreMMO.WDF.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace StoreMMO.WDF.ViewModels
{
    public class AllStoreListViewModel : BaseViewModel
    {
        private readonly IStoreService _storeService;
        public ObservableCollection<StoreViewModels> storeViewModels { get; set; }
        public ICommand Hide { get; }


        public AllStoreListViewModel(IStoreService storeService)
        {
            _storeService = storeService;
            storeViewModels = new ObservableCollection<StoreViewModels>();  
            LoadData();
            Hide = new RelayCommand(Update);
        }

        public void LoadData()
        {
            var obj = _storeService.getAll("1");
            storeViewModels = new ObservableCollection<StoreViewModels>(obj);
            OnPropertyChanged(nameof(storeViewModels));
        }
        
        private StoreViewModels _SelectItem;
        public StoreViewModels SelectItem
        {
            get
            {
                return _SelectItem;
            }
            set
            {
                _SelectItem = value;
                OnPropertyChanged(nameof(SelectItem));
                if (_SelectItem != null) {
                    nameStoreInfo = _SelectItem.nameStore;
                    UserNameInfo = _SelectItem.UserName;
                }
            }
            
        }

        private string _nameStoreInfo;
        public string nameStoreInfo
        {
            get
            {
                return _nameStoreInfo;
            }
            set
            {
                _nameStoreInfo = value;
                OnPropertyChanged(nameof(nameStoreInfo));   
            }
        }
        private string _UserNameInfo;
        public string UserNameInfo
        {
            get
            {
                return _UserNameInfo;
            }
            set
            {
                   _UserNameInfo = value;
                OnPropertyChanged(nameof(UserNameInfo));
            }
        }
        private void Update(object paramater)
        {
            if(_SelectItem == null)
            {
                System.Windows.MessageBox.Show("Please select a store to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var storeToUpdate = new StoreAddViewModels
            {
                Id = _SelectItem.storeID,
                UserId = _SelectItem.userid,
                IsAccept = "0" // Cập nhật trạng thái IsAccept
            };

            // Gọi dịch vụ để cập nhật store
            var updatedStore = _storeService.Update(storeToUpdate);

            // Kiểm tra kết quả cập nhật
            if (updatedStore != null)
            {
                System.Windows.MessageBox.Show("Store updated successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData(); // Tải lại dữ liệu nếu cần
            }
            else
            {
                System.Windows.MessageBox.Show("Failed to update the store.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
