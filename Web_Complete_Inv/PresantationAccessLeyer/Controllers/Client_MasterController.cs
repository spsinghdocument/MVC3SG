using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using PresantationAccessLeyer.Models;
using System.IO;
using Microsoft.Web.Administration;


namespace PresantationAccessLeyer.Controllers
{
    public class Client_MasterController : Controller
    {
        //
        // GET: /Client_Master/

        private static string st_cafno; private static string files_all = string.Empty;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = "";
        ClientMasterDAL cmcls;
        //inventory_MVCEntities imv;
        public Client_MasterController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            username = Convert.ToString(System.Web.HttpContext.Current.Session["username"]);
            cmcls = new ClientMasterDAL();
           
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
        public JsonResult insertclientmastervalidate(string date, string cafno, string customeracno,  string User_Name, string Father_Name,
           string Payment_Mode, Decimal Amount, string Bank, string Cheque_No, string Sim_No,
          string Plan, string Validity, string Country, string Start_Date, string End_Date, string Sim_Return_Date,
          string Tarrif_Detail, string execalotcode, string tfcode, string redioopt, string itemname, string imeino, string amtstaus, string neft)
        {
            st_cafno = cafno;
            string Msg = cmcls.insertclientmastervalidate("Insert", date, cafno, customeracno, 0, files_all.TrimEnd(','), User_Name, Father_Name,
                "", Payment_Mode, Amount, Bank, Cheque_No, Sim_No.Trim().TrimStart(),Plan,  Validity, Country,
               Start_Date, End_Date, Sim_Return_Date, Tarrif_Detail, execalotcode, sessid, branchcode, "NOT IN USE", tfcode, redioopt, itemname, imeino, amtstaus, neft, "");

            files_all = "";

         return  Json(Msg, JsonRequestBehavior.AllowGet);
        }


         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult Update_cafdoc(string cafno)
         {
             string msg = "";
             string steqry = "Update clientmaster set pdffile ='" + files_all + "' where cafno = '" + cafno + "' and Branchcode = '" + branchcode + "' ";
             msg = cmcls.updatedocdal(steqry);
             files_all = "";
             return Json(msg, JsonRequestBehavior.AllowGet);
         }
          
         

          [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult ret_sessid()
         {             
             return Json(sessid, JsonRequestBehavior.AllowGet);
         }
          [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult ret_username()
         {
             return Json(username, JsonRequestBehavior.AllowGet);
         }

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult searchautocomcustname(string empName)
         {
             return Json(cmcls.searchautocomcustnameDAL(empName, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }


         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult Searchvalidity(string simno)
         {
             return Json(cmcls.SearchValidity(simno , branchcode,usertype), JsonRequestBehavior.AllowGet);
         }


               [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult Searchcafno(string empName)
         {
             return Json(cmcls.SearchCafDAL(empName, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }
               [AcceptVerbs(HttpVerbs.Post)]
               public JsonResult SearchVoucher(string empName)
               {
                   return Json(cmcls.SearchVOUCHERDAL(empName, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
               }
        

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult SearchSimch_no(string empName, string country )
         {
             return Json(cmcls.SearchSimFromChallanOutMaster(empName,country ,usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }


        

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult SearchPlan()
         {
             return Json(cmcls.SearchPlanDAL("", usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult SearchTariff(string county)
         {
             return Json(cmcls.Tariffcodefill(usertype, sessid, branchcode, county), JsonRequestBehavior.AllowGet);
         }

        

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult searchauitocomcustname(string searchauitocomcustname)
         {
             return Json(cmcls.searchauitocomcustname(searchauitocomcustname),  JsonRequestBehavior.AllowGet);

         }

         [OutputCache(Duration=0)]
         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult search_ChallanOutSimno(string country)
         {
             var custd = cmcls.SimSearch(usertype, country, sessid, branchcode);
             return Json(custd, JsonRequestBehavior.AllowGet);
         }

         public ActionResult RenderIndex()
         {
             return PartialView("pv_uploaddata");            
         }

         public ActionResult RenderSearchIndex()
         {

             return PartialView("pv_search");

         }

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult auto_number()
         {
             return Json(cmcls.autotfno(branchcode, sessid));
         }
         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult cafnum(string cafno)
         {
             st_cafno = "";
             st_cafno = cafno;
             return Json("ab", JsonRequestBehavior.AllowGet);
         }

         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult AllSearch(string option , string cname , string c_name)
         {

             return Json(cmcls.all_search(option, cname , c_name ,usertype ,sessid ,branchcode ) , JsonRequestBehavior.AllowGet);
         }


         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult AccountSearch(string option, string accountno)
         {

             return Json(cmcls.AC_Search(option, accountno , usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }


         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult ActivePostpond()
         {

             return Json(cmcls.Active_Postpond( usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }
         //
         [AcceptVerbs(HttpVerbs.Post)]
         public ActionResult CAFSERACH(string simno)
         {

             return Json(cmcls.sim_no_Search(simno, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
         }

       

         [HttpPost]
         public ContentResult UploadFiles()
         {

             string files_Upload = string.Empty;
             var r = new List<UploadFilesResult>();

             if (!System.IO.Directory.Exists(Server.MapPath("~/App_Data/UserFile/" + st_cafno)))
             {
                 System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/UserFile/" + st_cafno));
             }

            
                 foreach (string file in Request.Files)
                 {
                     HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                     if (hpf.ContentLength == 0)
                         continue;

                     string savedFileName = Path.Combine(Server.MapPath("~/App_Data/UserFile/" + st_cafno), Path.GetFileName(hpf.FileName));
                     hpf.SaveAs(savedFileName);

                     files_Upload =  hpf.FileName + "," + files_Upload;

                     r.Add(new UploadFilesResult()
                     {
                         Name = hpf.FileName,
                         Length = hpf.ContentLength,
                         Type = hpf.ContentType
                     });

                 }
                 files_all += files_Upload;
                 return Content("{\"name\":\"" + st_cafno + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");

             
                   
         }
    }
}
 