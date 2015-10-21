using ReportManagement;
using System.Web.Mvc;
using PresantationAccessLeyer.Models;

namespace PresantationAccessLeyer.Controllers
{
    public class PDFCustomerentryController : PdfViewController
    {
        //
        // GET: /PDFCustomerentry/

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
        }
       
        private CustomerList CreateCustomerList()
        {
            dynamic data = System.Web.HttpContext.Current.Session["customerentrydata"];
            CustomerList custobj = new CustomerList();
            int i = 0;

            foreach (var item in data)
            {
                custobj.Add(new Customer
                {
                    sno = ++i,
                    billno = item.customeracno,
                    Validity = item.customername,
                    itemtype = item.customeraddress,
                    simno = item.emailid,
                    country = item.website,
                    PhoneNo = item.passportno,
                    apn = item.mobileno,
                    simcode = item.alternateno,
                    puk = item.branchcode,
                   
                });
            }
            return custobj;
        }
    }
}
