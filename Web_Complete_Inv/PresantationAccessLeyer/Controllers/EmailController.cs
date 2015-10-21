using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresantationAccessLeyer.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = "";
        public EmailController()
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
        }
        public ActionResult E_mail()
        {
            return PartialView("pv_emailpassword");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Send_mail(string email_To)
        {
            DataAccessLeyer.DAL.BILLINSERT_DAL bcls = new DataAccessLeyer.DAL.BILLINSERT_DAL();
            string strqry1 = "select * from user_name where emailid = '" + email_To + "'";
            System.Data.DataTable dt1 = bcls.settingval(strqry1);
            if (dt1.Rows.Count == 0)
            {
                return Json("Mail Id Not Exist", JsonRequestBehavior.AllowGet);
            }

            string strqry = "select * from MAILSETTINGTABLE where branchcode = '" + dt1.Rows[0]["branchcode"].ToString() + "'";
            System.Data.DataTable dt = bcls.settingval(strqry);

           
            

            string smtpAddress = dt.Rows[0]["SMTP_ADDRESS"].ToString();// "smtp.gmail.com";
            int portNumber = Convert.ToInt32(dt.Rows[0]["PORT_NUMBER"].ToString()); //  587;
            bool enableSSL = true;

            string emailFrom = dt.Rows[0]["EMAIL_FROM"].ToString(); // "donotreplysansoft@gmail.com";
            string password = dt.Rows[0]["PASSWORD"].ToString(); // "sansoft@123";
            string emailTo = email_To; //  "spsinghdocument@gmail.com";
            string subject =  "Hello";
            string body = "Your  Password is : "+ dt1.Rows[0]["Password"].ToString(); // "Hello, I'm just writing this to say Hi!";

            using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
            {
                mail.From = new System.Net.Mail.MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.
               // mail.Attachments.Add(new System.Net.Mail.Attachment(""));
                //  mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }

            return Json("Mail Send Successfully", JsonRequestBehavior.AllowGet);
        }

    }
}
