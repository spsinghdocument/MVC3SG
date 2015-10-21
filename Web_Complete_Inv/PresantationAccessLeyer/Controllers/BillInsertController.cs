using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using PresantationAccessLeyer.Models;
using System.IO;

namespace PresantationAccessLeyer.Controllers
{
    public class BillInsertController : Controller
    {
        //
        // GET: /BillInsert/
        BILLINSERT_DAL bcls; private static string files_all = string.Empty; private static string st_cafno = string.Empty; 
        CustomerList cst; private static string pf_file = string.Empty;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = "";
        public BillInsertController()
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
         
           
            bcls = new BILLINSERT_DAL();
        }
        public ActionResult Bill_Insert()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdata(string  cafno,string billno,string  mobileno,string country)
        {
            string Msg = bcls.insertdatadal("Insert", cafno, billno, mobileno, sessid, country, files_all, branchcode);
            files_all = "";
           return Json(Msg, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult settingInsertformail(string f, string SMTP_ADDRESS, string PORT_NUMBER, bool ENABLE_SSL, string EMAIL_FROM,
           string PASSWORD, string SUBJECT, string BODY)
        {
            string msg = "";
            msg = bcls.insertmailsettingsDAL("Insert", SMTP_ADDRESS, PORT_NUMBER, ENABLE_SSL, EMAIL_FROM, PASSWORD, SUBJECT, BODY, sessid, branchcode);
            return Json(msg ,JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult sendmail(string  cafno)
        {
            return Json(bcls.sendmailDAL(cafno, branchcode) ,JsonRequestBehavior.AllowGet) ;
        }

        public ActionResult RenderIndex()
        {
            return PartialView("pv_uploadbill");
        }
         [AcceptVerbs(HttpVerbs.Post)]
        public void caf_no(string cafno)
        {
             st_cafno = cafno;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult excelresult()
        {

            return Json(bcls.ExecllChkVal(usertype ,branchcode), JsonRequestBehavior.AllowGet);
            
        }
        public FileResult DisplayPDF()
        {
            return File(pf_file, "application/pdf");
        }
        public ActionResult render_pv()
        {
            return PartialView("pv_sendmailler");
        }

        public JsonResult show(string pdf)
        {
            string[] strpath = pdf.Split(',');

            cst = new CustomerList();
            //pf_file = "/App_Data/" + pdf;
            pf_file = "/App_Data/Billcaf/" + strpath[0] + "/" + strpath[1];
            cst.ImageUrl = "/App_Data/Billcaf/" + strpath[0] + "/" + strpath[1];
            return Json(cst.ImageUrl, JsonRequestBehavior.AllowGet);
        }


        public JsonResult mailattctment(string cafno)
        {

            string[] strpath = cafno.Split(',');

            cst = new CustomerList();
           
            pf_file = "/App_Data/Billcaf/" + strpath[0] + "/" + strpath[1];
            return Json(pf_file, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ContentResult UploadFiles()
        {

            string files_Upload = string.Empty;
            var r = new List<UploadFilesResult>();

            if (!System.IO.Directory.Exists(Server.MapPath("~/App_Data/Billcaf/" + st_cafno)))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/Billcaf/" + st_cafno));
            }


            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data/Billcaf/" + st_cafno), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                files_Upload = hpf.FileName;

                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });
                files_all = files_Upload;
            }
           
            return Content("{\"name\":\"" +  r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");



        }

    }
}
