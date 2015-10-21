using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
 

namespace PresantationAccessLeyer.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Setting/
        SettingDAL scls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";

        public SettingController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            scls = new SettingDAL();

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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata(string prefixtype ,  string prefixnum ,  string  startwith )
        {
            string Msg = scls.insert_data(prefixtype, "PURCHASE", prefixnum, startwith, branchcode);
            return Json(Msg , JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata_PI(string prefixtype, string prefixnum, string startwith)
        {
            string Msg = scls.insert_data(prefixtype, "PURCHASE ITEM", prefixnum, startwith, branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata_C(string prefixtype, string prefixnum, string startwith)
        {
            string Msg = scls.insert_data(prefixtype, "CHALLAN", prefixnum, startwith, branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata_CI(string prefixtype, string prefixnum, string startwith)
        {
            string Msg = scls.insert_data(prefixtype, "CHALLAN ITEM", prefixnum, startwith, branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata_UserDetails(string prefixtype, string prefixnum, string startwith)
        {
            string Msg = scls.insert_data(prefixtype, "USER", prefixnum, startwith, branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata_Customer(string prefixtype, string prefixnum, string startwith)
        {
            string Msg = scls.insert_data(prefixtype, "CUSTOMER", prefixnum, startwith, branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata_ClientmasterTF(string prefixtype, string prefixnum, string startwith)
        {
            string Msg = scls.insert_data(prefixtype, "CLIENTMASTERTF", prefixnum, startwith, branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
    }
}
