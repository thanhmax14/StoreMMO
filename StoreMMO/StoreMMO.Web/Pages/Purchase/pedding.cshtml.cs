using BusinessLogic.Services.CreateQR;
using BusinessLogic.Services.Encrypt;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Net.payOS;
using Net.payOS.Types;
using StoreMMO.Web.Models.ViewModels;

namespace StoreMMO.Web.Pages.Purchase
{
    public class peddingModel : PageModel
    {

        private readonly PayOS _Payos;
        private readonly CreateQR _createQR;
        public peddingModel(PayOS payOS, CreateQR create)
        {
             this._Payos = payOS;
            this._createQR = create;
              sendInfo = new Infopayment();
        }
        [BindProperty(SupportsGet = true)]
        public Infopayment getinfo { get; set; }
        public Infopayment sendInfo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {


            try
            {
                sendInfo.Ordercode = EncryptSupport.DecodeBase64(getinfo.Ordercode);
                sendInfo.descrip = EncryptSupport.DecodeBase64(getinfo.descrip);
                sendInfo.NameBank = EncryptSupport.DecodeBase64(getinfo.NameBank);
                sendInfo.NumberBank = EncryptSupport.DecodeBase64(getinfo.NumberBank);
                sendInfo.thoigian = getinfo.thoigian;
                sendInfo.amount =  getinfo.amount;
                sendInfo.price = EncryptSupport.DecodeBase64(getinfo.Price);
                sendInfo.img = EncryptSupport.DecodeBase64(getinfo.img);




                var check = await this._Payos.getPaymentLinkInformation(long.Parse(sendInfo.Ordercode));
                if (check != null)
                {
                    if (check.status.ToLower() == "EXPIRED".ToLower())
                    {
                        return Redirect("/Purchase/fail");
                    }
                    else if (check.status.ToLower() == "CANCELLED".ToLower())
                    {
                        return Redirect("/Purchase/fail");
                    } else if(check.status.ToLower() == "paid".ToLower())
                    {
                        return Redirect("/Purchase/Success");
                    }
                    else if (check.status.ToLower() == "PENDING".ToLower())
                    {
                        string imgqr = this._createQR.GetQR(sendInfo.img, "#FFFFFF", "#003366", 342, 342);
                        sendInfo.img = imgqr;
                        return Page();
                    }
                    return Page();
                }
            }
            catch(Exception e)
            {
                return NotFound();
            }
            return NotFound();
        }

     public async Task<IActionResult>OnpostAsync(string Ordercode)
        {
            PaymentLinkInformation paymentLinkInformation = await this._Payos.cancelPaymentLink(long.Parse(Ordercode));
            var a = paymentLinkInformation;
            return Redirect("/Purchase/fail");

        }

    }
}
