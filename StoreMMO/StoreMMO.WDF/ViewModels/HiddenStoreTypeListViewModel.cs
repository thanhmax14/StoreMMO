using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
using Org.BouncyCastle.Utilities;
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

namespace StoreMMO.WDF.ViewModels
{
    public class HiddenStoreTypeListViewModel : BaseViewModel
    {

        private readonly IStoreTypeService _storeTypeService;

        public HiddenStoreTypeListViewModel(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
            StoreTypeViewModels = new ObservableCollection<StoreTypeViewModels>();
            LoadData();
            Show = new RelayCommand(Hidden);
        }

        public ObservableCollection<StoreTypeViewModels> StoreTypeViewModels { get; set; }
        public ICommand Show { get; }

        public void LoadData()
        {
            var obj = _storeTypeService.getAllStoreType();
            StoreTypeViewModels.Clear();    
            foreach(var item in obj)
            {
                if (item != null && item.IsActive == false)
                {
                    var storeTypeViewModel = new StoreTypeViewModels
                {
                    Id = item.Id,
                    Name = item.Name,
                    Commission = item.Commission,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                    // Map các thuộc tính khác tương ứng
                };
                StoreTypeViewModels.Add(storeTypeViewModel);
            }
            }
        }
        private string _NameInfo;
        public string NameInfo
        {
            get { return _NameInfo; }
            set
            {
                _NameInfo = value;
                OnPropertyChanged(nameof(NameInfo));
            }
        }
        private double _Commission;
        public double Commission
        {
            get { return _Commission; }
            set
            {
                _Commission = value;
                OnPropertyChanged(nameof(Commission));
            }
        }

        private DateTimeOffset? _CreatedDateInfo;
        public DateTimeOffset? CreatedDateInfo
        {
            get { return _CreatedDateInfo; }
            set
            {
                _CreatedDateInfo = value;
                OnPropertyChanged(nameof(CreatedDateInfo));
            }
        }

        private DateTimeOffset? _ModifiedDateInfo;
        public DateTimeOffset? ModifiedDateInfo
        {
            get { return _ModifiedDateInfo; }
            set
            {
                _ModifiedDateInfo = value;
                OnPropertyChanged(nameof(ModifiedDateInfo));
            }
        }
        private StoreTypeViewModels _SelectedStore;
        public StoreTypeViewModels SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
                if (_SelectedStore != null)
                {
                    NameInfo = _SelectedStore.Name ?? NameInfo;
                    Commission = _SelectedStore?.Commission ?? 0;
                    CreatedDateInfo = _SelectedStore.CreatedDate;
                    ModifiedDateInfo = _SelectedStore.ModifiedDate;
                }
                else
                {
                    // Reset dữ liệu nếu không có mục nào được chọn
                    // ResetCategoryInfo();
                }
            }
        }
        private void Hidden(object parameter)
        {
            if (SelectedStore == null) return;

            string categoryId = SelectedStore.Id;

            var categoryToHide = _storeTypeService.getByIdStoreType(categoryId);

            if (categoryToHide != null)
            {
                categoryToHide.IsActive = true;
                try
                {
                    _storeTypeService.UpdateStoreType1(categoryToHide);
                    LoadData();
                    MessageBox.Show("Hide successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy danh mục để ẩn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
