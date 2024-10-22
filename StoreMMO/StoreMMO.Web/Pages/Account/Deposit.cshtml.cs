
using BusinessLogic.Services.CreateQR;
using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.Payments;
using BusinessLogic.Services.StoreMMO.Core.Balances;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StoreMMO.Core.ViewModels;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Account
{
    public class DepositModel : PageModel
    {
        private readonly PaymentLIb _pay;
        private readonly CreateQR _createQR;
        private readonly IBalanceService _balanceService;
        public DepositModel(PaymentLIb paymentLIb, CreateQR create, IBalanceService balanceService)
        {
            this._pay = paymentLIb;
            this._createQR = create;
            this._balanceService = balanceService;
        }
        [BindProperty]
        public int Amount { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var checkUser = HttpContext.Session.GetString("UserID");
            if (checkUser != null)
            {
                var host = Request.Host.ToString();
                var fullUrl = $"{Request.Scheme}://{host}/Purchase/pedding";
                var failUrl = $"{Request.Scheme}://{host}/Purchase/fail";

                // Tạo yêu cầu thanh toán
                var create = await _pay.CreatePay("Mua Mì Tôm", 1, Amount, fullUrl, failUrl, "chuyen tien mi tom", 10);
                if (create != null)
                {
                    var transaction = new BalanceViewModels
                    {
                        Id = Guid.NewGuid().ToString(),
                        Amount = Amount,
                        Description = "Deposit",
                        OrderCode = create.orderCode.ToString(),
                        Status = create.status,
                        TransactionDate = DateTime.Now,
                        TransactionType = "Deposit",
                        UserId = checkUser
                    };

                    // Sử dụng phương thức add bất đồng bộ
                    bool add = await _balanceService.AddAsync(transaction); // Cập nhật gọi phương thức AddAsync
                    if (add)
                    {
                        // Chuyển hướng đến trang thành công
                        return RedirectToPage("/Purchase/pedding", new
                        {
                            Ordercode = EncryptSupport.EncodeBase64(create.orderCode.ToString()),
                            descrip = EncryptSupport.EncodeBase64(create.description),
                            NameBank = EncryptSupport.EncodeBase64("PHAM QUANG THANH"),
                            NumberBank = EncryptSupport.EncodeBase64(create.accountNumber),
                            thoigian = create.expiredAt,
                            amount = create.amount,
                            Price = EncryptSupport.EncodeBase64("2000"),
                            img = EncryptSupport.EncodeBase64(create.qrCode)
                        });
                    }
                    else
                    {
                        bool cancel = await _pay.cancelPay(create.orderCode.ToString());
                        return RedirectToPage("/Purchase/fail");
                    }
                }
                else
                {        
                    return NotFound();
                }
            }
            return NotFound();
        }
    }
}
