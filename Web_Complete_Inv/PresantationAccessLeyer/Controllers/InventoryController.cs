using System;
using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/
        InventoryDAL icls;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = "";

        public InventoryController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
           
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);

            icls = new InventoryDAL();
        }
        public ActionResult Index()
        {
            if (sessid == "")
            {

                return RedirectToAction("Index" , "Login");
            }
            return View();
        }

        public JsonResult search_country(string opt, string strval, string strdate1, string strdate2)
        {
            string strsubqry ="";
            string QueryDivice = "select ItemType,SimNo ,PhoneNo ,simcode ,PUK from PurchaseEntry where [Date] between '" + strdate1 + "' and '" + strdate2 + "'";
            switch (opt)
            {
                case "DATE WISE SEARCH" :
                    strsubqry = QueryDivice;
                    break;
                case "COUNTRY SEARCH":
                    strsubqry = QueryDivice + " and country = '" + strval + "'";
                    break;
                case "SIM NO SEARCH":
                    strsubqry = QueryDivice + " and simno = '" + strval + "'";
                    break;
            }

            return Json(icls.searchcountry(usertype, strsubqry, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }

    }
}
