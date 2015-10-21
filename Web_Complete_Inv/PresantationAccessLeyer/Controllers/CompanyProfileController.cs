using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class CompanyProfileController : Controller
    {
        //
        // GET: /CompanyProfile/
        CompanyProfileDAL cpcls;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        public CompanyProfileController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            cpcls = new CompanyProfileDAL();
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
        public JsonResult WEBlvalidate(string websp)
        {

            string emaill = websp;
            string msg = "";
            if (emaill.IndexOf("www") == -1 || emaill.IndexOf(".") == -1 || emaill.IndexOf(".") == -1)
            {

                msg = "invalid WEBSITE address";

            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }





        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertcompanyvalidate(string CompanyName, string Address, string ManagingDirector, string ContactNo, string CustSupportNo,

            string EmailID, string WebSite, string bankdetails, string panno, string servicetax)
        {
            string Msg = string.Empty;

            Msg = cpcls.insertcompany("Insert", 1, CompanyName, Address, ManagingDirector, ContactNo, CustSupportNo, "pch",
               EmailID, WebSite, bankdetails, panno, servicetax, sessid);

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateCompanyValidate(long cmpid, string Address, string ContactNo, string EmailID, string panno, string servicetax)
        {
            string Msg = string.Empty;

            Msg = cpcls.insertcompany("Update", cmpid, "", Address, "", ContactNo, "", "pch",
               EmailID, "", "", panno, servicetax, sessid);

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult searchdata(string strval)
        {
            string subqry = string.Empty;
            string strv1 = string.Empty;
            string strv2 = string.Empty;
            if (strval == "0")
            {
                strv1 = "A";
                strv2 = "Dz";
            }


            switch (strval)
            {
                case "A-D":
                    strv1 = "A";
                    strv2 = "Dz";
                    break;

                case "E-H":
                    strv1 = "E";
                    strv2 = "Hz";
                    break;

                case "I-P":
                    strv1 = "I";
                    strv2 = "Pz";
                    break;

                case "Q-Z":
                    strv1 = "Q";
                    strv2 = "Zz";
                    break;

            }
            string strqry = "select * from companyprofile where companyname between  '" + strv1 + "' and '" + strv2 + "' ";

            switch (usertype)
            {
                case "ADMIN":
                    subqry = strqry;
                    break;

                case "PARTNER":
                    subqry = strqry + " and loginusercode = '" + sessid + "' ";
                    break;


            }


            return Json(cpcls.CompanySearch(subqry), JsonRequestBehavior.AllowGet);
        }
    }
}
