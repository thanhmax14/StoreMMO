using BusinessLogic.Services.StoreMMO.Core.Categorys;
using StoreMMO.WDF.ViewModels;
using System.Windows;

namespace StoreMMO.WDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CategoryViewModel _categoryService;
        private readonly HiddenCategoriesListModel _hiddenCategoriesList;
        private readonly StoreTypeListViewModel _storeTypeListViewModel;
        private readonly HiddenStoreTypeListViewModel _hiddenStoreTypeListViewModel;
        private readonly UserAccountViewModel _userAccountViewModel;
        private readonly HidentUserAccountViewModel _hidentUserAccountViewModel;
       
        public MainWindow(CategoryViewModel categoryService, HiddenCategoriesListModel hiddenCategoriesList, StoreTypeListViewModel storeTypeListViewModel
            , HiddenStoreTypeListViewModel hiddenStoreTypeListViewModel, UserAccountViewModel userAccountViewModel,
            HidentUserAccountViewModel hidentUserAccountViewModel)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _hiddenCategoriesList = hiddenCategoriesList;
            _storeTypeListViewModel = storeTypeListViewModel;
            _hiddenStoreTypeListViewModel = hiddenStoreTypeListViewModel;
            _userAccountViewModel = userAccountViewModel;
            _hidentUserAccountViewModel = hidentUserAccountViewModel;
        }

        private void button_UserAccountList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new UserAccount(_userAccountViewModel);
        }

        private void buttonHiddenUserAccountList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new HiddenUserAccount(_hidentUserAccountViewModel);
        }

        private void buttonRegisteredSellers(object sender, RoutedEventArgs e)
        {
            frMain.Content = new RegisteredSellers();
        }

        private void button_RegisteredStores(object sender, RoutedEventArgs e)
        {
            frMain.Content = new RegisteredStores();
        }

        private void button_StoresListt(object sender, RoutedEventArgs e)
        {

        }

        private void button_HiddenStoresList(object sender, RoutedEventArgs e)
        {

        }

        private void button_CategoryList(object sender, RoutedEventArgs e)
        {

            // Truyền đối tượng categoryViewModel vào hàm khởi tạo
            frMain.Content = new CategoriesList(_categoryService);
        }

        private void button_HiddenCategoryList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new HiddenCategoriesList(_hiddenCategoriesList);
            _hiddenCategoriesList.loadData();
        }

        private void button_StoreTypeList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new StoreTypeList(_storeTypeListViewModel);
            _storeTypeListViewModel.loadData();
        }

        private void button_HiddenStoreTypeList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new HiddenStoreTypeList(_hiddenStoreTypeListViewModel);
            _hiddenStoreTypeListViewModel.LoadData();
        }

        private void button_DisputesList(object sender, RoutedEventArgs e)
        {

        }

        private void button_WithdrawalRequestsFromSellerList(object sender, RoutedEventArgs e)
        {

        }
    }
}