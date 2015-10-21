using System;
using System.Web.Mvc;

namespace PresantationAccessLeyer.Controllers
{
    public class RevenueReportController : Controller
    {
        //
        // GET: /RevenueReport/
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public RevenueReportController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
        }

        public ActionResult Index()
        {
            if (sessid == "")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }

        }

        public void revenue_search(string ddlsearchoption, string txtdatefrom, string txtdateto , string strval)
        {
            if (ddlsearchoption == "DATE WISE SEARCH")
            {
                string qry = "select * from vouchertable where [date] between '" + txtdatefrom + "' and '" + txtdateto + "' ";

            }
            else if (ddlsearchoption == "ACCOUNT WISE SEARCH")
            {
                string qry = "select * from vouchertable where Acno = '" + strval + "' and [Date] between '" + txtdatefrom + "' and '" + txtdateto + "' ";

            }
            else if (ddlsearchoption == "EXECUTIVE CODE SEARCH")
            {
                string qry = "select * from vouchertable where executivecode = '" + strval + "' and [Date] between '" + txtdatefrom + "' and '" + txtdateto + "' ";

            }
            else if (ddlsearchoption == "CAF NO SEARCH")
            {
                string qry = "select * from vouchertable where cafno = '" + strval + "' and  [Date] between '" + txtdatefrom + "' and '" + txtdateto + "' ";

            }
          //  filldata(qry);
        }

    }
}
