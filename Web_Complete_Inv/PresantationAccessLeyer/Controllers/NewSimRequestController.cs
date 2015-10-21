using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class NewSimRequestController : Controller
    {
        //
        // GET: /NewSimRequest/
        NewSimRequersDAL nscls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public NewSimRequestController()
        {

            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);

            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            nscls = new NewSimRequersDAL();
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
        public JsonResult insertsimrequest(string date, string counrty, string noofsim, string priority)
        {
            string Msg = nscls.Insert_Sim(date , counrty , noofsim , priority  , sessid,branchcode);
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult update(int id)
        {
            string Qry = "";
            if (usertype == "ADMIN")
            {
                 Qry = "update newsimrequest set status_n = 'OUT' where requestid = " + id + "";
            }
            else
                Qry = "update newsimrequest set status_n = 'OUT' where requestid = " + id + "";

            return Json(nscls.updatadal(Qry), JsonRequestBehavior.AllowGet);
        }

        public JsonResult NewSimRequest_Search()
        {
            return Json(nscls.NewSimRequest_SearchDAL(branchcode,usertype), JsonRequestBehavior.AllowGet);
        }
        public ActionResult pv_index()
        {
            return PartialView("pv_requestsim");
        }
    }
}
