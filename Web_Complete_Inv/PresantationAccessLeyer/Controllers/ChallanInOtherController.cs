using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class ChallanInOtherController : Controller
    {
        //
        // GET: /ChallanInOther/

        public ActionResult Index()
        {
            return View();
        }
        ChallanInDAL cicls;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
       
        public ChallanInOtherController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);

            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            cicls = new ChallanInDAL();
        
        
        
        }


        public JsonResult ChallaninotherValidate_Client2(string Challanno, string cDate, string Country, string SimNo, string Imeno_c, string HAND_SET, string handover_c, string execname_cmgh)
        {

            string Msg = string.Empty;
            //Msg = cicls.ChallaninOTHREValidate("Insert", Challanno, cDate, SimNo, Country, execname_c, branchcode, "ClientMaster", HAND_SET);
            Msg = cicls.ChallaninOTHREValidate("Insert", Challanno, cDate, SimNo, Country, "NA", branchcode, "", HAND_SET, execname_cmgh);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

    }
}
