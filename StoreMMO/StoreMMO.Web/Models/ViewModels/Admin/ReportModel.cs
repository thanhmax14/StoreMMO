namespace StoreMMO.Web.Models.ViewModels.Admin
{
	public class ReportModel
	{
		public string Account { get; set; } // Tài khoản liên quan đến đơn hàng
		public string Password { get; set; } // Mật khẩu liên quan (nếu cần thiết)
		public decimal Price { get; set; } // Giá của đơn hàng
		public DateTime OrderDate { get; set; } // Ngày đặt hàng
		public string Status { get; set; } // Trạng thái đơn hàng (ví dụ: Pending, Completed, etc.)
		public string Message { get; set; } // Nội dung thông điệp hoặc lý do báo cáo
		public int DetailID { get; set; } // ID chi tiết của đơn hàng
	}
}
