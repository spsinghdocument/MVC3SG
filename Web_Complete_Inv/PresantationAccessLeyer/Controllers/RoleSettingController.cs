using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;


namespace PresantationAccessLeyer.Controllers
{
    public class RoleSettingController : Controller
    {
        //
        // GET: /RoleSetting/
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";
        RolesettingDAL rlcls;
        public RoleSettingController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            rlcls = new RolesettingDAL();
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
        public JsonResult Load_Datarole(string rolename)
        {
            string strqry = "select * from Rollsetting_subtab where rolsetingname = '" + rolename + "'";
            return Json(rlcls.searchdata_Role(strqry), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Load_Datarolename()
        {
            string strqry = "select rolesettingname from Rollsetting_subtab ";
            return Json(rlcls.searchdata_Role(strqry), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertrole_data(string rolesettingname, string rolesettingcode)
        {
           
          string msg =  rlcls.insertsolename("Insert", 1, rolesettingname, rolesettingcode,sessid ,branchcode , "");
          return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updaterole_dataALL(string formname, bool insert, bool view, bool edit, bool delete_1, bool all, string rolsetingname)
        {
            string Msg = string.Empty;
            var msg = rlcls.AllPermissionInsert("Update", 1, formname, insert, view, edit, delete_1, all, rolsetingname, sessid, partnername);
            if (msg.Count > 0)
            {
                Msg = "Record Update Successfully........";
            }
            else
                Msg = "Record Not Update........";
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertrole_dataALLAutofill(string rolsetingname)
        {
            var msg = rlcls.AllPermissionInsert("Insert", 1, "", false, false, false, false, false, rolsetingname, sessid, partnername);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


          [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertrole_RoleNameALLAutofill()
        {

            return Json(rlcls .GetRoleName(branchcode), JsonRequestBehavior.AllowGet);
        }

          [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult RoleAssigntoExecutive(string strexecname  , string strrolename)
          {
              return Json(rlcls.GetRoleName(branchcode), JsonRequestBehavior.AllowGet);
          }
        
    }
}
