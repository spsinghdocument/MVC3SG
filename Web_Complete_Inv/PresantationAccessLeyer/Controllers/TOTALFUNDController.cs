using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class TOTALFUNDController : Controller
    {
        private static string st_cafno; private static string files_all = string.Empty;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = "";
 
        Add_Fund ad;
        //
        // GET: /TOTALFUND/

        public TOTALFUNDController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            username = Convert.ToString(System.Web.HttpContext.Current.Session["username"]);
            ad = new Add_Fund();

        }


        public ActionResult TOTAL_FUND()
        {
            return View();
        }
          [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateresult(string opt , string usercode )
        {
            return Json(ad.updateresultDAL(opt, usercode,branchcode,usertype) ,JsonRequestBehavior.AllowGet);
        }

    }
}
