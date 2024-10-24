using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public class Constants
    {
        public enum BalanceStatus
        {
            None,  //=> 0
            EXPIRED,
            CANCELLED,
            PENDING,
            PAID

        }
        public enum BalanceTransactionType
        {
            None,  //=> 0
            Deposit,
            withdraw,
            Buy,
            Complaint

        }
        public enum Order
        {
            None,  //=> 0
            PAID,
            fail

        }
        public enum OrderDetailStatus
        {
            None,  //=> 0// don bth khong co compaint
            report, // don co complaint dang trang thai none
            ok,  // complaint done
            refun,  // admin / seller chon 1 torng 2 set thanh done trong complaint 
            backmoney

        }
        public enum ComplaintsStatus
        {
            None,  //=> 0
            done, 
            ReportAdmin,// gui tiep admin set lai thanh done
        }

    }
}