using System;
using System.Linq;
using System.Web.Mvc;
using DataAccessLeyer.DAL;
using DataAccessLeyer.EF;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace PresantationAccessLeyer.Controllers
{
    public class PurchaseController : Controller
    {
        //
        // GET: /Purchase/
        SqlConnection con;
        SqlCommand cmd;
   
        inventoryforwebappEntities inv; static long bl_np = 0;
        PurchaseEntryDal pcls;  private static string  qry_data = "";
        protected static string sessid = ""; protected static string branchcode = ""; protected static string partnername = ""; protected static string usertype = "";
        protected static string rolename = "";
        public PurchaseController()
        {

            sessid = Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]); // "a";  //Convert.ToString(Session["UserId"]);

            branchcode = Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]);
            partnername = Convert.ToString(System.Web.HttpContext.Current.Session["partnername"]);
            usertype = Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]);
            rolename = Convert.ToString(System.Web.HttpContext.Current.Session["rolename"]);

            pcls = new PurchaseEntryDal();
            inv = new inventoryforwebappEntities();
      
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);
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
        public JsonResult purchaseentry_Insert(string f, string BillNo, string Date, string SimNo, string Country, string PUK, string Validity, string APN, string PhoneNo, string simcode, string loginuser , string username, string password)
        {
            string Msg = "";
            if (sessid == "")
            {
              return Json("LOGOUT" , JsonRequestBehavior.AllowGet);
               
            }
            if (partnername!= "")
            {
             Msg = pcls.purchaseValidate("Insert", 1, BillNo, Date, SimNo.Trim().TrimStart(), Country, PUK.Trim().TrimStart(), Validity, APN.Trim().TrimStart(), sessid, sessid, PhoneNo.Trim().TrimStart(), simcode.Trim().TrimStart(), branchcode, partnername, username, password);
            }
                return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseentry_Update(long sno, string BillNo, string SimNo, string Country, string PUK, string APN, string PhoneNo, string simcode)
        {
            if (sessid == "")
            {
                Index();
            }
            string Msg = pcls.purchaseValidate("Update", sno, BillNo, "11/11/2014", SimNo.Trim().TrimStart(), Country, PUK.Trim().TrimStart(), "11/11/2014", APN.Trim().TrimStart(), sessid, sessid, PhoneNo.Trim().TrimStart(), simcode.Trim().TrimStart(), branchcode, partnername, "", "");
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult pv(long billno)
        {
            bl_np = billno;
            return View("pv_purchaseSearch");
        }

        [OutputCache(Duration = 0)]
        [AcceptVerbs (HttpVerbs.Post)]
        public JsonResult purchaseentry_Search(int pageIndex, string option, string strval, string date1, string date2 , string chkval)
        {
         int pageSkip = (pageIndex - 1) * 20;
            string QryDevice = string.Empty; 
            string StrQry = string.Empty;
            if (chkval == "ALL")
            {
                if (option == "BILL NO SEARCH")
                {
                    QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where  Date between '" + date1 + "' and '" + date2 + "' and billno='" + strval + "' ";
                }
                else if (option == "COUNTRY SEARCH")
                {
                    QryDevice = "select Sno,billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where Date between '" + date1 + "' and '" + date2 + "' and country='" + strval + "' ";
                }
                else if (option == "SIM NO SEARCH")
                {
                    QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where Date between '" + date1 + "' and '" + date2 + "' and simno='" + strval + "' ";
                }
                else if (option == "DATE SEARCH")
                {
                    QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where Date between '" + date1 + "' and '" + date2 + "' ";
                }
            }
            else if (chkval == "STOCK")
            {
                if (option == "BILL NO SEARCH")
                {
                    QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where status = 'NOT IN USE' and  Date between '" + date1 + "' and '" + date2 + "' and billno='" + strval + "' ";
                }
                else if (option == "COUNTRY SEARCH")
                {
                    QryDevice = "select Sno,billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where status = 'NOT IN USE' and Date between '" + date1 + "' and '" + date2 + "' and country='" + strval + "' ";
                }
                else if (option == "SIM NO SEARCH")
                {
                    QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where status = 'NOT IN USE' and Date between '" + date1 + "' and '" + date2 + "' and simno='" + strval + "' ";
                }
                else if (option == "DATE SEARCH")
                {
                    QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk  from purchaseentry where status = 'NOT IN USE' and Date between '" + date1 + "' and '" + date2 + "' ";
                }
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

            qry_data = StrQry;
            var v = pcls.purchaseserach(StrQry + " order by sno asc ");
            var vdata = v.Skip(pageSkip)
                .Take(20).OrderByDescending(em => em.billno).ToList();
                    
            System.Web.HttpContext.Current.Session["purchasedata"] = v;
            return Json(vdata, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult purchaseentry_SearchOnLoad(string strval, string date1)
        {
            string QryDevice = string.Empty;
            string StrQry = string.Empty;

            QryDevice = "select Sno, billno,Validity, itemtype, simno, country, PhoneNo,apn, simcode, puk ,username , password  from purchaseentry where  Date = '" + date1 + "'  and billno='" + strval + "' ";
            var v = pcls.purchaseserach(QryDevice);

            return Json(v, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SearchData(string empName)
        {
            var Emp = pcls.purchase_Countryserach(empName);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchData_simno(string empName)
        {
            var Emp = pcls.purchase_simserach(empName , usertype , branchcode , sessid);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchData_billno(string empName)
        {
            var Emp = pcls.purchase_billserach(empName, usertype, branchcode, sessid);

            return Json(Emp, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult SearchData_Billno(Int64 SimNo)
        //{

        //    List<PresantationAccessLeyer.EF.PurchaseEntry> Emp = imv.PurchaseEntries

        //    return Json(Emp, JsonRequestBehavior.AllowGet);
        //}


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Purchaseprint()
        {

            DataTable dt = PrintData();
            System.Web.HttpContext.Current.Session["Purchase_dt"] = dt;


            return Json("Ok", JsonRequestBehavior.AllowGet);

        }

         [OutputCache(Duration=0)]
         [AcceptVerbs(HttpVerbs.Post)]
        public DataTable PrintData()
        {
            string strqry = qry_data;
            if (strqry == "")
            {
                return null;
            }
            cmd = new SqlCommand(strqry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Purchase_t");
            da.Fill(dt);
            cmd.Dispose();
            da.Dispose();
            return dt;
        }
        public RedirectResult Purchase_print(string simno, string country, string challanno)
        {
          

       return Redirect("/Viewer/PurchaseentryView.aspx");

            //Response.Redirect(@"Viewer\ChallanoutViewer.aspx");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult auto_number()        
        {
            return Json(pcls.autobillno(branchcode, sessid));
        }

        public JsonResult Permission(string rolenam)
        {
            SettingDAL scls = new SettingDAL();
          var v =  scls.Permission("PURCHASEENTRY", rolenam);
          return Json(v, JsonRequestBehavior.AllowGet);
        }
        public JsonResult count_sim(string country)
        {
          return Json(  pcls.count(country, branchcode ,usertype) , JsonRequestBehavior.AllowGet) ;
        }
    }
}
