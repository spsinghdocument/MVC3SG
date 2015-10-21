using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class SimReplacementController : Controller
    {
        //
        // GET: /SimReplacement/
        SimReplacementDAL srcls;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        public SimReplacementController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
         //   sessid = "a"; // Convert.ToString(Session["UserId"]);
            srcls = new SimReplacementDAL();
         
            
           
        }
        
        public ActionResult Sim_Replacement()
        {
            return View();
        }


        public JsonResult insert_data( string date , string currentsimno , string cafno ,string name ,string alotnewsim )
        {
            return Json(srcls.InsertData("Insert", date, currentsimno, cafno, name, alotnewsim, sessid), JsonRequestBehavior.AllowGet);
        }
    }
}
