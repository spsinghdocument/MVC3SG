using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class PurchaseItemEntryController : Controller
    {
        //
        // GET: /PurchaseItemEntry/
        PurchaseEntryOthersDAL pecls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public PurchaseItemEntryController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            pecls = new PurchaseEntryOthersDAL();
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

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseentryothersvalidate(string BillNo, string Date, string ItemType, string itemname, string IMEINO , string validity , string country)
        {
            string Msg = string.Empty;
          
           Msg =  pecls.purchaseentryotherstableinsert("Insert", 1, BillNo,Date, ItemType, itemname,
                sessid, sessid, IMEINO,validity , country, branchcode, partnername);
         
            return Json(Msg , JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration=0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseentryothers_Update(long sno, string ItemType, string itemname, string IMEINO)
        {
            string Msg = string.Empty;

            Msg = pecls.purchaseentryotherstableinsert("Update", sno, "", "11/11/2014", ItemType, itemname, "", "", IMEINO,"","", "", "");

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchData_imei(string empName)
        {
            var Emp = pecls.purchase_imeinoserach(empName);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchData_itemname(string empName)
        {
            var Emp = pecls.purchase_itemnmeserach(empName);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult auto_number()
        {
            return Json(pecls.autobillno_item(branchcode, sessid));
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseitementry_Search(string option, string strval, string date1, string date2)
        {
            string QryDevice = string.Empty;
            string StrQry = string.Empty;
            if (option == "BILL NO SEARCH")
            {
                QryDevice = "select *  from PurchaseEntryotherstab where  Date between '" + date1 + "' and '" + date2 + "' and billno='" + strval + "' ";
            }
            else if (option == "ITEM NAME SEARCH")
            {
                QryDevice = "select *  from PurchaseEntryotherstab where Date between '" + date1 + "' and '" + date2 + "' and itemname='" + strval + "' ";
            }
            else if (option == "IMEI NO SEARCH")
            {
                QryDevice = "select *  from PurchaseEntryotherstab where Date between '" + date1 + "' and '" + date2 + "' and simno='" + strval + "' ";
            }
            else if (option == "DATE SEARCH")
            {
                QryDevice = "select *  from PurchaseEntryotherstab where Date between '" + date1 + "' and '" + date2 + "' ";
            }

            switch (usertype)
            {
                case "ADMIN":
                    StrQry = QryDevice;
                    break;

                case "PARTNER":
                    StrQry = QryDevice + "  and branchcode = '" + branchcode + "' ";
                    break;

                case "EXECUTIVE":
                    StrQry = QryDevice + "  and branchcode = '" + branchcode + "' and loginusercode = '" + sessid + "' ";
                    break;

            }

            TempData["Qry"] = QryDevice;
            var v = pecls.purchaseitemwiseserach(QryDevice);

            return Json(v, JsonRequestBehavior.AllowGet);

        }
    }
}
