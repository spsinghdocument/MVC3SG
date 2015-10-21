using System.Web.Mvc;
using DataAccessLeyer.DAL;

namespace PresantationAccessLeyer.Controllers
{
    public class LiveReportController : Controller
    {
        //
        // GET: /LiveReport/

        Live_ReportDAL lvcls;
        public LiveReportController()
        {
            lvcls = new Live_ReportDAL();
        }

        public ActionResult Live_Report()
        {
            return View();
        }
        public JsonResult Search_Data(string country)
        {
            return Json(lvcls.livefun(country), JsonRequestBehavior.AllowGet);
        }

    }
}
