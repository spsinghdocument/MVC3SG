using System.Web.Mvc;
using System.Text.RegularExpressions;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class ContactUsController : Controller
    {
        //
        // GET: /ContactUs/
        ContactDAL cdalcls;
        public ContactUsController()
        {
            cdalcls = new ContactDAL();
        }


        public ActionResult Contact_Us()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult MOBILEvalidate(string mobil)
        {
            string Msg = "";
            if (mobil.Length > 0)
            {


                string s = "^[0-9]{10}$";
                string s1 = mobil;
                Match aa = Regex.Match(s1, s);
                if (aa.Success)
                {
                    Msg = "OK";
                }
                else
                {
                    Msg = "MOBILE NO  Not Valid";
                }
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insertdatacontacttab(string   NAME,string  MOBILENO,string  EMAILID,string  PRODUCT, string   COMMENT )
        {
          string Msg=  cdalcls.insertdalcust(NAME, "ab", MOBILENO, EMAILID, PRODUCT, COMMENT);
          return Json(Msg, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Contactusval()
        {
            
          return Json(cdalcls.Contactusdal(), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Contactussearch(string strmob)
        {

            return Json(cdalcls.Contactussearchdal(strmob), JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Contactusdelete(string strmob)
        {

            return Json(cdalcls.Contactusdeletedal(strmob), JsonRequestBehavior.AllowGet);
        }
    }

    
}
