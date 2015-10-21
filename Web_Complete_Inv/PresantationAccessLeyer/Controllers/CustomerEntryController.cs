using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using DataAccessLeyer.EF;
using System.Text.RegularExpressions;


namespace PresantationAccessLeyer.Controllers
{
    public class CustomerEntryController : Controller
    {
        //
        // GET: /CustomerEntry/
        CustomerEntryDAL cecls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public CustomerEntryController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            cecls = new CustomerEntryDAL();
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
        public JsonResult insertcustomervalidate(string customeracno, string customername, string customeraddress,
        string emailid, string passportno, string mobileno, string alternateno)
        {
            string Msg = string.Empty;

            Msg = cecls.insertcustomertable("Insert", customeracno, customername, customeraddress,
                    emailid, "", passportno, mobileno, alternateno, sessid, branchcode);

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Updatecustomervalidate(string customeracno, string customername, string customeraddress,
        string emailid, string passportno, string mobileno, string alternateno)
        {
            string Msg = string.Empty;

            Msg = cecls.insertcustomertable("Update", customeracno, customername, customeraddress,
                    emailid, "", passportno, mobileno, alternateno, sessid, branchcode);

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult auto_number()
        {
            return Json(cecls.autobillno(branchcode, branchcode));
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Customerentry_Search(string option, string strval)
        {
            string QryDevice = string.Empty;
            string StrQry = string.Empty;
            if (option == "CUSTOMER ACNO SEARCH")
            {
                QryDevice = "SELECT   customeracno, customername, customeraddress, emailid, website, passportno, mobileno, alternateno ,branchcode  FROM  customerdetailstable where   customeracno='" + strval + "' ";
            }
            else if (option == "CUSTOMER NAME SEARCH")
            {
                QryDevice = "SELECT   customeracno, customername, customeraddress, emailid, website, passportno, mobileno, alternateno,branchcode  FROM  customerdetailstable where  customername='" + strval + "' ";
            }
            else if (option == "MOBILE NO SEARCH")
            {
                QryDevice = "SELECT   customeracno, customername, customeraddress, emailid, website, passportno, mobileno, alternateno,branchcode  FROM  customerdetailstable where  mobileno='" + strval + "' ";
            }
            else if (option == "ALL")
            {
                QryDevice = "SELECT   customeracno, customername, customeraddress, emailid, website, passportno, mobileno, alternateno ,branchcode FROM  customerdetailstable  where 1=1 ";
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

            TempData["Qry"] = StrQry;
            var v = cecls.customerserach(StrQry);
            System.Web.HttpContext.Current.Session["customerentrydata"] = v;

            return Json(v, JsonRequestBehavior.AllowGet);

        }

        
           [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult searchautocomcustacno(string empName)
        {
         
      return Json(cecls.customeracno(empName, usertype, branchcode, sessid), JsonRequestBehavior.AllowGet);
           
        }

           [AcceptVerbs(HttpVerbs.Post)]
           public JsonResult searchautocomcustname(string empName)
           {

               return Json(cecls.customername(empName ,usertype ,branchcode ,sessid), JsonRequestBehavior.AllowGet);

           }

           [AcceptVerbs(HttpVerbs.Post)]
           public JsonResult searchautocomcustmobile(string empName)
           {
               DataAccessLeyer.EF.inventoryforwebappEntities inv = new inventoryforwebappEntities();
               List<customerdetailstable> custd = null;
               switch (usertype)
               {

                   case "ADMIN":
                       custd = inv.customerdetailstables
                        .Where(em => em.mobileno.Contains(empName)).ToList();
                       break;

                   case "PARTNER":
                       custd = inv.customerdetailstables
                        .Where(em => em.branchcode == branchcode && em.mobileno.Contains(empName)).ToList();
                       break;

                   case "EXECUTIVE":
                       custd = inv.customerdetailstables
                        .Where(em => em.branchcode == branchcode && em.loginusercode == sessid && em.mobileno.Contains(empName)).ToList();
                       break;
               }
               return Json(custd, JsonRequestBehavior.AllowGet);

           }

  [AcceptVerbs(HttpVerbs.Post)]
  public JsonResult Emailvalidate(string email)
  {
      string Msg = "";
      if (email.Length > 0)
      {


          string s = "[a-z0-9A-z]@[a-zA-z0-9 ]*[.]";
          string s1 = email;
          Match aa = Regex.Match(s1, s);
          if (aa.Success)
          {
              Msg = "OK";
          }
          else
          {
              Msg = "Email id Not Valid";
          }
      }
      return Json(Msg, JsonRequestBehavior.AllowGet);
  }
 

    }
}
