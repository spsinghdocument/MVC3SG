using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ReportManagement;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using DataAccessLeyer.DAL;
using PresantationAccessLeyer.Models;
using System.Collections;

using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Microsoft.VisualBasic;
using Zayko.Finance;

namespace PresantationAccessLeyer.Controllers
{
    public class BillController : Controller
    {
        
        BILLDAL cld;
        private static string st_cafno; private static string files_all = string.Empty;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = ""; string useEmail = "";
        ClientMasterDAL cmcls;
        //inventory_MVCEntities imv;
        private static List<string> lsts = new List<string>();

        public BillController()
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
            useEmail = Convert.ToString(System.Web.HttpContext.Current.Session["useremailid"]);
            cld = new BILLDAL();           
        }

        public ActionResult Bill_Index()
        {
            System.Web.HttpContext.Current.Session["fname"] = "";
            return View();
           
        }
        public void loaddata()
        {
            readpdftext();
            return;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=D:\\Dropboxdata\\sp bil.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //  GridView1.RenderControl(hw);
            List<HtmlTextWriter> lst = new List<HtmlTextWriter>();
            lst.Add(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();

        }

        public void readpdftext()
        {
            pdfreader_i.ExtractTextFromPdf("");
        }

        public void insertdata(string date, string username, string user, string billNo, string accountCode, string country, string agreementno, string mobileno,
           string billdate, string duedate, string billperiod, string currency1, string currency2, string rental, string useges, string aopfubs,
           string totalamount, string amountininr, string servicetax, string amountpayable, string afterduedate, string conversuin, string excelFile, string branchcode, string loginusercode)
        {
            string Msg = cld.file_Data("Insert", date, "", "", billNo, accountCode, country, agreementno, mobileno, billdate, duedate,
                  billperiod, currency1, currency2, rental, useges, aopfubs, totalamount, amountininr, servicetax, amountpayable, afterduedate, conversuin, excelFile, branchcode, loginusercode);


        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult PROFILEDATA_VALIDATE(string PROFILE_NAME, string A, string B, string C, string D, string E, string F, string G, string H)
        {
            string MSG = cld.PROFILEDATA(PROFILE_NAME, A, B, C, D, E, F, G, H, branchcode, sessid);
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }

        public JsonResult search_datawithcaf(string cafno)
        {
            string strqry = "select  cl.customeracno , cl.Country, cd.mobileno ,cd.customeraddress ,cd.customername, cd.emailid  from ClientMaster cl  " +
                            " inner join customerdetailstable cd on cl.customeracno = cd.customeracno where cl.cafno = '" + cafno + "' ";
            return Json(cld.withcafno(strqry), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SearchTariff(string country)
        {
            return Json(cld.Tariffcodefill_withcountry(country, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }


        //tarifupload
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult tarifselect_code(string tcode)
        {
            return Json(cld.tarifupload(tcode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Bill_Index(HttpPostedFileBase file)
        {
            if (file == null)
            {
             //   string.Format ("<script>alert('Please select file')</script>");
                return View();

            }
            if (file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                var filepath = Path.Combine(Server.MapPath("~/App_Data"), filename);
                file.SaveAs(filepath);
                System.Web.HttpContext.Current.Session["fname"] = filename;
            }
            return View();
            //return Redirect("/Viewer/WebForm1.aspx");
        }




        //==========================================================================================================================================


        public ActionResult RenderIndex()
        {

            return PartialView("pv_upload");

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult excelimp(string fnameval)
        {
            System.Data.DataTable ds = new System.Data.DataTable();
          
            SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;");
           
                string query1 = "select  * from spExe2 ";
                System.Data.DataTable dt = new System.Data.DataTable();
                using (SqlCommand cmd = new SqlCommand(query1, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }
                if (dt.Rows.Count == 0)
                {
                    return Json("Please Download Exe than upload excel file ..", JsonRequestBehavior.AllowGet);
                }
                ArrayList myArrayList = new ArrayList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                     List<string> lst = new List<string>();
                    try
                    {
                         
                        lst.Add(dt.Rows[i][0].ToString());
                        lst.Add(dt.Rows[i][1].ToString());
                        lst.Add(dt.Rows[i][2].ToString());
                        try
                        {
                            var timeSpan = TimeSpan.FromDays(Convert.ToDouble(dt.Rows[i][3].ToString()));
                            int hh = timeSpan.Hours;
                            int mm = timeSpan.Minutes;
                            int ss = timeSpan.Seconds;
                            string dur = string.Format("{0} :{1} :{2}", hh, mm, ss);
                            lst.Add(dur);
                        }
                        catch
                        {
                            lst.Add(dt.Rows[i][3].ToString());
                           
                        }
                       
                        lst.Add(dt.Rows[i][4].ToString());
                        lst.Add(dt.Rows[i][5].ToString());
                        lst.Add(dt.Rows[i][6].ToString());

                    }
                    catch { }
                    myArrayList.Add(lst);
                }



             return Json(myArrayList, JsonRequestBehavior.AllowGet);
        }


        


        [HttpPost]
        public JsonResult data(Listcls ast)
        {
            return Json(lsts, JsonRequestBehavior.AllowGet);
        }


        public void abc(string caf, string phone, string user, string address, string yoStayconnectno, string country, string agreementno, string billno, string billDate, string paymentmode, string billPeyiod, string seviceTaxno, string[] dataaa, string fixdcharges, string rental, string otherchg, string total, string servicet, string totalamtt,string from,string to , string currate ,string symbol)
        //public void abc(List<object> dataaa, string caf) datapaeruser
        {
            CustomerList custobj = new CustomerList();
            string[] spt = { caf, phone, user, address, yoStayconnectno, country, agreementno, billno, billDate, paymentmode, billPeyiod, seviceTaxno, fixdcharges, rental, otherchg, total, servicet, totalamtt,from,to,currate,symbol };

            // sno = ++i1,
            custobj.a = spt[0]; custobj.b = spt[1]; custobj.c = spt[2];
            custobj.d = spt[3]; custobj.e = spt[4]; custobj.f = spt[5];
            custobj.g = spt[6]; custobj.h = spt[7]; custobj.i = spt[8];
            custobj.j = spt[9]; custobj.k = spt[10] == "NaN" ? "" : spt[10]; custobj.l = spt[11] == "NaN" ? "" : spt[11];
            custobj.m = spt[12] == "NaN" ? "" : spt[12];
            
          //  custobj.n = (((Convert.ToDecimal(System.Web.HttpContext.Current.Session["grandt_val"])*Convert.ToDecimal( currate)) - (Convert.ToDecimal(System.Web.HttpContext.Current.Session["Free_Value"]) ))) > 0 ? "0" :
          
            custobj.n =     Convert.ToString(System.Web.HttpContext.Current.Session["rntl"]);

            custobj.o = Convert.ToString(System.Web.HttpContext.Current.Session["datavalue"]) == "" ? "0" : Convert.ToString(Convert.ToDecimal(System.Web.HttpContext.Current.Session["datavalue"]) * Convert.ToDecimal(currate));
            custobj.p = spt[15] == "NaN" ? "" : spt[15]; custobj.q = spt[16] == "NaN" ? "" : spt[16]; custobj.u = spt[17] == "NaN" ? "" : spt[17];
            custobj.s = spt[18]; custobj.t = spt[19]; custobj.w = spt[20]; custobj.z = spt[21];


            dynamic d = System.Web.HttpContext.Current.Session["grd"];
            foreach (var  item in d)
            {
                 custobj.Add(new Customer
                {
                    // sno = ++i1,
                    DATETIME = item.DATETIME,  //spt[0],
                    NUMBER = item.NUMBER,
                    TYPE = item.TYPE,
                    DURATION = item.DURATION,
                    UNITS = item.UNITS,
                    RATE = item.RATE,
                    COST = item.COST,

                });
                 //if (Information.IsNumeric(item.COST) ==true)
                 //{
                 //    custobj.m = item.COST == "NaN" ? "" : item.COST;
                 //}
            }

          //  custobj.m = Convert.ToString(Convert.ToDecimal(currate) *   Convert.ToDecimal( System.Web.HttpContext.Current.Session["grandt_val"]));
            custobj.m = (((Convert.ToDecimal(System.Web.HttpContext.Current.Session["grandt_val"]) * Convert.ToDecimal(currate)) -  (Convert.ToDecimal(System.Web.HttpContext.Current.Session["Free_Value"])))) < 0 ? "0" :
                Convert.ToString((((Convert.ToDecimal(System.Web.HttpContext.Current.Session["grandt_val"]) * Convert.ToDecimal(currate)) - (Convert.ToDecimal(System.Web.HttpContext.Current.Session["Free_Value"])))));

            //List<string[]> lst = new List<string[]>();
            //lst.Add(spt);
            
           
            //for (int i = 0; i < dataaa.Length; i++)
            //{
            //    spt = dataaa[i].Split(',');
            //    custobj.Add(new Customer
            //    {
            //        // sno = ++i1,
            //        billno = spt[0],
            //        Validity = spt[1],
            //        itemtype = spt[2],
            //        simno = spt[3],
            //        country = spt[4],
            //        apn = spt[5],
            //        puk = spt[6],
            //    });
            //    lst.Add(spt);
            //}

            System.Web.HttpContext.Current.Session["excel"] = custobj;
            System.Web.HttpContext.Current.Session["fname"] = "";
           
        }
        public ActionResult Rend_ndex()
        {
            return PartialView("pv_payslip", System.Web.HttpContext.Current.Session["excel"]);
        }

        public JsonResult nextval()
        {
            return Json(files_all ,JsonRequestBehavior.AllowGet);
        }

        public ActionResult PV_Profile()
        {
            return PartialView("pv_newprofile");
        }

        [HttpPost]
        public ContentResult UploadFiles()
        {

            string files_Upload = string.Empty;
            var r = new List<UploadFilesResult>();

            if (!System.IO.Directory.Exists(Server.MapPath("~/App_Data/Billexcel/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/Billexcel/"));
            }


            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data/Billexcel/"), Path.GetFileName(hpf.FileName));
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

            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
        }


        public ActionResult sendmail_data()
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "donotreplysansoft@gmail.com";
            string password = "sansoft@123";
            string emailTo = "spsinghdocument@gmail.com";
            string subject = "Hello";
            string body = "Hello, I'm just writing this to say Hi!";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                // mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //  mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }

            return View();
        }



        public JsonResult currencylist()
        {
            string[] desc = new string[CurrencyList.Count];
            CurrencyList.Descriptions.CopyTo(desc, 0);
            return Json(desc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult code_get(string from_val, string to_val)
        {
        
            double min_v = 0;
            try
            {
                CurrencyData cd = new CurrencyData(CurrencyList.GetCode(from_val), CurrencyList.GetCode(to_val));
                CurrencyCode cc = new CurrencyCode();
                cc.AdjustToLocalTime = true;              
                string st1 = CurrencyList.GetCode(from_val);
                string st2 = CurrencyList.GetCode(to_val);
                IList<CurrencyData> list = new List<CurrencyData>(1);
                cc.GetCurrencyData(ref cd);
                 min_v = cd.Rate;
                double max_v = cd.Max;
                
            }
            catch (Exception)
            {
               
                throw;
            }
            return Json(min_v, JsonRequestBehavior.AllowGet);
        }

    }
}
