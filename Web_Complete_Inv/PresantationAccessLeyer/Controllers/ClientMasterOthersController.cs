using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class ClientMasterOthersController : Controller
    {
        //
        // GET: /ClientMasterOthers/
        ClientMasterOthersDAL clmocls;

        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        protected static string username = "";

        public ClientMasterOthersController()
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
            clmocls = new ClientMasterOthersDAL();
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

        public JsonResult insertclientmasterother_validate( string cafno, string customeracno, string User_Name, string Father_Name,
          string Payment_Mode, Decimal Amount, string Bank, string Cheque_No, string itemname,string imeino , string itemtype )
        {
            string Msg = string.Empty;

            Msg = clmocls.insertclientmasterothervalidate("Insert", cafno, customeracno, 0, User_Name, Father_Name,
                "CLIENT", Payment_Mode, Amount, Bank, Cheque_No, itemname, imeino, "NA", username, sessid, branchcode, "NOT IN USE" ,itemtype );
           
            return Json( Msg , JsonRequestBehavior.AllowGet);
        }

        public JsonResult ret_sessid()
        {
            return Json(sessid, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ret_username()
        {
            return Json(username, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AutocompleteSaerchitemname(string empName )
        {
            var data = clmocls.searchautocomitemnamenameDAL(empName, "HAND SET", usertype, sessid, branchcode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Fill_Imeino(string empName)
        {
            var data = clmocls.FillImeiNo(empName, "HAND SET", usertype, sessid, branchcode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
