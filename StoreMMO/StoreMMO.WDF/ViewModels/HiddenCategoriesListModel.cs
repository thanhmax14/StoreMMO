    using BusinessLogic.Services.StoreMMO.Core.Categorys;
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
        public class HiddenCategoriesListModel : BaseViewModel
        {
            private readonly ICategoryService _categoryService;
            public ObservableCollection<CategoryViewModels> HiddenCategoryViewModels { get; set; }
            public ICommand Hide { get; }


            public HiddenCategoriesListModel(ICategoryService categoryService)
            {
                _categoryService = categoryService;
                HiddenCategoryViewModels = new ObservableCollection<CategoryViewModels>();
               _=loadData();
                Hide = new RelayCommand(HideCategory);

            }
            public async Task loadData()
            {
                var categories = await _categoryService.GetAll1();
                HiddenCategoryViewModels.Clear();
                foreach (var category in categories)
                {
                    if (category.IsActive == false)
                    {
                        // Kiểm tra giá trị trước khi thêm vào danh sách
                        if (category != null)
                        {
                            HiddenCategoryViewModels.Add(category);
                        }
                    }

                }
            }
            private async void HideCategory(object parameter)
            {
                if (SelectedCategory == null) return;

                // Lấy Id từ danh mục đã chọn
                string categoryId = SelectedCategory.Id;

                // Lấy danh mục từ cơ sở dữ liệu bằng Id
                var categoryToHide =   await _categoryService.GetByIdAsync(categoryId); // Giả định bạn đã có phương thức này

                if (categoryToHide != null)
                {
                    categoryToHide.IsActive = true; // Ẩn bằng cách vô hiệu hóa
                    _categoryService.UpdateCategory(categoryToHide); // Cập nhật danh mục
                    MessageBox.Show("HideCategory successful!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    await loadData();

                }
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
            private void ResetCategoryInfo()
            {
                Name = string.Empty;
                CreatedDateInfo = null;
                ModifiedDateInfo = null;
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

        }
    }
