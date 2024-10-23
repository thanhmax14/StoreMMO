using BusinessLogic.Services.StoreMMO.Core.Categorys;
using StoreMMO.Core.ViewModels;
using StoreMMO.WDF.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StoreMMO.WDF.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;

        public ObservableCollection<CategoryViewModels> CategoryViewModels { get; set; }
        public ICommand AddNew { get; }
        public ICommand Update { get; }
        public ICommand Hide { get; }

        public CategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            CategoryViewModels = new ObservableCollection<CategoryViewModels>();
            loadData();
            AddNew = new RelayCommand(AddCategory);
            Update = new RelayCommand(UpdateCategory);
            Hide = new RelayCommand(HideCategory);

        }

        private CategoryViewModels _SelectedCategory;
        public CategoryViewModels SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                if (_SelectedCategory != null)
                {
                    Name = _SelectedCategory.Name;  // Kiểm tra tại đây
                    CreatedDateInfo = _SelectedCategory.CreatedDate;
                    ModifiedDateInfo = _SelectedCategory.ModifiedDate;
                }
                else
                {
                    // Reset dữ liệu nếu không có mục nào được chọn
                    ResetCategoryInfo();
                }
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
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

        public void loadData()
        {
            var categories = _categoryService.GetAll();
            CategoryViewModels.Clear();
            foreach (var category in categories)
            {
                if(category.IsActive == true)
                {
                    // Kiểm tra giá trị trước khi thêm vào danh sách
                    if (category != null)
                    {
                        CategoryViewModels.Add(category);
                    }
                }
              
            }
        }

        private void AddCategory(object parameter)
        {
            try
            {
                var newCategory = new CategoryViewModels
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    CreatedDate = CreatedDateInfo ?? DateTimeOffset.Now,
                    ModifiedDate = null
                    
                };

                _categoryService.AddCategory(newCategory);
                CategoryViewModels.Add(newCategory);

                // Reset dữ liệu
                ResetCategoryInfo();
            }
            catch (Exception ex)
            {
                // Bắt lỗi và log hoặc hiển thị lỗi
                Console.WriteLine("Lỗi xảy ra khi thêm danh mục: " + ex.Message);
            }
        }
        private bool CanAddCategory(object parameter)
        {
            // Ví dụ: chỉ cho phép thêm nếu NameInfo không rỗng
            return !string.IsNullOrWhiteSpace(Name);
        }

        private void UpdateCategory(object parameter)
        {
            if (SelectedCategory == null) return;

            SelectedCategory.Name = Name;
            SelectedCategory.CreatedDate = CreatedDateInfo;
            SelectedCategory.ModifiedDate = DateTimeOffset.Now;

            _categoryService.UpdateCategory(SelectedCategory);
            loadData();
        }

        private void HideCategory(object parameter)
        {
            if (SelectedCategory == null) return;

            // Lấy Id từ danh mục đã chọn
            string categoryId = SelectedCategory.Id;

            // Lấy danh mục từ cơ sở dữ liệu bằng Id
            var categoryToHide = _categoryService.getByIdCategory(categoryId); // Giả định bạn đã có phương thức này

            if (categoryToHide != null)
            {
                categoryToHide.IsActive = false; // Ẩn bằng cách vô hiệu hóa
                _categoryService.UpdateCategory(categoryToHide); // Cập nhật danh mục
                loadData();

            }
        }
        private bool CanUpdateOrHide(object parameter)
        {
            return SelectedCategory != null;
        }

        private void ResetCategoryInfo()
        {
            Name = string.Empty;
            CreatedDateInfo = null;
            ModifiedDateInfo = null;
        }
    }
}
