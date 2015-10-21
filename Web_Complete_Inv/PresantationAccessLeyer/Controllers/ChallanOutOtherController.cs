using System;
using System.Linq;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class ChallanOutOtherController : Controller
    {
        //
        // GET: /ChallanOutOther/
        ChallanOutOthersDAL cccls;
        PurchaseEntryOthersDAL pecls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public ChallanOutOtherController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]);   // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            cccls = new ChallanOutOthersDAL();
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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult auto_number()
        {
            return Json(cccls.autobillno_item(branchcode, sessid));
        }

        public JsonResult purchaseentryotherstablesearch(string itemname)
        {
            var v = cccls.purchaseentryotherstablesearchforchallanout(usertype,sessid ,itemname ,sessid,branchcode).ToList();
            return Json(v, JsonRequestBehavior.AllowGet);
        }

        public JsonResult challanoutothersvalidate(string challanno, string cdate, string itemtype, string itemname, string Imeino, string AlotUser)
        {
            string Msg = String.Empty;

         Msg =   cccls.challanoutotherstableentry(challanno, cdate, "1", itemtype, itemname, Imeino,
                       AlotUser , sessid, branchcode, partnername);          
            return Json( Msg ,JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SearchData_imei(string empName  , string type)
        {
            var Emp = cccls.GetItemName(empName , type ,usertype ,branchcode , sessid );

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SearchData_imeino(string empName, string type)
        {
            var Emp = cccls.GetImeiNo_Search(empName,  usertype, branchcode, sessid);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SearchData_itemname(string empName, string type)
        {
            var Emp = cccls.GetItemName_Search(empName, usertype, branchcode, sessid);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }



          [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SearchData_Voucher(string empName)
        {
            var Emp = pecls.purchase_vouchernoserach_Voucher(empName);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult searchdata(string option , string itemname , string  date , string  date1)
          {
              string strsubqry = string.Empty;
              string stroptionval = string.Empty;

              if (option == "ITEM NAME SEARCH")
              {
                  stroptionval = "select challanno, cdate , itemtype ,itemname ,Imeino from challanoutothers where cdate between '" + date + "'  and  '" + date1 + "' and itemname = '" + itemname + "' ";
              }
           

             else if (option == "DATE SEARCH")
              {
                  stroptionval = "select challanno, cdate , itemtype ,itemname ,Imeino from challanoutothers where cdate between '" + date + "'  and  '" + date1 + "'  ";
              }
              else if (option == "IMEI NO SEARCH")
              {
                  stroptionval = "select challanno, cdate , itemtype ,itemname ,Imeino from challanoutothers where cdate between '" + date + "'  and  '" + date1 + "' and Imeino = '" + itemname + "' ";
              }

              switch (usertype)
              {
                  case "ADMIN" :
                      strsubqry = stroptionval;
                      break;

                  case "PARTNER":
                      strsubqry = stroptionval + " and branchcode = '"+branchcode+"' ";
                      break;

                  case "EXECUTIVE":
                      strsubqry = stroptionval + " and branchcode = '" + branchcode + "' and executivenameto = '"+sessid+"' ";
                      break; 
              }

              return Json(cccls. searchdatadal(strsubqry), JsonRequestBehavior.AllowGet);
          }
       
    }
}
