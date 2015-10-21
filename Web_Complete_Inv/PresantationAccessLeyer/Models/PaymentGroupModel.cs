using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLeyer.EF;

namespace PresantationAccessLeyer.Models
{
    public class PaymentGroupModel
    {
        public List<Paymentsheet_Result> EmpList { get; set; }
        public int noofPage { get; set; }
    }
}