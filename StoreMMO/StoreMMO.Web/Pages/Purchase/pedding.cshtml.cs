using BusinessLogic.Services.CreateQR;
using BusinessLogic.Services.Encrypt;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
                // Decode input information
                sendInfo.Ordercode = EncryptSupport.DecodeBase64(getinfo.Ordercode);
                sendInfo.descrip = EncryptSupport.DecodeBase64(getinfo.descrip);
                sendInfo.NameBank = EncryptSupport.DecodeBase64(getinfo.NameBank);
                sendInfo.NumberBank = EncryptSupport.DecodeBase64(getinfo.NumberBank);
                sendInfo.thoigian = getinfo.thoigian;
                sendInfo.amount = getinfo.amount;
                sendInfo.price = EncryptSupport.DecodeBase64(getinfo.Price);
                sendInfo.img = EncryptSupport.DecodeBase64(getinfo.img);

                // Retrieve payment information
                var check = await _Payos.getPaymentLinkInformation(long.Parse(sendInfo.Ordercode));
                if (check != null)
                {
                    var status = check.status.ToLower(); // Store status in lowercase
                    switch (status)
                    {
                        case "expired":
                        case "cancelled":
                            return Redirect("/Purchase/fail");
                        case "paid":
                            return Redirect("/Purchase/Success");
                        case "pending":
                            string imgqr = _createQR.GetQR(sendInfo.img);
                            sendInfo.img = imgqr;
                            return Page();
                        default:
                            return Page(); // Unrecognized status, stay on page
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return NotFound();
            }

            return Redirect("/Purchase/fail");
        }


		public async Task<IActionResult> OnPostAsync(string Ordercode)
		{
			try
			{
				// Gọi phương thức để hủy thanh toán
				PaymentLinkInformation paymentLinkInformation = await this._Payos.cancelPaymentLink(long.Parse(Ordercode));

			
				if (paymentLinkInformation != null)
				{
					return Redirect("/Purchase/fail");
				}
				else
				{
					return Redirect("/Purchase/fail");
				}
			}
			catch (Exception ex)
			{
				PaymentLinkInformation paymentLinkInformation = await this._Payos.cancelPaymentLink(long.Parse(Ordercode));
				return Redirect("/Purchase/fail");
			}
		}


	}
}
