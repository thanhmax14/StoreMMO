using System.Windows;

namespace StoreMMO.WDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_UserAccountList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new UserAccount();
        }

        private void buttonHiddenUserAccountList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new HiddenUserAccount();
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
            frMain.Content = new CategoriesList();
        }

        private void button_HiddenCategoryList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new HiddenCategoriesList();
        }

        private void button_StoreTypeList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new StoreTypeList();
        }

        private void button_HiddenStoreTypeList(object sender, RoutedEventArgs e)
        {
            frMain.Content = new HiddenStoreTypeList();
        }

        private void button_DisputesList(object sender, RoutedEventArgs e)
        {

        }

        private void button_WithdrawalRequestsFromSellerList(object sender, RoutedEventArgs e)
        {

        }
    }
}