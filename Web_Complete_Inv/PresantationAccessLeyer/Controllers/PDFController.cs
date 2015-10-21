using System.Web.Mvc;
using ReportManagement;
using PresantationAccessLeyer.Models;


namespace PresantationAccessLeyer.Controllers
{
    public class PDFController : PdfViewController
    {
        //
        // GET: /PDF/

        public ActionResult PDF_INDEX()
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
            dynamic data = System.Web.HttpContext.Current.Session["purchasedata"];
            CustomerList custobj = new CustomerList();
            int i = 0;

            foreach (var item in data)
            {
                custobj.Add(new Customer
                {
                    sno = ++i,
                    billno = item.billno,
                    Validity = item.Validity,
                    itemtype = item.itemtype,
                    simno = item.simno,
                    country = item.country,
                    PhoneNo = item.PhoneNo,
                    apn = item.apn,
                    simcode = item.simcode,
                    puk = item.puk,
                    challanno = item.challanno
                });
            }
            return custobj;
        }
    }
}
