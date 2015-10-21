using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class PlanController : Controller
    {
        //
        // GET: /Plan/
        PlanDAL plancls;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        public PlanController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]);   // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            plancls = new PlanDAL();
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


        public JsonResult Insert_datavalidate( string plancode, string planname, string plantype, string country, string planvalue, string talktimedata) 
        {
            string msg = plancls.Plan_InsertDAL("INSERT", plancode, planname, plantype, country, planvalue, talktimedata, branchcode,sessid);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}
