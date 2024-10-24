using StoreMMO.Core.Models;
using System;

namespace StoreMMO.Core.ViewModels
{
	public class GetOrderByUserViewModel
	{
        public string OrderCode { get; set; }
        public string OrderID { get; set; }              // Mã đơn hàng
		public DateTime OrderDate { get; set; }       // Ngày đặt hàng
		public string StoreName { get; set; }         // Tên cửa hàng
		public string ProName { get; set; }           // Tên loại sản phẩm
		public string Seller { get; set; }            // Người bán
		public int Quantity { get; set; }             // Số lượng sản phẩm trong đơn hàng
		public string TotalPrice { get; set; }       // Tổng giá trị đơn hàng
		public string Status { get; set; }            // Trạng thái đơn hàng
	}
}
