using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLeyer.EF;
using DataAccessLeyer.DAL;
using System.IO;
using PresantationAccessLeyer.Models;
using System.Data;

namespace PresantationAccessLeyer.Controllers
{
    public class AddTariffController : Controller
    {
        inventoryforwebappEntities inv;
        Add_TariffDAL atcls; private static string st_cafno;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        //
        // GET: /AddTariff/

        public AddTariffController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            inv = new inventoryforwebappEntities();
            atcls = new Add_TariffDAL();

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
                //  return PartialView("pv_printtariff");
            }

        }
        public ActionResult RenderPv_up()
        {
            return PartialView("pv_tariffupload");
        }



        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ShowEmployee()
        {
            return Json(inv.ADDTARIFs.Where(EM => EM.A == "10"),
                JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SaveEmployee(string TARIFF_CODE, string COUNTRY, string PLANTYPE, string CURRENCY, string CHARGESTYPE, string VALUE, string TALK_VALUE,
                         string PERMINCHARGES, string VALIDITY_DAYS, string COUNTRY_FROM, string COUNTRY_TO, string Ch_type)
        {
            string Msg = string.Empty;
            try
            {
                int t = inv.addtarif_Proc_INSERT("Insert", 1, TARIFF_CODE, COUNTRY, PLANTYPE, CURRENCY, CHARGESTYPE, VALUE, TALK_VALUE,
                     PERMINCHARGES, VALIDITY_DAYS, COUNTRY_FROM, COUNTRY_TO, Ch_type, branchcode, sessid, "");
                if (t > 0)
                {
                    Msg = "Record Inserted Successfully..";
                }
                else
                    Msg = "Record Not Saved..";
            }
            catch (Exception ex)
            {
                Msg = ex.Message;

            }
            return Json(Msg,
                JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload_Tariff(string TARIFF_CODE, string COUNTRY, string PLANTYPE, string CURRENCY)
        {
            string Msg = string.Empty;
            try
            {
                int t = inv.addtarif_Proc_INSERT("Upload", 1, TARIFF_CODE, COUNTRY, PLANTYPE, CURRENCY, "", "", "", "", "", "", "", "", branchcode, sessid, st_cafno);
                if (t > 0)
                {
                    Msg = "Record Inserted Successfully..";
                    st_cafno = "";
                }
                else
                    Msg = "Record Not Saved..";
            }
            catch (Exception ex)
            {
                Msg = ex.Message;

            }
            return Json(Msg,
                JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Load_DataTariff(string tariffcode)
        {
            string strqry = "select * from ADDTARIF where TARIFF_CODE = '" + tariffcode + "'";
            return Json(atcls.searchdata_addtariff(strqry), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Tariffcode_fillData(string empName)
        {
            return Json(atcls.Tariffcode_fill(empName, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult DeleteEmployee(int empId)
        //{
        //    string Msg = string.Empty;
        //    try
        //    {
        //        Employee Emp = db.Employees.First(em => em.EmpId == empId);
        //        db.DeleteObject(Emp);
        //        db.SaveChanges();
        //        Msg = "Record has Deleted";
        //    }
        //    catch (Exception ex)
        //    {
        //        Msg = ex.Message;

        //    }
        //    return Json(Msg,
        //        JsonRequestBehavior.AllowGet);
        //}


        public FilePathResult Image()
        {
            string filename = Request.Url.AbsolutePath.Replace("/home/image", "");
            string contentType = "";
            var filePath = new FileInfo(Server.MapPath("~/App_Data") + filename);

            var index = filename.LastIndexOf(".") + 1;
            var extension = filename.Substring(index).ToUpperInvariant();

            // Fix for IE not handling jpg image types
            contentType = string.Compare(extension, "JPG") == 0 ? "image/jpeg" : string.Format("image/{0}", extension);

            return File(filePath.FullName, contentType);
        }

        [HttpPost]
        public ContentResult UploadFiles()
        {

            string files_Upload = string.Empty;
            var r = new List<UploadFilesResult>();

            //ServerManager iisManager = new ServerManager();
            //Application app = iisManager.Sites[0].Applications["wwwroot"];
            //app.VirtualDirectories.Add("/VDir", "/VDir/MyVDir");

            //iisManager.CommitChanges();

            if (!System.IO.Directory.Exists(Server.MapPath("~/App_Data/Add Tariff/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/Add Tariff/"));
            }


            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data/Add Tariff/"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);
                st_cafno = hpf.FileName;
                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });

            }

            return Content("{\"name\":\"" + st_cafno + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");


            //  return Content("{\"name\":\"" + app.ToString() + "\",\"type\":\"" + "bc" + "\",\"size\":\"" + string.Format("{0} bytes", 1) + "\"}", "application/json");
        }

        public void TARIFFPRINT(string tariffcode)
        {
            Add_Tariffcls ads = new Add_Tariffcls();
            BILLDAL bdl = new BILLDAL();
            System.Data.DataTable dt = bdl.tariff_dataFill(tariffcode);
            ads.TARIFFCODE = dt.Rows[0]["TARIFF_CODE"].ToString();
            ads.COUNTRY = dt.Rows[0]["country"].ToString();

            DataRow[] drowrental = dt.Select("chargestype = 'RENTAL'", "");
            foreach (var item in drowrental)
            {
                ads.RENTAL = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }

            DataRow[] drowvalidity = dt.Select("chargestype = 'VALIDITY'", "");
            foreach (var item in drowvalidity)
            {
                ads.VALIDITY = item["value"] + " " + "DAYS";
            }

            DataRow[] drowfreeval = dt.Select("chargestype = 'FREE VALUE'", "");
            foreach (var item in drowfreeval)
            {
                ads.FREEVALUE = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }

            DataRow[] drowincoming = dt.Select("chargestype = 'INCOMING CALL'", "");
            foreach (var item in drowincoming)
            {
                ads.INCOMING = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }


            DataRow[] drowlocaloutgoing = dt.Select("chargestype = 'LOCAL OUTGOING CALL'", "");
            foreach (var item in drowlocaloutgoing)
            {
                ads.LOCALOUTGOING = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
                ads.CURRENCY = item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString();
            }

            DataRow[] drowintcallindia = dt.Select("chargestype = 'INDIA OUTGOING CALL'", "");
            foreach (var item in drowintcallindia)
            {
                ads.INTCALL = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }
            DataRow[] drowlocalsms = dt.Select("chargestype = 'LOCAL SMS'", "");
            foreach (var item in drowlocalsms)
            {
                ads.LOCALSMS = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }
            DataRow[] drowintsms = dt.Select("chargestype = 'INTL SMS'", "");
            foreach (var item in drowintsms)
            {
               // ads.INTSMS = item["value"].ToString();
                ads.INTSMS = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }

            DataRow[] drowcug = dt.Select("chargestype = 'CUG CALLING'", "");
            foreach (var item in drowcug)
            {
               // ads.CUG = item["value"].ToString();
                ads.CUG = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }

            DataRow[] drowdatause = dt.Select("chargestype = 'DATA PAY PER USE'", "");
            foreach (var item in drowdatause)
            {
               // ads.DATAPER = item["A"].ToString();
                ads.DATAPER = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString();
            }
            DataRow[] drowboltans = dt.Select("chargestype = 'BOLTONS'", "");
            foreach (var item in drowboltans)
            {
                ads.Add(new ADD_TARIFFCLS1
                {
                    DATANAME = item["A"].ToString(),
                    DATAVALIDITY = item["Validity_Days"].ToString(),
                    DATAUSE = (item["CURRENCY"].ToString() == "?" ? "INR" : item["CURRENCY"].ToString()) + " " + item["value"].ToString()
                });

            }

            System.Web.HttpContext.Current.Session["dexcel"] = ads;

        }

        public ActionResult TARIFF_PRINT()
        {
            return PartialView("pv_printtariff", System.Web.HttpContext.Current.Session["dexcel"]);
        }
        public JsonResult deletedata(string sno, string tcode)
        {
            string Msg = "";
            string strdel = "delete from addtarif where tariff_code = '" + tcode + "' and sno = " + sno + "";
            int t = inv.ExecuteStoreCommand(strdel);
            if (t > 0)
            {
                Msg = "Record Deleted Successfully";
            }
            else
            {
                Msg = "Please Try Again";
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult update_data(string sno, string tcode, string type, string value, string validity)
        {
            string Msg = "";
            string strdel = "update addtarif set value = '" + value + "' , A = '" + type + "',validity_days = '" + validity + "' where tariff_code = '" + tcode + "' and sno = " + sno + "";

            int t = inv.ExecuteStoreCommand(strdel);
            if (t > 0)
            {
                Msg = "Record Updated Successfully";
            }
            else
            {
                Msg = "Please Try Again";
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
    }
}
