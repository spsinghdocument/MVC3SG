using System.Web.Mvc;
using PresantationAccessLeyer.Models;
using ReportManagement;


namespace PresantationAccessLeyer.Controllers
{
    public class PDFChallanoutController : PdfViewController
    {
        //
        // GET: /PDFChallanout/

        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult PrintCustomers()
        {
            try
            {
                CustomerList customerList = CreateCustomerList();
                FillImageUrl(customerList, "report.jpg");
                return this.ViewPdf("", "PrintDemo", customerList);

            }
            catch
            {
                return View();

            }
        }

        private void FillImageUrl(CustomerList customerList, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            customerList.ImageUrl = url + "Content/" + imageName;
            customerList.chno = stval;
        }
        private static string stval; private static string stval1;
        private CustomerList CreateCustomerList()
        {
            dynamic data = System.Web.HttpContext.Current.Session["challandata"];
            CustomerList custobj = new CustomerList();
            int i = 0;

            foreach (var item in data)
            {
                stval = item.challanno;
         BusinessAccessLeyer.BAL.StaticVariables.selectuser   = item.executivenameto;
                custobj.Add(new Customer
                {
                    sno = ++i,
                    billno = item.challanno,
                    itemtype = item.itemtype,
                    simno = item.others,
                    country = item.country,
                    PhoneNo = item.PhoneNo,                  
                    simcode = item.simcode,
                    puk = item.puk ,
                   
                });
            }
            return custobj;
        }

    }
}
