using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using DataAccessLeyer.EF;

namespace PresantationAccessLeyer.Controllers
{
    public class UserNameController : Controller
    {
        //
        // GET: /UserName/
        ADD_UserNameDAL  acicls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public UserNameController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            acicls = new ADD_UserNameDAL();
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

        [OutputCache(Duration=0)]
         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ret_usertype()
        {
            return Json(usertype ,JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ret_branchcode()
        {
            return Json(branchcode, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ret_partnername()
        {
            return Json(partnername, JsonRequestBehavior.AllowGet);
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }


        public JsonResult createuservalidate(string date, string Branchcode, string partnername, string User_Code, string UserType,
      string UserName, string password, string address, string mobileno, string emailid, string rolesetting, string valueassign)
        {
             string Msg = string.Empty;

          //   string pass = EncodePasswordToBase64(password);
            try
            {
                Msg = acicls.createuser("Insert", date, Branchcode, partnername, User_Code, UserType, UserName, password, "",
                      address, mobileno, emailid, rolesetting, valueassign);
            }
            catch (Exception ex)
            {

                Msg = ex.InnerException.Message;
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);
          

        }


        public JsonResult Updateuservalidate( string User_Code, string password, string newpass)
        {
            string Msg = string.Empty;
            try
            {
                Msg = acicls.createuser("ChangePassword", "11/11/2014", branchcode, "", User_Code, usertype, "", password,newpass,"", "", "", "", "");
            }
            catch (Exception ex)
            {

                Msg = ex.InnerException.Message;
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);


        }

        public JsonResult AllotRole(string strexecname, string strrolename)
        {
            string Msg = string.Empty;
            try
            {
                Msg = acicls.createuser("RoleAssign", "11/11/2014", branchcode, "", strexecname, "", "", "", "", "", "", "", strrolename, "");
            }
            catch (Exception ex)
            {

                Msg = ex.InnerException.Message;
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);


        }

          [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult auto_number()
         {
             return Json(acicls.autouserid(branchcode, sessid));
         }

          [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult usercodeautosearch(string empName)
          {
              DataAccessLeyer.EF.inventoryforwebappEntities inv = new inventoryforwebappEntities();
              List<User_Name> custd = null;
              switch (usertype)
              {

                  case "ADMIN":
                      custd = inv.User_Name
                       .Where(em => em.User_Code.Contains(empName)).ToList();
                      break;

                  case "PARTNER":
                      custd = inv.User_Name
                       .Where(em => em.Branchcode == branchcode && em.User_Code.Contains(empName)).ToList();
                      break;

                  case "EXECUTIVE":
                      custd = inv.User_Name
                       .Where(em => em.Branchcode == branchcode && em.User_Code == sessid && em.User_Code.Contains(empName)).ToList();
                      break;
              }
              return Json(custd, JsonRequestBehavior.AllowGet);

          }
        
    }
}
