using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System;
using System.Data;


namespace PresantationAccessLeyer.Controllers
{
    public class SendMailerController : Controller
    {
        //
        // GET: /SendMailer/

        //public ActionResult Index()
        //{
        //    return View();
        //}
         static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = "";
         public SendMailerController()
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
    
        public ViewResult Inde_x1(Models.MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("dubey.mayank4@gmail.com", "");
                
             

                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Index", _objModelMail);
            }
            else
            {
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Send_mail(string email_To, string subject_b, string body_b, string attach_ment, string Adminem, string Exuctem)
        {
            DataAccessLeyer.DAL.BILLINSERT_DAL bcls = new DataAccessLeyer.DAL.BILLINSERT_DAL();

            string strqry = "select * from MAILSETTINGTABLE where branchcode = '"+branchcode+"'";
            DataTable dt =   bcls.settingval(strqry);

            string smtpAddress = dt.Rows[0]["SMTP_ADDRESS"].ToString() ;// "smtp.gmail.com";
            int portNumber = Convert.ToInt32(dt.Rows[0]["PORT_NUMBER"].ToString()); //  587;
            bool enableSSL = true;

            string emailFrom = dt.Rows[0]["EMAIL_FROM"].ToString(); // "donotreplysansoft@gmail.com";
            string password = dt.Rows[0]["PASSWORD"].ToString(); // "sansoft@123";
            string emailTo = email_To; // "dubey.mayank4@gmail.com";
            string subject = subject_b; // "Hello";
            string body = body_b; // "Hello, I'm just writing this to say Hi!";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.To.Add(Adminem);
                mail.To.Add(Exuctem);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.
                mail.Attachments.Add(new Attachment(Server.MapPath(attach_ment)));
              //  mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }

            return Json("Mail Send Successfully",JsonRequestBehavior.AllowGet);
        }

    }
}
