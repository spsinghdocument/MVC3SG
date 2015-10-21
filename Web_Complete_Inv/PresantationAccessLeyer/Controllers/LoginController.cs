using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using DataAccessLeyer.EF;


namespace PresantationAccessLeyer.Controllers
{
    public class LoginController : Controller
    {
        inventoryforwebappEntities inv;
        LoginDAL lcls;
       // System.Threading.Thread login_t = new System.Threading.Thread(LoginValidate , );

        public LoginController()
        {
            inv = new inventoryforwebappEntities();
            lcls = new LoginDAL();
        }
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ret_pv()
        {
            return PartialView("pv_emailpassword");
        }

           [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult LoginValidate(string Ucode, string Password)
        {         
          List<User_Name> MstUsr = lcls.LoginUserDal(Ucode, Password);
            foreach (var item in MstUsr)
            {
                System.Web.HttpContext.Current.Session["usercode"] = item.User_Code.Trim().ToUpper();
                System.Web.HttpContext.Current.Session["usertype"] = item.UserType.Trim().ToUpper();
                System.Web.HttpContext.Current.Session["branchcode"] = item.Branchcode.Trim().ToUpper();
                System.Web.HttpContext.Current.Session["partnername"] = item.partnername.Trim().ToUpper();
                System.Web.HttpContext.Current.Session["username"] = item.UserName.Trim().ToUpper();

                System.Web.HttpContext.Current.Session["useremailid"] = item.emailid;
                //StaticVariables.usercode = item.User_Code.Trim().ToUpper();
                //StaticVariables.usertype = item.UserType.Trim().ToUpper();
                //StaticVariables.partnername = item.partnername.Trim().ToUpper();
                //StaticVariables.branchcode = item.partnername.Trim().ToUpper();
                ViewBag.key= item.User_Code.Trim().ToUpper();
            }
            string Msg = string.Empty;
          
            try
            {
                if (MstUsr.Count > 0)
                {                                      
                    Session["UserId"] = MstUsr[0].UserName;                  
                    Session["UserName"] = MstUsr[0].UserName;                  
                    Msg = "User";
                }
                else
                {                   
                        Msg = "User and Password Invalid";
                    
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
          //  return Json(inv.User_Name.Where(em => em.User_Code == Ucode && em.Password == Password));
        }


      

       }
}
