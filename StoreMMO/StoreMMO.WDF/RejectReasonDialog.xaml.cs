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
using System.Windows.Shapes;

namespace StoreMMO.WDF
{
	/// <summary>
	/// Interaction logic for RejectReasonDialog.xaml
	/// </summary>
	public partial class RejectReasonDialog : Window
	{
		public string Reason => ReasonTextBox.Text;
		public RejectReasonDialog()
		{
			InitializeComponent();
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true; // Đóng dialog và trả về true
			Close();
		}
    }
}
