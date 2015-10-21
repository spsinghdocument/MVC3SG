using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace PresantationAccessLeyer.Viewer
{
    public partial class ChallanoutViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            challanout();
        }

        public void challanout()
        {
            DataTable dt = new DataTable("challandt");
            dt = (DataTable)Session["challanout_dt"];
           
            DataSet ds = new DataSet();
            Viewer.Report.ChallanOutrpt crystalReport = new Report.ChallanOutrpt();
            string st = HttpContext.Current.Server.MapPath("~/ChallanOutCrpt1.rpt");

            //  Label1.Text = st;
            //   Response.Write(string.Format("<script language='javascript' type='text/javascript'>alert( "+st+") </script>"));

            crystalReport.Load(st);
            if (ds.Tables.Contains("challandt") == true)
            {
                ds.Tables.Remove("challandt");
            }
            ds.Tables.Add(dt);

            ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("~/App_Data/Challanout.xml"));

            crystalReport.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = crystalReport;


            crystalReport.ExportToHttpResponse
           (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "CHALLANOUTSHEET");


             

        }
    }
}