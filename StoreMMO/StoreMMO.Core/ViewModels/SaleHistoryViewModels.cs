using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class SaleHistoryViewModels
    {
        
        public string OrderCode { get; set; }
        public string OrderID { get; set; }
        public DateTime Dates { get; set; }
        public string NguoiMua { get; set; }
        public string StoreName { get; set; }
        public string Productype { get; set; }
        public string Price { get; set; }

        public string totalMoney { get; set; }
        public string AdminMoney { get; set; }
        public string stasusPayment { get; set; }


    }
}
