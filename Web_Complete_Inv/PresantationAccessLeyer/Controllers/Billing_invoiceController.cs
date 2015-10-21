using System.Web.Mvc;

namespace PresantationAccessLeyer.Controllers
{
    public class Billing_invoiceController : Controller
    {
        //
        // GET: /Billing_invoice/

        public ActionResult Index()
        {
            return View();
        }

        public void abc(string caf, string Username, string mobileNo, string date, string billNo, string country, string agreementno, string EmailID, string user, string billDate, string billPeriod, string dueDate, string address)
        {
            //TempData["uv"] = a;
            //Session["arvinf"] = b;
            TempData["caf"] = caf;
            Session["Username"] = Username;
            Session["mobileNo"] = mobileNo;
            Session["date"] = date;
            Session["billNo"] = billNo;
            Session["country"] = country;
            Session["agreementno"] = agreementno;
            Session["EmailID"] = EmailID;
            Session["user"] = user;
            Session["billDate"] = billDate;
            Session["billPeriod"] = billPeriod;     
            Session["dueDate"] = dueDate;
            Session[" address"] = address;

        }

    }
}
