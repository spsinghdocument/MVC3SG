using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class VoucherEntryController : Controller
    {
        //
        // GET: /VoucherEntry/

        VoucherEntryDAL vecls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public VoucherEntryController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            vecls = new VoucherEntryDAL();
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
        public JsonResult v_auto_number()
        {
            return Json(vecls.voucher_autobillno(branchcode, sessid), JsonRequestBehavior.AllowGet);
        }


        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveVoucherEntry(string voucherno, string date, string cafno)
        {

            var v = vecls.VouchersearchData(voucherno, date, 1, cafno, sessid, branchcode);

            return Json(v, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveData(String Voucherno, String Date, String cafno, String Acno, String Name, String type, String paymentmode,
        decimal Amount, String Chequeno, String Bank, String Description, decimal netbalance)
        {
            string msg = string.Empty;
            int t = vecls.VoucherSaveData("InsertAll", Voucherno, Date, 1, cafno, Acno, Name, type, paymentmode, Amount, Chequeno, Bank,
                  Description, netbalance, sessid, branchcode);
            if (t > 0)
            {
                msg = "Record Insert Successfully.....";
            }
            else
                msg = "Record Not Inserted.....";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveUPDATE(int voucherid, String typ, String amount, String Chequeno2, String BANK2, String DESCRIPTION2, String Balance)
        {
            string msg = string.Empty;
            string t = vecls.VoucherSaveDataEdit("update", typ, amount, Chequeno2, BANK2, DESCRIPTION2, Balance, voucherid);
            if (t == "save")
            {
                msg = "Record Insert Successfully.....";
            }
            else
                msg = "Record Not Inserted.....";

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult setvoucherwithcafno(string cafno)
        {
            var v = vecls.search_cafno(cafno);
            return Json(v, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Duration = 0)]
        public JsonResult Delete(int voucherid)
        {
            string mg = "";
            string t = "";
            mg = vecls.delteDAL(voucherid);
            if (t == "DELETE")
            {
                mg = "Record Not Delete.....";
            }
            else
            {
                mg = "Record Delete Successfully.....";

            }
            return Json(mg, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////  ALL Voucher Delete ///////////////////////////////////
        [AcceptVerbs(HttpVerbs.Post)]
        [OutputCache(Duration = 0)]
        public JsonResult AllVoucherDelete(string voucherid)
        {
            string mg = "";
            string t = "";
            mg = vecls.AllVoucherdelteDAL(voucherid);
            if (t == "DELETE")
            {
                mg = "Record Not Delete.....";
            }
            else
            {
                mg = "Record Delete Successfully.....";

            }
            return Json(mg, JsonRequestBehavior.AllowGet);

        }

        ////////////////////////////////////////////////////////////////////////////////////////// End ///////////////////////////////////////////

          [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult setvoucherwith_cafno(string cafno)
        {
            var v = vecls.searchwith_cafno(cafno ,branchcode ,usertype);
            return Json(v, JsonRequestBehavior.AllowGet);
        }

          [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult setvoucherwith_voucherno(string voucherno)
        {
            var v = vecls.searchwith_voucherno(voucherno, branchcode, usertype);
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        
    }
}
