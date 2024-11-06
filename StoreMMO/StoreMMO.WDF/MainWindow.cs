using System.Windows;
using StoreMMO.WDF.ViewModels;

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
		private readonly RegisterSellerViewModel _registerSellerViewModel;
		private readonly ManageDisputesViewModel _manageDisputes;
		private readonly AllStoreListViewModel _allStoreListViewModel;
		private readonly HiddenStoreListViewModel _hiddenStoreListViewModel;
		private readonly ManageWithdrawlRequestViewModel _manageWithdrawlRequestViewModel;

		public MainWindow(CategoryViewModel categoryService, HiddenCategoriesListModel hiddenCategoriesList, StoreTypeListViewModel storeTypeListViewModel
			, HiddenStoreTypeListViewModel hiddenStoreTypeListViewModel, UserAccountViewModel userAccountViewModel,
			HidentUserAccountViewModel hidentUserAccountViewModel, RegisterSellerViewModel registerSellerViewModel,
            ManageDisputesViewModel manageDisputes, AllStoreListViewModel allStoreListViewModel,
            HiddenStoreListViewModel hiddenStoreListViewModel, ManageWithdrawlRequestViewModel manageWithdrawlRequestViewModel


            )
		{
			InitializeComponent();
			_categoryService = categoryService;
			_hiddenCategoriesList = hiddenCategoriesList;
			_storeTypeListViewModel = storeTypeListViewModel;
			_hiddenStoreTypeListViewModel = hiddenStoreTypeListViewModel;
			_userAccountViewModel = userAccountViewModel;
			_hidentUserAccountViewModel = hidentUserAccountViewModel;
			_registerSellerViewModel = registerSellerViewModel;
			_manageDisputes = manageDisputes;
			_allStoreListViewModel = allStoreListViewModel;
            _hiddenStoreListViewModel = hiddenStoreListViewModel;
			_manageWithdrawlRequestViewModel = manageWithdrawlRequestViewModel;	

        }

		private void button_UserAccountList(object sender, RoutedEventArgs e)
		{
			frMain.Content = new UserAccount(_userAccountViewModel);
		}

		private void buttonHiddenUserAccountList(object sender, RoutedEventArgs e)
		{
			frMain.Content = new HiddenUserAccount(_hidentUserAccountViewModel);
		}

		//private void buttonRegisteredSellers(object sender, RoutedEventArgs e)
		//{
		//	frMain.Content = new RegisteredSellers(_registerSellerViewModel);
		//}

		private void button_RegisteredStores(object sender, RoutedEventArgs e)
		{
			frMain.Content = new RegisteredStores();
		}

		private void button_StoresListt(object sender, RoutedEventArgs e)
		{
			frMain.Content = new AllStoreList(_allStoreListViewModel);
		}

		private void button_HiddenStoresList(object sender, RoutedEventArgs e)
		{
			frMain.Content = new HiddenStoreList(_hiddenStoreListViewModel);
		}

		private void button_CategoryList(object sender, RoutedEventArgs e)
		{

			// Truyền đối tượng categoryViewModel vào hàm khởi tạo
			frMain.Content = new CategoriesList(_categoryService);
		}

		private void button_HiddenCategoryList(object sender, RoutedEventArgs e)
		{
			frMain.Content = new HiddenCategoriesList(_hiddenCategoriesList);
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

		//private void button_DisputesList(object sender, RoutedEventArgs e)
		//{
		//	frMain.Content = new ManageDisputes(_manageDisputes);
		//}

		private void button_WithdrawalRequestsFromSellerList(object sender, RoutedEventArgs e)
		{
			frMain.Content = new ManageWithdrawlRequest(_manageWithdrawlRequestViewModel);
		}
	}
}