using AutoMapper;
using BusinessLogic.Services.StoreMMO.Core.StoreTypes;
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
    public class StoreTypeListViewModel : BaseViewModel
    {
        private readonly IStoreTypeService _storeTypeService;
        public ObservableCollection<StoreTypeViewModels> StoreTypeViewModels { get; set; }
        private readonly IMapper _mapper;
        public ICommand AddNew { get; }
        public ICommand Update { get; }
        public ICommand Hide { get; }


        public StoreTypeListViewModel(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
            StoreTypeViewModels = new ObservableCollection<StoreTypeViewModels>();
            loadData();
            AddNew = new RelayCommand(AddStore);
            Update = new RelayCommand(UpdateStore);
            Hide = new RelayCommand(Hidden);
        }
        public void loadData()
        {
            var storeTypes = _storeTypeService.getAllStoreType();

            // Xóa danh sách cũ
            StoreTypeViewModels.Clear();

            // Map từng phần tử từ storeTypes sang StoreTypeViewModels và thêm vào ObservableCollection
            foreach (var storeType in storeTypes)
            {
                if (storeType != null && storeType.IsActive == true)
                {
                    
                    var storeTypeViewModel = new StoreTypeViewModels
                    {
                        Id = storeType.Id,
                        Name = storeType.Name,
                        Commission = storeType.Commission,
                        CreatedDate = storeType.CreatedDate,
                        ModifiedDate = storeType.ModifiedDate,
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
        private void AddStore(object parameter)
        {
            try
            {
                var newCategory = new StoreTypeViewModels
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NameInfo,
                    Commission = Commission,
                    CreatedDate = DateTimeOffset.Now,  // Lấy ngày hiện tại
                    IsActive = true,
                };

                _storeTypeService.AddStoreType(newCategory);
                StoreTypeViewModels.Add(newCategory);

                // Reset dữ liệu
                MessageBox.Show("Add successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                // Bắt lỗi và log hoặc hiển thị lỗi
                Console.WriteLine("Lỗi xảy ra khi thêm danh mục: " + ex.Message);
            }
        }
        private void UpdateStore(object parameter)
        {
            // Kiểm tra nếu SelectedCategory là null
            if (SelectedStore == null)
            {
                MessageBox.Show("Please select a category to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Cập nhật giá trị
            SelectedStore.Name = NameInfo;
            SelectedStore.Commission = Commission;
            SelectedStore.ModifiedDate = DateTimeOffset.Now;    
            SelectedStore.IsActive = true;    
            _storeTypeService.UpdateStoreType1(SelectedStore); // Gọi phương thức cập nhật với đối tượng mới

            // Làm mới dữ liệu
            loadData();
            MessageBox.Show("Update successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Hidden(object parameter)
        {
            if (SelectedStore == null) return;

            string categoryId = SelectedStore.Id;

            var categoryToHide = _storeTypeService.getByIdStoreType(categoryId);

            if (categoryToHide != null)
            {
                categoryToHide.IsActive = false;
                try
                {
                    _storeTypeService.UpdateStoreType1(categoryToHide);
                    loadData();
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
