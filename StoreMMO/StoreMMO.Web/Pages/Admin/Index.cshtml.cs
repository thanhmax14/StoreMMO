using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace StoreMMO.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        [HttpPost] // Đổi sang POST
        [ValidateAntiForgeryToken] // Kiểm tra token
        public IActionResult OnPostData(string filter)
        {
            List<int> transactionData;
            List<int> revenueData;
            List<string> dates;

            switch (filter)
            {
                case "month":
                    // Dữ liệu cho "This Month"
                    transactionData = new List<int> { 31, 45, 38, 60, 72, 90, 120 };
                    revenueData = new List<int> { 10550, 120, 130, 160, 180, 210, 250 };
                    dates = new List<string>
                    {
                        "2023-10-01", "2023-10-08", "2023-10-15",
                        "2023-10-22", "2023-10-29", "2023-10-30", "2023-10-31"
                    };
                    break;

                case "year":
                    // Dữ liệu cho "This Year"
                    transactionData = new List<int> { 300, 400, 450, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300 };
                    revenueData = new List<int> { 1200, 1500, 1700, 1900, 244200, 2500, 3000, 3200, 3500, 4000, 4500, 5000 };
                    dates = new List<string>
                    {
                        "2023-01-01", "2023-02-01", "2023-03-01",
                        "2023-04-01", "2023-05-01", "2023-06-01",
                        "2023-07-01", "2023-08-01", "2023-09-01",
                        "2023-10-01", "2023-11-01", "2023-12-01"
                    };
                    break;

                default:
                    // Dữ liệu cho "Today"
                    transactionData = new List<int> { 5, 8, 110, 6, 12, 9, 7 };
                    revenueData = new List<int> { 50, 840, 40, 7070, 6550, 9440, 100 };
                    dates = new List<string>
                    {
                        "2023-10-30 08:00", "2023-10-30 09:00", "2023-10-30 10:00",
                        "2023-10-30 11:00", "2023-10-30 12:00", "2023-10-30 13:00",
                        "2023-10-30 14:00"
                    };
                    break;
            }

            var data = new
            {
                transactionData,
                revenueData,
                dates
            };

            return new JsonResult(data);
        }
    }
}
