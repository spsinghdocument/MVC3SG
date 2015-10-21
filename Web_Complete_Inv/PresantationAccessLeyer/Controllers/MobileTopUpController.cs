using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class MobileTopUpController : Controller
    {
        MobileTopupDAL mcls;
        //
        // GET: /MobileTopUp/ mobileTopDAL

         protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";

         public MobileTopUpController()
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
            mcls = new MobileTopupDAL();
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

        public ActionResult RenderIndex()
        {
            return PartialView("pv_newtoprequest");
        }

        public ActionResult Render_Index()
        {
            return PartialView("pv_execdataview");
        }

        [OutputCache(Duration=0)]
        public JsonResult insertdata_validate( string country, string mobileno, string plancode, string topup, string topup_data, string topup_sptopup, string total)
        {
          string Msg =   mcls.mobileTopDAL("Insert", country, mobileno, plancode, topup, topup_data, topup_sptopup, total, sessid, branchcode);
            return Json(Msg ,JsonRequestBehavior.AllowGet);
        }
      

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SimSearch_mTopup(string empName, string country)
        {
            return Json(mcls.SearchSim_No_mobileTopUp(empName, country, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }
        public JsonResult fillcombo(string country)
        {
            return Json(mcls.fillcomboDAL("",country,usertype,sessid ,branchcode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult fillcombodataval(string plancode , string ptype)
        {
            var v = mcls.filldatavalue(plancode, ptype);
            if (v.Count > 0)
            {
                return Json(mcls.filldatavalue(plancode, ptype), JsonRequestBehavior.AllowGet);
            }
            else
                return Json(0, JsonRequestBehavior.AllowGet);
            
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult fillrequest()
        {
            return Json(mcls.fillrequestDAL(usertype , branchcode), JsonRequestBehavior.AllowGet);
        }

         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateaccept(string id, string result, string loginuser, string tdata)
        {
            return Json(mcls.updateacceptDAL(id, result, loginuser, tdata), JsonRequestBehavior.AllowGet);
        }

             [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult ExecChkVal_data(string id, string result)
        {
            return Json(mcls.ExecChkVal(sessid , usertype ,branchcode), JsonRequestBehavior.AllowGet);
        }
    }
}
