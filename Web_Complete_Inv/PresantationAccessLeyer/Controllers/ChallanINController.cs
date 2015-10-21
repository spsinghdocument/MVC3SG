using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class ChallanINController : Controller
    {
        //
        // GET: /ChallanIN/
        ChallanInDAL cicls;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        public ChallanINController()
        {

            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);

            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            cicls = new ChallanInDAL();
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

        public JsonResult ChallaninValidate_Executive(string Challanno, string cDate, string SimNo, string Country,  string execcode)
        {
            string Msg = string.Empty;
            Msg = cicls.ChallaninValidate("Insert", Challanno, cDate, SimNo, Country, execcode, branchcode, "ChallanOut");
           return Json(Msg, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ChallaninValidate_Client(string Challanno, string cDate, string SimNo, string Country, string execname_c)
        {
            string Msg = string.Empty;
            Msg = cicls.ChallaninValidate("InsertCM", Challanno, cDate, SimNo, Country, execname_c, branchcode, "ClientMaster");
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Challaningetsim_Executive(string empname, string emp)
        {
           
            return Json(cicls.GetSim_challanout(empname , emp), JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Challaningetsim_Client(string empname, string emp)
        {

            return Json(cicls.GetSim_clientmaster(empname, emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsername_Onselect(string execcode)
        {

            return Json(cicls.GetUsername_OnselectDAL(execcode), JsonRequestBehavior.AllowGet);
        }
        
    }
}
