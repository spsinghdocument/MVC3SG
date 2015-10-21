using System;
using System.Linq;
using System.Web.Mvc;
using DataAccessLeyer.EF;
using DataAccessLeyer.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace PresantationAccessLeyer.Controllers
{
    public class ChallanOutController : Controller
    {
        //
        // GET: /ChallanOut/
        SqlConnection con;
        SqlCommand cmd;
          inventoryforwebappEntities inv;
          Add_CountryInsert acicls;
        static long bl_np = 0;
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        PurchaseEntryDal pcls;
        ChallanDAL  ccls;
        public ChallanOutController()
        {
            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);
            if (sessid == "")
            {

                return;
            }
            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
         //   sessid = "a"; // Convert.ToString(Session["UserId"]);

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);
                pcls = new PurchaseEntryDal();
                ccls = new ChallanDAL();
                inv = new inventoryforwebappEntities();
                acicls = new Add_CountryInsert();
            
           
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

         [OutputCache(Duration = 0)]
         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult challaninsert(string bill_no, string c_date, string country, string simno, string AlotUser, string givento)
        {
            string Msg = ccls.challanoutvalidate(bill_no, c_date, country, "SIM", simno, AlotUser, sessid, branchcode, givento);
            return Json(Msg , JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult pv_c()
        {
            return View("pv_challanout");
        }


        public ActionResult pv_searchchallan()
        {
            return View("pv_challanoutsearch");
        }
   
         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseentry_Search(string country)
        {
            return Json(ccls.purchaseserachinchallan(country , usertype , branchcode , sessid), JsonRequestBehavior.AllowGet);
        }


        // call function in submit click  button

       
         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult purchaseentry_WIthSIMSearch(string simno)
         {
             return Json(ccls.purchaseserachinchallan_withsimno(simno, usertype, branchcode, sessid), JsonRequestBehavior.AllowGet);
         }

        
         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseentry_Searchbillno(string challanno)
        {
            if (challanno == "null")
            {
                return null;
            }
            var v = ccls.serachin_challanbill(challanno.Trim(), usertype, branchcode, sessid);
             System.Web.HttpContext.Current.Session["challandata"] = v;
            return Json(v, JsonRequestBehavior.AllowGet);
        }
    
         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult fillcombo()
        {
          return  Json(acicls.GetCountry(), JsonRequestBehavior.AllowGet);
        }

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult GetSim(string Countryname)
         {
             return Json(pcls.GetSim(Countryname), JsonRequestBehavior.AllowGet);
         }

       
         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult challan_Search(string option, string strval, string date1, string date2, string chkval)
         {
             string QryDevice = string.Empty;
             if (chkval == "ALL")
             {
                 if (option == "CHALLANNOSEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout  where cDate between '" + date1 + "' and '" + date2 + "' and challanno = '" + strval + "'";
                 }
                 else if (option == "COUNTRYSEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' and country='" + strval + "' ";
                 }
                 else if (option == "SIMNOSEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' and others='" + strval + "' ";
                 }
                 else if (option == "EXECUTIVECODESEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' and executivenameto='" + strval + "' ";
                 }
                 else if (option == "DATESEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' ";
                 }
             }
             else if (chkval == "ALL")
             {
                 if (option == "CHALLANNOSEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout  where status ='' cDate between '" + date1 + "' and '" + date2 + "' and challanno = '" + strval + "'";
                 }
                 else if (option == "COUNTRYSEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' and country='" + strval + "' ";
                 }
                 else if (option == "SIMNOSEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' and others='" + strval + "' ";
                 }
                 else if (option == "EXECUTIVECODESEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' and executivenameto='" + strval + "' ";
                 }
                 else if (option == "DATESEARCH")
                 {
                     QryDevice = "select distinct (challanno), cdate, itemtype , country , executivenameto from challanout where cDate between '" + date1 + "' and '" + date2 + "' ";
                 }
             }
             string multiqry = "";
             switch (usertype)
             {
                 case "ADMIN" :
                     {
                         multiqry = QryDevice;
                         break;
                     }
                 case "PARTNER":
                     {
                         multiqry = QryDevice +" and " + " branchcode = '" + branchcode + "' ";
                         break;
                     }
                 case "EXECUTIVE":
                     {
                         multiqry = QryDevice + " and " + " branchcode = '" + branchcode + "' and executivenameto ='" + sessid + "' ";
                         break;
                     }
             }
             var v = ccls.challanserach(multiqry);
             return Json(v, JsonRequestBehavior.AllowGet);

         }

 
         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult SearchData(string empName)
         {
            
         //var   v =   inv.challanouts.Where(em => em.others.Contains(empName)).ToList();
            var   v  = ccls.challanserachothers(empName).ToList();
             return Json(v , JsonRequestBehavior.AllowGet);
         }

       
         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult challanoutprint(string simno, string country, string challanno)
         {
             if (challanno == "null")
             {
                 return null;
             }
            
             DataTable dt = null;
             string Msg = "";
             if (simno != "null")
             {
                 dt = challanoutreport(simno, country, challanno);
                 Session["challanout_dt"] = dt;
                 Msg = "YES";
             }
             else
             {
                 Msg = "NULL";
             }
           
             return Json(Msg, JsonRequestBehavior.AllowGet);
            
         }

          [AcceptVerbs(HttpVerbs.Post)]
         public DataTable challanoutreport(string simno, string country, string challanno)
         {
             if (challanno == "null")
             {
                 return null;
             }

             string strqry = "select distinct ch.others as SimNo,ph.apn  ,ph.Country  ,ph.PUK ,ph.simcode as PIN  ,ph.PhoneNo ,ph.apn ,ph.username,ph.password ,ch.challanno  , co.isdcode, co.Indiacalling, co.importantnotice ,co.impnotice as impnotice2 from challanout ch left outer join  PurchaseEntry ph  on ch.others = ph.SimNo inner join CountryList co on  ch.country = co.Country where ch.others = '" + simno + "' and   ch.country ='" + country + "' and ch.cdate =(select MAX(cdate) from challanout where others = '" + simno + "') ";
             cmd = new SqlCommand(strqry, con);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable("challanout");
             da.Fill(dt);
             da.Dispose();
             cmd.Dispose();
          
             return dt;
         }
          [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult challanoutreport1(string simno, string country, string challanno)
         {
             if (challanno == "null")
             {
                 return null;
             }

             //string strqry = "select distinct ch.others as SimNo,ph.apn  ,ph.Country  ,ph.PUK ,ph.simcode as PIN  ,ph.PhoneNo ,ph.apn ,ph.username,ph.password ,ch.challanno  , co.isdcode, co.Indiacalling, co.importantnotice ,co.impnotice as impnotice2 from challanout ch left outer join  PurchaseEntry ph  on ch.others = ph.SimNo inner join CountryList co on  ch.country = co.Country where ch.others = '" + simno + "' and   ch.country ='" + country + "' and ch.cdate =(select MAX(cdate) from challanout where others = '" + simno + "') ";
             string strqry = "select distinct ch.others as SimNo,ph.apn  ,ph.Country  ,ph.PUK ,ph.simcode as PIN  ,ph.PhoneNo ,ph.apn ,ph.username,ph.password ,ch.challanno  , co.isdcode, co.Indiacalling, co.importantnotice ,co.impnotice as impnotice2 from challanout ch left outer join  PurchaseEntry ph  on ch.others = ph.SimNo inner join CountryList co on  ch.country = co.Country where ch.others = '" + simno + "' and  ch.cdate =(select MAX(cdate) from challanout where others = '" + simno + "') ";
              cmd = new SqlCommand(strqry, con);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dtrpt = new DataTable("challanout");
             da.Fill(dtrpt);
             da.Dispose();

             cmd.Dispose();
             string ab = dtrpt.Rows[0]["SimNo"].ToString();
             string ac = dtrpt.Rows[0]["apn"].ToString();
             string ad = dtrpt.Rows[0]["Country"].ToString();
             string ae = dtrpt.Rows[0]["PUK"].ToString();
             string af = dtrpt.Rows[0]["PIN"].ToString();

             string mob = dtrpt.Rows[0]["PhoneNo"].ToString();


             string userName = dtrpt.Rows[0]["username"].ToString();
             string PAss = dtrpt.Rows[0]["password"].ToString();
             string Chall = dtrpt.Rows[0]["challanno"].ToString();
             string India = dtrpt.Rows[0]["Indiacalling"].ToString();
             string importantnotice = dtrpt.Rows[0]["importantnotice"].ToString();
             string impnotice2 = dtrpt.Rows[0]["impnotice2"].ToString();

             Session["SimNo"] = ab;
             TempData["apn"] = ac;
             TempData["Country"] = ad;
             TempData["PUK"] = ae;
             TempData["PIN"] = af;
             TempData["PhoneNo"] = mob;
             TempData["username"] = userName;
             TempData["password"] = PAss;
             TempData["challanno"] = Chall;
             TempData["isdcode"] = dtrpt.Rows[0]["isdcode"].ToString();
             TempData["Indiacalling"] = India;
             TempData["importantnotice"] = importantnotice;
             TempData["impnotice2"] = impnotice2;

             return Json("ok", JsonRequestBehavior.AllowGet);
         }

         public RedirectResult challanout_print(string simno, string country, string challanno)
         {
           
             return Redirect("/Viewer/ChallanoutViewer.aspx");
             //Response.Redirect(@"Viewer\ChallanoutViewer.aspx");  GetAloatUser
         }

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult GetAloat_User()
         {
             return Json(ccls.GetAloatUser ("",usertype ,branchcode ,sessid), JsonRequestBehavior.AllowGet);
         }

         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult auto_number()
         {
             return Json(ccls.autobillno(branchcode, sessid) , JsonRequestBehavior.AllowGet);
         }

          [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult SimSearch(string empName , string country)
         {
            return Json(ccls.SearchSim_No(empName, country ,usertype,  sessid, branchcode), JsonRequestBehavior.AllowGet);
         }
         

          [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult ChallanSearch(string empName)
          {
              return Json(ccls.SearchChallan_No(empName, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
          }
        

          [AcceptVerbs(HttpVerbs.Post)]
          public JsonResult SimnoSearch(string empName)
          {
              return Json(ccls.SearchSimNo_Challanout(empName, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
          } 
             public JsonResult count_sim(string country)   
             {
                 return Json(ccls.count(country, branchcode, usertype), JsonRequestBehavior.AllowGet);
             }

             public JsonResult count_simcl(string country)
             {
                 return Json(ccls.count_client(country, branchcode, usertype), JsonRequestBehavior.AllowGet);
             }
             public ActionResult pvprintret()
             {
                 return PartialView("a");
             }
    }
}
