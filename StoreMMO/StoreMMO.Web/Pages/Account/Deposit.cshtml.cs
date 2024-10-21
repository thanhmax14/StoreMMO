using BusinessLogic.Services.CreateQR;
using BusinessLogic.Services.Encrypt;
using BusinessLogic.Services.Payments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Account
{
    public class DepositModel : PageModel
    {
        private readonly PaymentLIb _pay;
        private readonly CreateQR _createQR;
        public DepositModel(PaymentLIb paymentLIb, CreateQR create)
        {
            this._pay = paymentLIb;   
            this._createQR = create;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnpostAsync()
        {
            var host = Request.Host.ToString();
            var fullUrl = $"{Request.Scheme}://{host}/Purchase/pending";
            var faulurl = $"{Request.Scheme}://{host}/Purchase/fail";

            var create = await this._pay.CreatePay("Mua Mì Tôm",10,2000, fullUrl,
                faulurl, "chuyen tien mi tom",10);
              if(create != null)
            {

                return RedirectToPage("/Purchase/pedding", new
                {
                    Ordercode = EncryptSupport.EncodeBase64(create.orderCode+""), // Mã hóa orderCode
                    descrip = EncryptSupport.EncodeBase64(create.description), // Mã hóa description
                    NameBank = EncryptSupport.EncodeBase64("PHAM QUANG THANH"), // Mã hóa NameBank
                    NumberBank = EncryptSupport.EncodeBase64(create.accountNumber), // Mã hóa accountNumber
                    thoigian = create.expiredAt,
                    amount = create.amount,
                    Price = EncryptSupport.EncodeBase64("2000"), 
                    img = EncryptSupport.EncodeBase64(create.qrCode) 
                });

                //  return Redirect("" + create.checkoutUrl);
            }
            else
            {
                return NotFound();
            }
        }   
    }
}
