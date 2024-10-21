using Azure;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Errors;
using Net.payOS.Types;
using Net.payOS.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Payments
{

   
    public class PaymentLIb
    {

        private readonly PayOS _payOS;
        public PaymentLIb(PayOS payOS)
        {
            this._payOS = payOS;
        }
        public async Task<CreatePaymentResult> CreatePay(string name, int quantity, int price, string returnUrl, string cancelUrl, string mess,int timeexpiration)
        {
            try
            {
                long expirationTimestamp = DateTimeOffset.UtcNow.AddMinutes(timeexpiration).ToUnixTimeSeconds();


                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData(name, quantity, price);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, price, mess, items, cancelUrl, returnUrl
                 , null, null, null, null, null, expirationTimestamp
                    );
                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                return createPayment;            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return null ;
            }
        }


        public async Task<bool> cancelPay(string ordercode)
        {
            PaymentLinkInformation paymentLinkInformation = await this._payOS.cancelPaymentLink(long.Parse(ordercode));
            return true;
        }

    }
}
