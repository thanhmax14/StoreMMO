using System;

namespace StoreMMO.Core.ViewModels
{
	public class GetOrderDetailsViewModel
	{
		public string Account { get; set; }             // Tài khoản của sản phẩm
		public string Password { get; set; }                 // Mật khẩu của sản phẩm
		public string quantity { get; set; }               // Số lượng sản phẩm trong đơn hàng
		public string Price { get; set; }              // Giá sản phẩm trong đơn hàng
		public DateTime Dates { get; set; }             // Ngày đặt hàng
		public string stasusPayment { get; set; }       // Trạng thái thanh toán
		public string status { get; set; }              // Trạng thái đơn hàng
	}
}
