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
            Buy

        }
        public enum Order
        {
            None,  //=> 0
            PAID,
            fail

        }
        public enum OrderDetailStatus
        {
            None,  //=> 0
            report,  //
            ok,
            refun,
            backmoney

        }
        public enum Complaints
        {
            None,  //=> 0
            done,
            ReportAdmin,


        }

    }
}
