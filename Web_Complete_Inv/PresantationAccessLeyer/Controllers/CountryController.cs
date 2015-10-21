using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using DataAccessLeyer.EF;

namespace PresantationAccessLeyer.Controllers
{
    public class CountryController : Controller
    {
        //
        // GET: /Country/
        inventoryforwebappEntities inv;
        Add_CountryInsert ccls;

          protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
      
        public CountryController()
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
            ccls = new Add_CountryInsert();
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
        public JsonResult  Insert_Country(string Country, string isdcode, string Indiacalling, string countrycode, string currency, string impnotice, string importantnotice)
        {
           string Msg = string.Empty;
           Msg =  ccls.AddCountryDal("Insert" , Country.ToUpper().Trim(),  isdcode,  Indiacalling,  countrycode,  currency,  impnotice,  importantnotice );
           return Json( Msg ,JsonRequestBehavior.AllowGet);
            
        }

           [AcceptVerbs(HttpVerbs.Post)]
           public JsonResult Update_Country(string Country, string isdcode, string Indiacalling,  string currency, string impnotice, string importantnotice)
           {
               string Msg = string.Empty;
               Msg = ccls.AddCountryDal("Update", Country.ToUpper().Trim(), isdcode, Indiacalling, "", currency, impnotice, importantnotice);
               return Json(Msg, JsonRequestBehavior.AllowGet);

           }
           [AcceptVerbs(HttpVerbs.Post)]
           public JsonResult countryData(string country)
           {              
               string strqry = "select * from countrylist where country  = '" + country.ToUpper().Trim() + "' ";
               return Json(ccls.countrysearch(strqry), JsonRequestBehavior.AllowGet);
           }
       
    }
}
