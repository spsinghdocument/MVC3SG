using System;
using System.Web.Mvc;
using DataAccessLeyer.EF;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class SimWiseSearchController : Controller
    {
        //
        // GET: /SimWiseSearch/
        inventoryforwebappEntities inv;
        SimWiseSearchDal sws;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public SimWiseSearchController()
        {

            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            sws = new SimWiseSearchDal();
            inv = new inventoryforwebappEntities();
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

        public JsonResult Simwise_Search(string Simno, string option)
        {
           return Json(sws.simwisesearch_proc(Simno, usertype, branchcode, sessid), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSimDetails(string empName, string option)
        {
            var v = sws.SearchPlanDAL(option, empName, usertype, sessid, branchcode);
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        public JsonResult puksearch(string Simno, string option)
        {
            string strsubqry = string.Empty;
            string strqry =  string.Empty;

            if (option == "PUK SEARCH")
            {
                strqry = "select [Date] as SimDate  , SimNo as Sim_No , Country , phoneno as MobileNo , puk as PUK ,'PurchaseEntry' as TName ,loginusercode as LoginCode from PurchaseEntry where puk = '" + Simno + "'  ;";
            }
            else if (option == "MOBILE NO. SEARCH")
            {
                strqry = "select [Date] as SimDate  , SimNo as Sim_No , Country, phoneno as MobileNo , puk as PUK ,'PurchaseEntry' as TName ,loginusercode as LoginCode from PurchaseEntry where PhoneNo = '" + Simno + "'  ;";
            }
           if (usertype == "ADMIN")
            {
                strsubqry = strqry;
            }
          else  if (usertype == "PARTNER")
            {
                strsubqry = strqry + " and branchcode = '"+branchcode+"'" ;
            }
           else if (usertype == "EXECUTIVE")
            {
                strsubqry = strqry + " and branchcode = '" + branchcode + "'  and loginusercode ='"+sessid+"' "; ;
            }
          
            return Json(sws.PurchaseSearch_PUKDATA(strqry) ,JsonRequestBehavior.AllowGet) ;
        }

    }
}
