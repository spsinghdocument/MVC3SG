using System;
using System.Web.Mvc;
using System.Data.SqlClient;
using DataAccessLeyer.DAL;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Web;

namespace PresantationAccessLeyer.Controllers
{
    public class HomeController : Controller
    {
       
        //
        // GET: /Home/
        SqlConnection con;
        SqlCommand cmd;
        LoginDAL lcls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        public HomeController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);

            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            lcls = new LoginDAL();
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
        public JsonResult PermissionFun( string strolename)
        {
            var v = lcls.PermissionDAL(System.Web.HttpContext.Current.Session["usertype"].ToString(), System.Web.HttpContext.Current.Session["branchcode"].ToString(), System.Web.HttpContext.Current.Session["usercode"].ToString(), strolename);
            return Json(v, JsonRequestBehavior.AllowGet);
        }
       [AcceptVerbs(HttpVerbs.Post)]
       public JsonResult LoadUserType()
       {
           var v = usertype; 
           return Json(v, JsonRequestBehavior.AllowGet);
       }

       [AcceptVerbs(HttpVerbs.Post)]
       public JsonResult SearchUserRoleName()
       {
           string strqry = "select rolesetting from User_Name where User_Code = '" + sessid + "'";
           var v = lcls.SearchRoleNameDAL(strqry);
            string name = string.Empty ;
           foreach (var item in v)
           {
               System.Web.HttpContext.Current.Session["rolename"] = item.RoleSetting.Trim();
              
           }
         
           return Json(v, JsonRequestBehavior.AllowGet);
       }

        

        

      
        
    
        
    }

   
}
