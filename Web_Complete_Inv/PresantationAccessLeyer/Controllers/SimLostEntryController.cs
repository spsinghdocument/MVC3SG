using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class SimLostEntryController : Controller
    {
        //
        // GET: /SimLostEntry/
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        SimLostData smdcls;
        public SimLostEntryController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            smdcls = new SimLostData();
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
        public JsonResult SimLostInsert(string date , decimal simno , string country , string resion )
        {
            string Msg = string.Empty;
            Msg = smdcls.SimLostValidate(date, simno, country, resion, branchcode, sessid);
           
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

    }
}
