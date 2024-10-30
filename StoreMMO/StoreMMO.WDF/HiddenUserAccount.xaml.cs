using StoreMMO.WDF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreMMO.WDF
{
    /// <summary>
    /// Interaction logic for HiddenUserAccount.xaml
    /// </summary>
    public partial class HiddenUserAccount : Page
    {
        public HiddenUserAccount(HidentUserAccountViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
             viewModel.LoadDataAsync1();
        }
    }
}
