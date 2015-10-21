using System;
using System.Web.Mvc;
using DisplayPDFDemo.Comman;
using DataAccessLeyer.DAL;
using PresantationAccessLeyer.Models;

namespace PresantationAccessLeyer.Controllers
{
    public class ActivePostponedController : Controller
    {
        //
        // GET: /ActivePostponed/
        CustomerList cst;
        private static string pf_file = string.Empty;
        static string sessid = ""; static string branchcode = ""; static string partnername = ""; static string usertype = ""; static string username = "";
        ClientMasterDAL cmcls;
        //inventory_MVCEntities imv;
        public ActivePostponedController()
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
            cmcls = new ClientMasterDAL();
        
        }

        public ActionResult Active_Postponed()
        {
           
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]  // controller
        public ActionResult ActivePostpondd()
        {

            return Json(cmcls.Active_Postpond(usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]  // controller
        public ActionResult ActivePostpondd2(string ser)
        {

            return Json(cmcls.Active_Postpond2(ser, usertype, sessid, branchcode), JsonRequestBehavior.AllowGet);
        }




        public JsonResult updateremark_Update(string remark, string cafno, string status)
        {
            return Json(cmcls.updateremark(remark ,cafno ,status),JsonRequestBehavior.AllowGet) ;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateclient_remark(string remark , string cafno)
        {

            string Msg = cmcls.insertclientmastervalidate("UpdateR", "", cafno, "", 1, "", "", "", "", "", 0, "", "", "", "", "", "", "", "", "", "", "a", "","", "", "", "", "", "", "", "", remark);

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateclient_status( string cafno)
        {
            string Msg = cmcls.insertclientmastervalidate("UpdateOk", "", cafno, "", 1, "", "", "", "", "", 0, "", "", "", "", "", "", "", "", "", "", "a", "", "ACTIVE","", "", "", "", "", "", "", "");
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

          [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult updateclient_cafno(string cafno)
        {

            return Json(cmcls.cafno_search(cafno ,branchcode), JsonRequestBehavior.AllowGet);
        }

        
        //==========================================================================================================================================

          //   public ActionResult DisplayPDF(string pdffl, string path_f)
     //   {
     //       string v_data = "/App_Data/" + pdffl;
     //       //Response.Write("<script>");
     //        Response.Write("<script>window.open(v_data, '_newtab');</script>");
     //       //Response.Write("</script>");
     //       return View();
                 
     //     //  string v_data =pdffl  ;
     //     //  string path = "/App_Data/" + path_f; 
                 
     //     ////  string path = "FullPAthTosomePDFfile.pdf";
     ////  return File(path + "/" + v_data, "application/pdf", v_data);
     //   }

        //public FileResult DisplayPDF(string pdffl )
        //{
        //  //  string v_data = "/App_Data/"+pdffl  ;

        //     string v_data = "/App_Data/caf--0909CHALLANOUTSHEET.pdf " ;
        //    return File(v_data, "application/pdf");
        //}

        public FileResult DisplayPDF()
        {
            return File(pf_file, "application/pdf");
        }
        public JsonResult show(string pdf)
        {
            string[] strpath = pdf.Split(',');

             cst = new CustomerList();
             //pf_file = "/App_Data/" + pdf;
             pf_file = "/App_Data/UserFile/" + strpath[0] + "/" + strpath[1];
             cst.ImageUrl = "/App_Data/UserFile/" + strpath[0] + "/" + strpath[1];
             return Json(cst.ImageUrl, JsonRequestBehavior.AllowGet);
        }

        public FileResult PDFDownload()
        {

            string filepath = Server.MapPath("/Temp.pdf");
            byte[] pdfByte = Helper.GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf", "demoform1");
        }

        public FileResult PDFDisplay()
        {
            string filepath = Server.MapPath("/Temp.pdf");
            byte[] pdfByte = Helper.GetBytesFromFile(filepath);
            return File(pdfByte, "application/pdf");
        }

        public PartialViewResult PDFPartialView()
        {
            return PartialView();
        }

       

    }
}
