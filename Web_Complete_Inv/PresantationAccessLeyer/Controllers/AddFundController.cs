using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class AddFundController : Controller
    {
        //
        // GET: /AddFund/
        Add_Fund adf;
        public AddFundController()
        {
            adf = new Add_Fund();
        }

        public ActionResult Add_Fund()
        {
            return View();
        }


        public JsonResult updatedata(string updatedata  , string usercode)
        {
            return Json(adf.updatefund(updatedata, usercode), JsonRequestBehavior.AllowGet);
        }
    }
}
