using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using PresantationAccessLeyer.Models;

namespace PresantationAccessLeyer.Controllers
{
    public class PayMentReportController : Controller
    {
        //
        // GET: /PayMentReport/
        PaymentReportDAL prcls;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";

        public PayMentReportController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            prcls = new PaymentReportDAL();
        }
        public ActionResult Pay_ment()
        {
            return View();
        }

      
        public JsonResult paysearch(string f, string userid, string vdate,int pageIndex)
        {
            string val = string .Empty;

            if (usertype == "ADMIN")
            {
                val = userid ;
            }
            else if (usertype == "PARTNER")
            {
                val = userid ;
            }
           else 
            {
                val = sessid ;
            }



            return Json(prcls.reportdal(f, val, usertype, branchcode, vdate, pageIndex), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search_ACNOSearch(string empName , string opt1 )
        {
            return Json(prcls.Search_ACNO(empName,opt1, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }


        public JsonResult SearchClickAcno(string EID)
        {
         
         return Json(prcls.SearchAcDAL(EID, usertype, branchcode, sessid), JsonRequestBehavior.AllowGet);
        }

    }
}
