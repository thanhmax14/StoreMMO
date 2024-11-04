using BusinessLogic.Services.StoreMMO.Core.Purchases;
using BusinessLogic.Services.StoreMMO.Core.SellerDashBoard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreMMO.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IPurchaseService _Puchase;
        private readonly ISellerDashBoardService _sellerDashBoardService;

        public IndexModel(IPurchaseService purchase,ISellerDashBoardService sellerDashBoardService)
        {
            this._Puchase = purchase;
        this._sellerDashBoardService = sellerDashBoardService;
        }



        public IEnumerable<TopStoreViewModels> list = new List<TopStoreViewModels>();

        public async void OnGet()
        {
             list = await _Puchase.TopStore();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostData(string filter)
        {
            List<int> transactionData = new List<int>(new int[24]); // Khởi tạo danh sách 24 giờ mặc định là 0
            List<decimal> revenueData = new List<decimal>(new decimal[24]);
            List<string> dates = Enumerable.Range(0, 24).Select(i => DateTime.Now.Date.AddHours(i).ToString("yyyy-MM-dd HH:mm")).ToList();


            DateTime now = DateTime.Now;

            switch (filter)
            {
                case "today":

                    var todayTransactions = await this._Puchase.GetDailyTransactionSummary();
                    var a = todayTransactions;
                    // Duyệt qua danh sách dữ liệu trả về và gán vào danh sách 24 giờ
                    foreach (var transaction in todayTransactions)
                    {
                        int hour = (transaction.TransactionDate.Hour + 7) % 24; 

                        transactionData[hour] = transaction.TotalTransactions; // Gán số lượng giao dịch
                        revenueData[hour] = transaction.TotalRevenue; // Gán doanh thu
                    }
                    break;

                case "month":
                   transactionData = new List<int>(new int[31]); // Khởi tạo danh sách 31 ngày
                     revenueData = new List<decimal>(new decimal[31]);
                     dates = Enumerable.Range(1, 31).Select(i => new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).ToString("yyyy-MM-dd")).ToList();

                    var monthlyTransactions = await this._Puchase.GetMonth();
                    foreach (var transaction in monthlyTransactions)
                    {
                        int day = transaction.TransactionDate.Day - 1; // Ngày trong tháng (0-30 cho 31 ngày)
                        if (day >= 0 && day < transactionData.Count) // Kiểm tra chỉ số hợp lệ
                        {
                            transactionData[day] += transaction.TotalTransactions; // Cộng dồn số lượng giao dịch
                            revenueData[day] += transaction.TotalRevenue; // Cộng dồn doanh thu
                        }
                    }

                    break;

                case "year":
                    transactionData = new List<int>(new int[12]); // 12 tháng trong năm
                    revenueData = new List<decimal>(new decimal[12]);
                    dates = Enumerable.Range(1, 12).Select(i => new DateTime(DateTime.Now.Year, i, 1).ToString("yyyy-MM")).ToList();

                    var yearlyTransactions = await this._Puchase.GetMonthInYear();
                    foreach (var transaction in yearlyTransactions)
                    {
                        int month = transaction.TransactionDate.Month - 1;
                        transactionData[month] += transaction.TotalTransactions;
                        revenueData[month] += transaction.TotalRevenue;
                    }
                    break;

              

                case "all":
                    transactionData = new List<int>();
                    revenueData = new List<decimal>();
                    dates = new List<string>();

                    var allTransactions = await this._Puchase.GetAllYear();
                    foreach (var transaction in allTransactions)
                    {
                        transactionData.Add(transaction.TotalTransactions);
                        revenueData.Add(transaction.TotalRevenue);
                        dates.Add(transaction.TransactionDate.ToString("yyyy"));
                    }
                    break;

                case "sellertoday":
                  
                    var today = await this._sellerDashBoardService.GetDailyTransactionSummary(UserId);
                 //   var a = today;
                    // Duyệt qua danh sách dữ liệu trả về và gán vào danh sách 24 giờ
                    foreach (var transaction in today)
                    {
                        int hour = (transaction.TransactionDate.Hour + 7) % 24;

                        transactionData[hour] = transaction.TotalTransactions; // Gán số lượng giao dịch
                        revenueData[hour] = transaction.TotalRevenue; // Gán doanh thu
                    }
                    break;
                case "sellermonth":
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); // Số ngày trong tháng hiện tại
                    transactionData = new List<int>(new int[daysInMonth]); // Khởi tạo danh sách theo số ngày thực tế
                    revenueData = new List<decimal>(new decimal[daysInMonth]);

                    dates = Enumerable.Range(1, daysInMonth)
                        .Select(i => new DateTime(DateTime.Now.Year, DateTime.Now.Month, i).ToString("yyyy-MM-dd"))
                        .ToList();

                    var m = await this._sellerDashBoardService.GetMonth(UserId);
                    foreach (var transaction in m)
                    {
                        int day = transaction.TransactionDate.Day - 1; // Ngày trong tháng (0-30 cho 31 ngày)
                        if (day >= 0 && day < transactionData.Count) // Kiểm tra chỉ số hợp lệ
                        {
                            transactionData[day] += transaction.TotalTransactions; // Cộng dồn số lượng giao dịch
                            revenueData[day] += transaction.TotalRevenue; // Cộng dồn doanh thu
                        }
                    }
                    break;
                case "selleryear":
                    transactionData = new List<int>(new int[12]); // 12 tháng trong năm
                    revenueData = new List<decimal>(new decimal[12]);
                    dates = Enumerable.Range(1, 12).Select(i => new DateTime(DateTime.Now.Year, i, 1).ToString("yyyy-MM")).ToList();

                    var y = await this._sellerDashBoardService.GetMonthlyTransactionSummary(UserId);
                    foreach (var transaction in y)
                    {
                        int month = transaction.TransactionDate.Month - 1;
                        transactionData[month] += transaction.TotalTransactions;
                        revenueData[month] += transaction.TotalRevenue;
                    }
                    break;
                case "sellerall":
                    transactionData = new List<int>();
                    revenueData = new List<decimal>();
                    dates = new List<string>();

                    var all = await this._sellerDashBoardService.GetYearlyTransactionSummary(UserId);
                    foreach (var transaction in all)
                    {
                        transactionData.Add(transaction.TotalTransactions);
                        revenueData.Add(transaction.TotalRevenue);
                        dates.Add(transaction.TransactionDate.ToString("yyyy"));
                    }
                    break;


                default:
                
                    break;
            }

            return new JsonResult(new { transactionData, revenueData, dates });
        }
    }
}
