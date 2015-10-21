using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Reflection;
using System.IO;
using DataAccessLeyer.DAL;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace PresantationAccessLeyer.E
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string FREETALKVALUE = "";
        Add_CountryInsert ad = new Add_CountryInsert();
        protected void Page_Load(object sender, EventArgs e)
        {




            if (!IsPostBack)
            {
                ddltariff.Items.Clear();
                DropDownExTextboxExample1.Items.Clear();
                tctdatapname.Text = "";
                DropDownExTextboxExample.Items.Clear();
                TextboxExample.Text = "";

                DropDownListBoltion.Items.Clear();
                TextBox1.Text = "";

                TextBox23.Text = "";
               

                loaddata();
                data_fillcountry();
                ddlcountry.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        public void data_fillcountry()
        {
            ddlcountry.DataSource = ad.GetCountry(); ;
            ddlcountry.DataValueField = "CountryName";
            ddlcountry.DataTextField = "CountryName";
            ddlcountry.DataBind();
        }
        protected void TxtId_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent.Parent.Parent;
            Label lblcost = (Label)currentRow.FindControl("lbldcost");
            Label lblunits = (Label)currentRow.FindControl("lblunits");
            TextBox txt = (TextBox)currentRow.FindControl("txtrate");
            decimal count = Convert.ToDecimal(lblunits.Text) * Convert.ToDecimal(txt.Text);
            lblcost.Text = Math.Round(count).ToString();
        }


        protected void loaddata()
        {

            btdiv.Visible = false;

            try
            {
 
                string st = Convert.ToString(System.Web.HttpContext.Current.Session["fname"]);
                string fileName = "App_Data\\" + st;

                const int startRow = 1;

                string folder = Server.MapPath("~");  // Assembly.GetEntryAssembly().Location;
                if (folder != null)
                {
                    folder = Path.GetDirectoryName(folder);
                    string filePath = Path.Combine(folder, fileName);
                    IList<ExampleData> exampleDataList = new List<ExampleData>();


                    // Get the file we are going to process
                    var existingFile = new FileInfo(filePath);
                    // Open and read the XlSX file.
                    using (var package = new ExcelPackage(existingFile))
                    {
                        // Get the work book in the file
                        ExcelWorkbook workBook = package.Workbook;
                        if (workBook != null)
                        {
                            if (workBook.Worksheets.Count > 0)
                            {
                                // Get the first worksheet
                                ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                                int t = currentWorksheet.Dimension.End.Column;
                                // read some data
                                if (t == 6)
                                {
                                    object col1Header = currentWorksheet.Cells[startRow, 1].Value;
                                    object col2Header = currentWorksheet.Cells[startRow, 2].Value;

                                    object col3Header = ""; // currentWorksheet.Cells[startRow, 3].Value;
                                    object col4Header = currentWorksheet.Cells[startRow, 3].Value;
                                    object col5Header = currentWorksheet.Cells[startRow, 4].Value;
                                    object col6Header = currentWorksheet.Cells[startRow, 5].Value;

                                    object col7Header = currentWorksheet.Cells[startRow, 6].Value;
                                    switch (col1Header.ToString().Trim())

                                    {
                                        case "DATETIME":
                                        case "DATE & TIME":
                                            col1Header = currentWorksheet.Cells[startRow, 1].Value == "DATE & TIME" ? "DATETIME" : "DATETIME";
                                            if (

                                       ((col1Header != null) && (col1Header.ToString().Trim() == "DATETIME")) &&
                                          ((col2Header != null) && (col2Header.ToString().Trim() == "NUMBER")) &&
                                           ((col4Header != null) && (col4Header.ToString().Trim() == "DURATION")) &&
                                          ((col5Header != null) && (col5Header.ToString().Trim() == "UNITS")) &&
                                          ((col6Header != null) && (col6Header.ToString().Trim() == "RATE")) &&
                                          ((col7Header != null) && (col7Header.ToString().Trim() == "COST")))
                                            {

                                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                                {
                                                    object col1Value = currentWorksheet.Cells[rowNumber, 1].Value;
                                                    object col2Value = currentWorksheet.Cells[rowNumber, 2].Value;
                                                    object col3Value = ""; // currentWorksheet.Cells[rowNumber, 3].Value;
                                                    object col4Value = "";

                                                    try
                                                    {
                                                        var timeSpan = TimeSpan.FromDays(Convert.ToDouble(currentWorksheet.Cells[rowNumber, 3].Value));
                                                        int hh = timeSpan.Hours;
                                                        int mm = timeSpan.Minutes;
                                                        int ss = timeSpan.Seconds;
                                                        string dur = string.Format("{0} :{1} :{2}", hh, mm, ss);

                                                        if (dur == "0 :0 :0")
                                                        {
                                                            col4Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                        }
                                                        else
                                                            col4Value = dur;
                                                    }
                                                    catch
                                                    {
                                                        col4Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                    }
                                                    object col5Value = currentWorksheet.Cells[rowNumber, 4].Value;


                                                    object col6Value = currentWorksheet.Cells[rowNumber, 5].Value == null ? "0" : currentWorksheet.Cells[rowNumber, 5].Value;
                                                    object col7Value = currentWorksheet.Cells[rowNumber, 6].Value == null ? "0" : currentWorksheet.Cells[rowNumber, 5].Value;

                                                    if (Convert.ToString(col1Value) == "GRAND TOTAL")
                                                    {
                                                        exampleDataList.Add(new ExampleData
                                                        {

                                                            DATETIME = "DATA PACK ",
                                                            NUMBER = "0",
                                                            TYPE = "",
                                                            DURATION = "",
                                                            UNITS = "",
                                                            RATE = "",
                                                            COST = ""


                                                        });
                                                    }

                                                    exampleDataList.Add(new ExampleData
                                                    {

                                                        DATETIME = Convert.ToString(col1Value),
                                                        NUMBER = Convert.ToString(col2Value),
                                                        TYPE = Convert.ToString(col3Value),
                                                        DURATION = Convert.ToString(col4Value),
                                                        UNITS = Convert.ToString(col5Value),
                                                        RATE = Convert.ToString(col6Value),
                                                        COST = Convert.ToString(col7Value)


                                                    });

                                                    btdiv.Visible = true;

                                                }
                                            }
                                            break;
                                        case "NUMBER":
                                            col2Header = currentWorksheet.Cells[startRow, 2].Value == "DATE & TIME" ? "DATETIME" : "DATETIME";
                                            if (

                                    (
                                     ((col2Header != null) && (col1Header.ToString().Trim() == "NUMBER")) &&
                                    (col1Header != null) && (col2Header.ToString().Trim() == "DATETIME")) &&
                                        ((col4Header != null) && (col4Header.ToString().Trim() == "DURATION")) &&
                                       ((col5Header != null) && (col5Header.ToString().Trim() == "UNITS")) &&
                                       ((col6Header != null) && (col6Header.ToString().Trim() == "RATE")) &&
                                       ((col7Header != null) && (col7Header.ToString().Trim() == "COST")))
                                            {

                                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                                {
                                                    object col1Value =currentWorksheet.Cells[rowNumber, 1].Value;
                                                    object col2Value = currentWorksheet.Cells[rowNumber, 2].Value;
                                                    object col3Value = ""; // currentWorksheet.Cells[rowNumber, 3].Value;
                                                    object col4Value = "";

                                                    try
                                                    {
                                                        var timeSpan = TimeSpan.FromDays(Convert.ToDouble(currentWorksheet.Cells[rowNumber, 3].Value));
                                                        int hh = timeSpan.Hours;
                                                        int mm = timeSpan.Minutes;
                                                        int ss = timeSpan.Seconds;
                                                        string dur = string.Format("{0} :{1} :{2}", hh, mm, ss);

                                                        if (dur == "0 :0 :0")
                                                        {
                                                            col4Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                        }
                                                        else
                                                            col4Value = dur;
                                                    }
                                                    catch
                                                    {
                                                        col4Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                    }
                                                    object col5Value = currentWorksheet.Cells[rowNumber, 4].Value;


                                                    object col6Value = currentWorksheet.Cells[rowNumber, 5].Value;
                                                    object col7Value = currentWorksheet.Cells[rowNumber, 6].Value;

                                                    if (Convert.ToString(col1Value) == "GRAND TOTAL")
                                                    {
                                                        exampleDataList.Add(new ExampleData
                                                        {

                                                            DATETIME = "DATA PACK ",
                                                            NUMBER = "0",
                                                            TYPE = "",
                                                            DURATION = "",
                                                            UNITS = "",
                                                            RATE = "",
                                                            COST = ""


                                                        });
                                                    }

                                                    exampleDataList.Add(new ExampleData
                                                    {
                                                        NUMBER = Convert.ToString(col1Value),
                                                        DATETIME = Convert.ToString(col2Value),
                                                       // TYPE = Convert.ToString(col3Value),
                                                        DURATION = Convert.ToString(col4Value),
                                                        UNITS = Convert.ToString(col5Value),
                                                        RATE = Convert.ToString(col6Value),
                                                        COST = Convert.ToString(col7Value)


                                                    });

                                                    btdiv.Visible = true;

                                                }
                                            }

                                            break;
                                    }     
                                       

                                   
                                

                                }
                                else if (t == 7)
                                {
                                    object col1Header = currentWorksheet.Cells[startRow, 1].Value;
                                    object col2Header = currentWorksheet.Cells[startRow, 2].Value;
                                    object col3Header = currentWorksheet.Cells[startRow, 3].Value;

                                    object col4Header = currentWorksheet.Cells[startRow, 4].Value;
                                    object col5Header = currentWorksheet.Cells[startRow, 5].Value;
                                    object col6Header = currentWorksheet.Cells[startRow, 6].Value;
                                    object col7Header = currentWorksheet.Cells[startRow, 7].Value;

                                   // if ((col1Header != null) && (col1Header.ToString().Trim() == "DATE"))

                                        switch(col1Header.ToString().Trim())
                                        {
                                            case "DATE":

                                        if (
                                            
                                            ((col1Header != null) && (col1Header.ToString().Trim() == "DATE")) &&
                                               ((col2Header != null) && (col2Header.ToString().Trim() == "TIME")) &&
                                            ((col3Header != null) && (col3Header.ToString().Trim() == "NUMBER")) &&
                                             ((col4Header != null) && (col4Header.ToString().Trim() == "DURATION")) &&
                                            ((col5Header != null) && (col5Header.ToString().Trim() == "UNITS")) &&
                                            ((col6Header != null) && (col6Header.ToString().Trim() == "RATE")) &&
                                            ((col7Header != null) && (col7Header.ToString().Trim() == "COST"))


                                              
                                            
                                            )
                                        
                                        
                                        {

                                            for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                            {
                                                object col1Value = currentWorksheet.Cells[rowNumber, 1].Value;
                                                object col2Value = currentWorksheet.Cells[rowNumber, 2].Value;
                                                object col3Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                object col4Value = "";
                                                try
                                                {
                                                    var timeSpan = TimeSpan.FromDays(Convert.ToDouble(currentWorksheet.Cells[rowNumber, 4].Value));
                                                    int hh = timeSpan.Hours;
                                                    int mm = timeSpan.Minutes;
                                                    int ss = timeSpan.Seconds;
                                                    string dur = string.Format("{0} :{1} :{2}", hh, mm, ss);
                                                    if (dur == "0 :0 :0")
                                                    {
                                                        col4Value = currentWorksheet.Cells[rowNumber, 4].Value;
                                                    }
                                                    else
                                                        col4Value = dur;
                                                }
                                                catch
                                                {
                                                    col4Value = currentWorksheet.Cells[rowNumber, 4].Value;
                                                }
                                                object col5Value = currentWorksheet.Cells[rowNumber, 5].Value;


                                                object col6Value = currentWorksheet.Cells[rowNumber, 6].Value;
                                                object col7Value = currentWorksheet.Cells[rowNumber, 7].Value;

                                                exampleDataList.Add(new ExampleData
                                                {

                                                    DATE = Convert.ToString(col1Value),
                                                    TIME = Convert.ToString(col2Value),

                                                    NUMBER = Convert.ToString(col3Value),
                                                    DURATION = Convert.ToString(col4Value),
                                                    UNITS = Convert.ToString(col5Value),
                                                    RATE = Convert.ToString(col6Value),
                                                    COST = Convert.ToString(col7Value)

                                                });

                                            }
                                            btdiv.Visible = true;
                                        }

                                    
                                    else
                                    {
                                        Response.Write("<script>alert('Example data incorrectly formatted.')</script>");

                                    }
                                        break;
                                            case "ORIGIN":
                                        col2Header = currentWorksheet.Cells[startRow, 2].Value == "DATE & TIME" ? "DATETIME" : "DATETIME";;
                                        if (


                                   ((col1Header != null) && (col1Header.ToString().Trim() == "ORIGIN")) &&
                                      ((col2Header != null) && (col2Header.ToString().Trim() == "DATETIME")) &&
                                   ((col3Header != null) && (col3Header.ToString().Trim() == "NUMBER")) &&
                                    ((col4Header != null) && (col4Header.ToString().Trim() == "DURATION")) &&
                                   ((col5Header != null) && (col5Header.ToString().Trim() == "UNITS")) &&
                                   ((col6Header != null) && (col6Header.ToString().Trim() == "RATE")) &&
                                   ((col7Header != null) && (col7Header.ToString().Trim() == "COST"))




                                   )
                                        {

                                            for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                            {
                                                object col1Value = currentWorksheet.Cells[rowNumber, 1].Value;
                                                object col2Value = currentWorksheet.Cells[rowNumber, 2].Value;
                                                object col3Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                object col4Value = "";
                                                try
                                                {

                                                 //   string Time = currentWorksheet.Cells[rowNumber, 4].Value;
                                                 //   DateTime date = DateTime.ParseExact(Time, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);

                                                // string sppp = date.ToString();

                                                 //DateTime dateTime = DateTime.ParseExact((string)currentWorksheet.Cells[rowNumber, 4].Value, "HH:mm:ss", CultureInfo.InvariantCulture);



                                                    var timeSpan = TimeSpan.FromDays(Convert.ToDouble(Convert.ToString( currentWorksheet.Cells[rowNumber, 4].Value)));
                                                    int hh = timeSpan.Hours;
                                                    int mm = timeSpan.Minutes;
                                                    int ss = timeSpan.Seconds;
                                                    string dur = string.Format("{0} :{1} :{2}", hh, mm, ss);
                                                    if (dur == "0 :0 :0")
                                                    {
                                                        col4Value = currentWorksheet.Cells[rowNumber, 4].Value;
                                                    }
                                                    else
                                                        col4Value = dur;
                                                }
                                                catch
                                                {
                                                    col4Value = currentWorksheet.Cells[rowNumber, 4].Value;
                                                }
                                                object col5Value = currentWorksheet.Cells[rowNumber, 5].Value;


                                                object col6Value = currentWorksheet.Cells[rowNumber, 6].Value;
                                                object col7Value = currentWorksheet.Cells[rowNumber, 7].Value;

                                                exampleDataList.Add(new ExampleData
                                                {

                                                    ORIGIN = Convert.ToString(col1Value),
                                                    DATETIME = Convert.ToString(col2Value),

                                                    NUMBER = Convert.ToString(col3Value),
                                                    DURATION = Convert.ToString(col4Value),
                                                    UNITS = Convert.ToString(col5Value),
                                                    RATE = Convert.ToString(col6Value),
                                                    COST = Convert.ToString(col7Value)

                                                });

                                            }
                                            btdiv.Visible = true;
                                        }


                                        else
                                        {
                                            Response.Write("<script>alert('Example data incorrectly formatted.')</script>");

                                        }

                                        break;

                                            case "DAYDATE":
                                            case "DAY & DATE":
                                         col1Header = currentWorksheet.Cells[startRow, 1].Value == "DAY & DATE" ? "DAYDATE" : "DAYDATE";;

                                        if (

                                   ((col1Header != null) && (col1Header.ToString().Trim() == "DAYDATE")) &&
                                      ((col2Header != null) && (col2Header.ToString().Trim() == "TIME")) &&
                                   ((col3Header != null) && (col3Header.ToString().Trim() == "NUMBER")) &&
                                    ((col4Header != null) && (col4Header.ToString().Trim() == "DURATION")) &&
                                   ((col5Header != null) && (col5Header.ToString().Trim() == "UNITS")) &&
                                   ((col6Header != null) && (col6Header.ToString().Trim() == "RATE")) &&
                                   ((col7Header != null) && (col7Header.ToString().Trim() == "COST"))




                                   )
                                        {

                                            for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                            {
                                                object col1Value = currentWorksheet.Cells[rowNumber, 1].Value;
                                                object col2Value = currentWorksheet.Cells[rowNumber, 2].Value;
                                                object col3Value = currentWorksheet.Cells[rowNumber, 3].Value;
                                                object col4Value = "";
                                                try
                                                {
                                                    var timeSpan = TimeSpan.FromDays(Convert.ToDouble(currentWorksheet.Cells[rowNumber, 4].Value));
                                                    int hh = timeSpan.Hours;
                                                    int mm = timeSpan.Minutes;
                                                    int ss = timeSpan.Seconds;
                                                    string dur = string.Format("{0} :{1} :{2}", hh, mm, ss);
                                                    if (dur == "0 :0 :0")
                                                    {
                                                        col4Value = currentWorksheet.Cells[rowNumber, 4].Value;
                                                    }
                                                    else
                                                        col4Value = dur;
                                                }
                                                catch
                                                {
                                                    col4Value = currentWorksheet.Cells[rowNumber, 4].Value;
                                                }
                                                object col5Value = currentWorksheet.Cells[rowNumber, 5].Value;


                                                object col6Value = currentWorksheet.Cells[rowNumber, 6].Value;
                                                object col7Value = currentWorksheet.Cells[rowNumber, 7].Value;

                                                exampleDataList.Add(new ExampleData
                                                {

                                                    DAYDATE = Convert.ToString(col1Value),
                                                    TIME = Convert.ToString(col2Value),

                                                    NUMBER = Convert.ToString(col3Value),
                                                    DURATION = Convert.ToString(col4Value),
                                                    UNITS = Convert.ToString(col5Value),
                                                    RATE = Convert.ToString(col6Value),
                                                    COST = Convert.ToString(col7Value)

                                                });

                                            }
                                            btdiv.Visible = true;
                                        }


                                        else
                                        {
                                            Response.Write("<script>alert('Example data incorrectly formatted.')</script>");

                                        }


                                        break;
                                           
                                                
                                }
                                }
                                //==================================================
                              

                                //==============================================END
                            }
                        }
                    }

                    GridView1.DataSource = exampleDataList;
                    GridView1.DataBind();

                }
                else
                {

                    Response.Write("<script>alert('Could not find excecuting path.')</script>");

                }
            }
            catch (IOException ioEx)
            {
                if (!String.IsNullOrEmpty(ioEx.Message))
                {
                    if (ioEx.Message.Contains("because it is being used by another process."))
                    {
                        Response.Write("<script>alert('Could not read example data. Please make sure it not open in Excel.')</script>");

                    }
                }


                Response.Write("Could not read example data. " + ioEx.Message);
            }
            catch (Exception ex)
            {

                Response.Write("An error occured while reading example data. " + ex.Message);
            }
            finally
            {

            }



        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            decimal a = 0;
            decimal c = 0;
            int v = 0;
            string a1 = "0";
            string a2 = "0";
            string a3 = "0";
            string a4 = "0";
            int ct = 0;
            for (int i = 0; i < (GridView1.Rows.Count); i++)
            {
                v++;
                TextBox txt = (TextBox)GridView1.Rows[i].FindControl("txtrate");
                if (txt.Text == "TOTAL")
                {
                    if (ct == 0)
                    {
                        Label lbla = (Label)GridView1.Rows[v - 1].FindControl("lbldcost");
                        lbla.Text = c.ToString();
                        a1 = c.ToString();
                        ct++;
                    }
                    else if (ct == 1)
                    {
                        Label lbla = (Label)GridView1.Rows[v - 1].FindControl("lbldcost");
                        lbla.Text = c.ToString();
                        a2 = c.ToString();
                        ct++;
                    }
                    else if (ct == 2)
                    {
                        Label lbla = (Label)GridView1.Rows[v - 1].FindControl("lbldcost");
                        lbla.Text = c.ToString();
                        a3 = c.ToString();
                        ct++;
                    }
                    c = 0;
                    a = 0;
                    continue;
                }
                else
                {
                    try
                    {
                        Label lbl_a = (Label)GridView1.Rows[i].FindControl("lbldcost");
                        a = Convert.ToDecimal(lbl_a.Text);
                        c = c + a;
                    }
                    catch
                    {


                    }
                }

                System.Web.HttpContext.Current.Session["grd"] = GridView1.DataSource;
            }


        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal c = 0;
            decimal gtotal = 0;

            string a1 = "0";
            string a2 = "0";
            string a3 = "0";
            string a4 = "0";
            string a5 = "0";
            int ct = 0;

            int v = 0;
            for (int i = 0; i < (GridView1.Rows.Count); i++)
            {
                v++;
                TextBox txt = (TextBox)GridView1.Rows[i].FindControl("txtrate");
                if (txt.Text == "TOTAL")
                {
                    if (ct == 0)
                    {
                        Label lbla = (Label)GridView1.Rows[v - 1].FindControl("lbldcost");
                        lbla.Text = c.ToString();
                        gtotal += Convert.ToDecimal(lbla.Text);
                        TextBox gtotal_val = (TextBox)GridView1.FooterRow.FindControl("txtgrandtotal");

                        gtotal_val.Text = gtotal.ToString();
                        a1 = gtotal.ToString();
                        ct++;
                    }
                    else if (ct == 1)
                    {
                        Label lbla = (Label)GridView1.Rows[v - 1].FindControl("lbldcost");
                        lbla.Text = c.ToString();
                        gtotal += Convert.ToDecimal(lbla.Text);
                        TextBox gtotal_val = (TextBox)GridView1.FooterRow.FindControl("txtgrandtotal");

                       // gtotal_val.Text = (gtotal + Convert.ToDecimal(TextboxExample.Text == "" ? "0" : TextboxExample.Text) - Convert.ToDecimal(Label1.Text == "Label" ? "0" : Label1.Text)).ToString();
                       // decimal agh = (gtotal + Convert.ToDecimal(TextboxExample.Text == "" ? "0" : TextboxExample.Text) - Convert.ToDecimal(Label1.Text == "Label" ? "0" : Label1.Text));
                        decimal agh = (gtotal + Convert.ToDecimal(TextboxExample.Text == "" ? "0" : "0") - Convert.ToDecimal(Label1.Text == "Label" ? "0" : "0"));

                        if (agh>0)
                        {
                            gtotal_val.Text = (gtotal + Convert.ToDecimal(TextboxExample.Text == "" ? "0" : "0") - Convert.ToDecimal(Label1.Text == "Label" ? "0" : "0")).ToString();
                        }
                        else
                        {
                            gtotal_val.Text = "0";
                        }
                       // gtotal_val.Text = ((gtotal + Convert.ToDecimal(TextboxExample.Text == "" ? "0" : TextboxExample.Text) - Convert.ToDecimal(Label1.Text == "Label" ? "0" : Label1.Text)).ToString()) > 0 ? "0" : ((gtotal + Convert.ToDecimal(TextboxExample.Text == "" ? "0" : TextboxExample.Text) - Convert.ToDecimal(Label1.Text == "Label" ? "0" : Label1.Text)).ToString());
                      
                        a2 = c.ToString();
                        ct++;
                    }
                    else if (ct == 2)
                    {
                        Label lbla = (Label)GridView1.Rows[v - 1].FindControl("lbldcost");
                        lbla.Text = c.ToString();
                        gtotal += Convert.ToDecimal(lbla.Text);
                        TextBox gtotal_val = (TextBox)GridView1.FooterRow.FindControl("txtgrandtotal");
                        gtotal_val.Text = gtotal.ToString();
                        a3 = c.ToString();
                        ct++;
                    }

                    c = 0;
                    a = 0;
                    continue;
                }
                else
                {
                    try
                    {
                        Label lbl_a = (Label)GridView1.Rows[i].FindControl("lbldcost");
                        a = Convert.ToDecimal(lbl_a.Text);

                        c = c + a; //storing total qty into variable 
                    }
                    catch
                    {


                    }
                }
            }

            for (int x = 0; x < (GridView1.Rows.Count); x++)
            {
                Label dtm = (Label)GridView1.Rows[x].FindControl("lbldtime");


                Label dtm_dv = (Label)GridView1.Rows[x].FindControl("lbldtime");

                Label dtm_dtime = (Label)GridView1.Rows[x].FindControl("LabelDATE");

                Label dtm_orgin = (Label)GridView1.Rows[x].FindControl("LabelORIGIN");

                Label dtm_daytime = (Label)GridView1.Rows[x].FindControl("LabelDAYDATE");



                if (dtm.Text == "SUMMARY" && dtm != null || dtm_dtime.Text == "SUMMARY" && dtm != null || dtm_dtime.Text == "SUMMARY" && dtm != null || dtm_daytime.Text == "SUMMARY" && dtm_daytime != null || dtm_orgin.Text == "SUMMARY" && dtm_orgin != null)
                {
                     Label num = (Label)GridView1.Rows[x + 1].FindControl("lblnum");
                    Label num_2 = (Label)GridView1.Rows[x + 2].FindControl("lblnum");
                    Label num_3 = (Label)GridView1.Rows[x + 3].FindControl("lblnum");
                  
                    Label lblt_ime = (Label)GridView1.Rows[x + 1].FindControl("LabelTime");
                    Label lblt_ime1 = (Label)GridView1.Rows[x+2].FindControl("LabelTime");
                    Label lblt_ime3 = (Label)GridView1.Rows[x + 3].FindControl("LabelTime");
                    lblt_ime.Text = ""; lblt_ime1.Text = ""; lblt_ime3.Text = "";

                    Label lbld_time = (Label)GridView1.Rows[x + 1].FindControl("lbldtime");
                    if (lbld_time.Text == "VOICE")
                    {
                    continue;
                    }
                    else
                    {
                    lbld_time.Text = "";
                    }
                    
                    Label lbld_time1 = (Label)GridView1.Rows[x + 2].FindControl("lbldtime");
                    if (lbld_time1.Text == "SMS")
                    {
                        continue;
                    }
                    else
                    {

                        lbld_time1.Text = "";
                    
                    }
                   
                    Label lbld_time2 = (Label)GridView1.Rows[x + 3].FindControl("lbldtime");
                    if (lbld_time2.Text == "DATA PACK ")
                    { continue; }
                    else
                    { 
                        lbld_time2.Text = "";
                    }
                   
                    
                  
                   
                    num.Text = a1;
                    num_2.Text = a2;
                    num_3.Text = a3;
                    
                }
                if (dtm.Text == "DATA PAY PER USE" || dtm_dtime.Text == "DATA PAY PER USE" || dtm_orgin.Text == "DATA PAY PER USE" || dtm_daytime.Text == "DATA PAY PER USE" || dtm_orgin.Text == "DATA PAY PER USE")
                {
                    if (TextboxExample.Text != "" && tctdatapname.Text != "")
                    {
                        Label num_3 = (Label)GridView1.Rows[x].FindControl("lblnum");
                    Label lblt_ime1 = (Label)GridView1.Rows[x].FindControl("LabelTime");
                    lblt_ime1.Text = "";

                    Label lbld_time = (Label)GridView1.Rows[x].FindControl("lbldtime");
                    lbld_time.Text = "";

                        num_3.Text = TextboxExample.Text == "" ? "0" : TextboxExample.Text;
                        a5 = TextboxExample.Text == "" ? "0" : TextboxExample.Text;
                       // a5 = "10";
                    }

                }
                else if (dtm.Text == "GRAND TOTAL" || dtm_dtime.Text == "GRAND TOTAL" || dtm_orgin.Text == "GRAND TOTAL" || dtm_daytime.Text == "GRAND TOTAL" || dtm_orgin.Text == "GRAND TOTAL")
                {

                    Label num_3 = (Label)GridView1.Rows[x].FindControl("lblnum");
                    num_3.Text = (Convert.ToDecimal(a1) + Convert.ToDecimal(a2) + Convert.ToDecimal(a3) + Convert.ToDecimal(a5)).ToString();

                    Label lblt_ime1 = (Label)GridView1.Rows[x].FindControl("LabelTime");
                    if (lblt_ime1.Text == "GRAND TOTAL")
                    { continue; }
                    else
                    {
                    lblt_ime1.Text = "";
                    }

                    Label lbld_time = (Label)GridView1.Rows[x].FindControl("lbldtime");
                    if (lbld_time.Text == "GRAND TOTAL")
                    { continue; }
                    else
                    {
                        lbld_time.Text = "";
                    }

                }


            }


            IList<Example_Data> exampleData_List = new List<Example_Data>();
            for (int i = 0; i < (GridView1.Rows.Count); i++)
            {
                Label date =(Label)GridView1.Rows[i].FindControl("LabelDATE");
                Label time =(Label)GridView1.Rows[i].FindControl("LabelTime");
                Label dayeDate =(Label)GridView1.Rows[i].FindControl("LabelDAYDATE");

                Label txt1 = (Label)GridView1.Rows[i].FindControl("lbldtime");
                TextBox txt2 = (TextBox)GridView1.Rows[i].FindControl("txtrate");
                Label txt3 = (Label)GridView1.Rows[i].FindControl("lblnum");
                Label txt4 = (Label)GridView1.Rows[i].FindControl("LabelORIGIN");
                Label txt5 = (Label)GridView1.Rows[i].FindControl("lbldur");
                Label txt6 = (Label)GridView1.Rows[i].FindControl("lblunits");
                Label txt7 = (Label)GridView1.Rows[i].FindControl("lbldcost");


                exampleData_List.Add(new Example_Data
                {

                    DATETIME = Convert.ToString(Convert.ToString(txt1.Text)) == "" ? Convert.ToString(Convert.ToString(date.Text)) == "" ?
                    Convert.ToString(Convert.ToString(dayeDate.Text)) + "" + Convert.ToString(Convert.ToString(time.Text))
                    :Convert.ToString(Convert.ToString(date.Text)) + "" + Convert.ToString(Convert.ToString(time.Text))
                    : Convert.ToString(Convert.ToString(txt1.Text)),

                    NUMBER = Convert.ToString(Convert.ToString(txt3.Text)),
                  //  TYPE = Convert.ToString(Convert.ToString(txt4.Text)),
                    TYPE = Convert.ToString(Convert.ToString(txt4.Text)),
                    DURATION = Convert.ToString(Convert.ToString(txt5.Text)),
                    UNITS = Convert.ToString(Convert.ToString(txt6.Text)),
                    RATE = Convert.ToString(Convert.ToString(txt2.Text)),
                    COST = Convert.ToString(Convert.ToString(txt7.Text))
                });
            }

            System.Web.HttpContext.Current.Session["grd"] = exampleData_List;
          
         
            System.Web.HttpContext.Current.Session["grandt_val"] =  gtotal;

            System.Web.HttpContext.Current.Session["datapackname"] = tctdatapname.Text;
           
           // System.Web.HttpContext.Current.Session["datavalue"] = TextboxExample.Text;

            System.Web.HttpContext.Current.Session["datavalue"] = TextBox1.Text;

        }

        protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            BILLDAL bl = new BILLDAL();
            ddltariff.DataSource = bl.Tariffcodefill_withcountry(ddlcountry.SelectedItem.ToString(), Convert.ToString(System.Web.HttpContext.Current.Session["usertype"]),
            Convert.ToString(System.Web.HttpContext.Current.Session["usercode"]), Convert.ToString(System.Web.HttpContext.Current.Session["branchcode"]));

            ddltariff.DataValueField = "tariffcode";
            ddltariff.DataTextField = "tariffcode";
            ddltariff.DataBind();
            ddltariff.Items.Insert(0, new ListItem("Select", "0"));
        }

        DataTable dt = null;
        protected void fillallvalue()
        {
            datawithminats();

            int indiaunits = 0;
            int othersunits = 0;
            BILLDAL bal = new BILLDAL();
            DataTable dtrowdata = bal.country_name();
             dt = bal.tarifupload(ddltariff.SelectedItem.ToString());


            DataRow[] dr_lunits = dt.Select("CHARGESTYPE = 'FREE VALUE'", "");
            decimal freeval = 0;
            string strmin = "";
            string cnty = "";
            if (dr_lunits.Length > 0)
            {
                foreach (var item in dr_lunits)
                {
                    freeval = Convert.ToDecimal(item["VALUE"]);
                    strmin = item["A"].ToString();
                    cnty = item["COUNTRY_TO"].ToString().ToUpper();
                }
            }
            string col = "";
            for (int i = 0; i < (GridView1.Rows.Count); i++)
            {
                TextBox txt_dr = (TextBox)GridView1.Rows[i].FindControl("txtrate");
                Label txtlblunits = (Label)GridView1.Rows[i].FindControl("lblunits");
                Label txtllbldcost = (Label)GridView1.Rows[i].FindControl("lbldcost");

                Label txtnum = (Label)GridView1.Rows[i].FindControl("lblnum");

                if (Information.IsNumeric(txt_dr.Text) == true)
                {
                    string str_rate = "";
                    string strnumv = txtnum.Text.Substring(0, 2);
                    DataRow[] dr = dtrowdata.Select("isdcode = '" + strnumv + "'", "");
             
                    if (dr.Length == 0)
                    {
                        //check for size=28 in DataRow[]

                        col += strnumv+ ";";
                      //  col += strnumv + ";";
                      // col[0] = strnumv;

                        Label2.Visible = true;
                        Label2.Text = "Country codes not available in Database ";
                        txtnum.ForeColor = System.Drawing.Color.Red;
                        
                    }
                    string strcut = "";
                    foreach (var item in dr)
                    {
                        strcut = item["country"].ToString().Replace('+', ' ').TrimStart().Trim();
                        break;
                    }
                    if (strcut == ddlcountry.SelectedItem.ToString())
                    {
                        DataRow[] dr_l = dt.Select("CHARGESTYPE = 'LOCAL OUTGOING CALL'", "");
                        if (dr_l.Length > 0)
                        {
                            if (strmin.ToUpper() == "MINUTES")
                            {
                                if (cnty == "OTHER")
                                {
                                    othersunits = Convert.ToInt32(txtlblunits.Text) + othersunits;
                                    if (othersunits > freeval)
                                    {
                                        str_rate = dr_l[0]["VALUE"].ToString();
                                    }
                                    else
                                    {
                                        str_rate = "0";
                                    }
                                }
                                else
                                {
                                    foreach (var item in dr_l)
                                    {
                                        str_rate = dr_l[0]["VALUE"].ToString();
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                str_rate = dr_l[0]["VALUE"].ToString();
                            }
                        }
                    }
                    else if (strcut.ToUpper() == "INDIA")
                    {

                        DataRow[] dr_l = dt.Select("CHARGESTYPE = 'INDIA OUTGOING CALL'", "");
                        if (dr_l.Length > 0)
                        {
                            if (strmin.ToUpper() == "MINUTES")
                            {
                                if (cnty == "OTHER")
                                {
                                    othersunits = Convert.ToInt32(txtlblunits.Text) + othersunits;
                                    if (othersunits > freeval)
                                    {
                                        str_rate = dr_l[0]["VALUE"].ToString();
                                    }
                                }
                                else
                                {
                                    indiaunits = Convert.ToInt32(txtlblunits.Text) + indiaunits;
                                    if (indiaunits > freeval)
                                    {
                                        str_rate = dr_l[0]["VALUE"].ToString();
                                    }
                                    else
                                    {
                                        str_rate = "0";
                                    }
                                }
                            }
                            else
                            {
                                str_rate = dr_l[0]["VALUE"].ToString();
                            }
                        }

                        else
                        {
                            DataRow[] dr_l41 = dt.Select("CHARGESTYPE = 'OTHER COUNTRIES OUTGOING CALL' and country_to = '" + strcut + "' ", "");
                            if (dr_l.Length > 0)
                            {
                                if (strmin.ToUpper() == "MINUTES")
                                {
                                    if (cnty == "OTHER")
                                    {
                                        othersunits = Convert.ToInt32(txtlblunits.Text) + othersunits;
                                        if (othersunits > freeval)
                                        {
                                            str_rate = dr_l41[0]["VALUE"].ToString();
                                        }
                                        else
                                        {
                                            str_rate = "0";
                                        }
                                    }
                                    else
                                    {
                                        foreach (var item in dr_l)
                                        {
                                            str_rate = dr_l41[0]["VALUE"].ToString();
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    str_rate = dr_l41[0]["VALUE"].ToString();
                                }
                            }
                            else
                            {
                                DataRow[] dr_oth = dt.Select("CHARGESTYPE = 'OTHER COUNTRIES OUTGOING CALL' and country_to = 'Other' ", "");
                                if (dr_oth.Length > 0)
                                {
                                    if (strmin.ToUpper() == "MINUTES")
                                    {
                                        if (cnty == "OTHER")
                                        {
                                            othersunits = Convert.ToInt32(txtlblunits.Text) + othersunits;
                                            if (othersunits > freeval)
                                            {
                                                str_rate = dr_oth[0]["VALUE"].ToString();
                                            }
                                            else
                                            {
                                                str_rate = "0";
                                            }
                                        }
                                        else
                                        {
                                            foreach (var item in dr_l)
                                            {
                                                str_rate = dr_oth[0]["VALUE"].ToString();
                                                break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        str_rate = dr_oth[0]["VALUE"].ToString();
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        DataRow[] dr_l = dt.Select("CHARGESTYPE = 'OTHER COUNTRIES OUTGOING CALL' and country_to = '" + strcut + "' ", "");
                        if (dr_l.Length > 0)
                        {
                            if (strmin.ToUpper() == "MINUTES")
                            {
                                othersunits = Convert.ToInt32(txtlblunits.Text) + othersunits;
                                if (othersunits > freeval)
                                {
                                    foreach (var item in dr_l)
                                    {
                                        str_rate = item["VALUE"].ToString();
                                        break;
                                    }
                                }
                                else
                                {
                                    foreach (var item in dr_l)
                                    {
                                        str_rate = "0";  //item["VALUE"].ToString();
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                str_rate = dr_l[0]["VALUE"].ToString();
                            }
                        }
                        else
                        {
                            DataRow[] dr_oth = dt.Select("CHARGESTYPE = 'OTHER COUNTRIES OUTGOING CALL' and country_to = 'Other' ", "");
                            if (dr_oth.Length > 0)
                            {
                                if (strmin.ToUpper() == "MINUTES")
                                {
                                    if (cnty == "OTHER")
                                    {
                                        othersunits = Convert.ToInt32(txtlblunits.Text) + othersunits;
                                        if (othersunits > freeval)
                                        {
                                            str_rate = dr_oth[0]["VALUE"].ToString();
                                        }
                                        else
                                        {
                                            str_rate = "0";
                                        }
                                    }
                                    else
                                    {
                                        str_rate = dr_oth[0]["VALUE"].ToString();
                                    }

                                }
                                else
                                {
                                    str_rate = dr_oth[0]["VALUE"].ToString();
                                }
                            }
                        }
                    }

                    //   DataRow[] drtp = TARIFDATA.Select("CHARGESTYPE = 'OTHER COUNTRIES OUTGOING CALL'");

                    txt_dr.Text = str_rate == "" ? "0" : str_rate; // dt.Rows[0]["LOCALOUTGOINGCALL"].ToString();  


                    decimal dval = Convert.ToDecimal(txtlblunits.Text) * Convert.ToDecimal(str_rate == "" ? "0" : str_rate);
                    txtllbldcost.Text = dval.ToString();
                }
                Label txt_dv = (Label)GridView1.Rows[i].FindControl("lbldtime");

                Label txt_dtime = (Label)GridView1.Rows[i].FindControl("LabelDATE");  

                Label txt_orgin = (Label)GridView1.Rows[i].FindControl("LabelORIGIN");

                Label txt_daytime = (Label)GridView1.Rows[i].FindControl("LabelDAYDATE");


                if (txt_dv.Text == "SMS" && txt_dv != null || txt_dtime.Text == "SMS" && txt_dtime != null || txt_orgin.Text == "SMS" && txt_orgin != null || txt_daytime.Text == "SMS" && txt_daytime != null)
                {
                    for (int ct = i; ct < (GridView1.Rows.Count); ct++)
                    {
                        i++;
                        TextBox txtr = (TextBox)GridView1.Rows[ct].FindControl("txtrate");
                        if (Information.IsNumeric(txtr.Text) == true)
                        {

                            //==============================================================================
                            Label txt_num = (Label)GridView1.Rows[ct].FindControl("lblnum");
                            string str_te = "";
                            string strnumv = txt_num.Text.Substring(0, 2);

                            string str_rate = "";

                            DataRow[] dr = dtrowdata.Select("isdcode = '" + strnumv + "'", "");
                            string strcut = "";
                            foreach (var item in dr)
                            {
                                strcut = item["country"].ToString();
                                break;
                            }
                            if (strcut == ddlcountry.SelectedItem.ToString())
                            {
                                DataRow[] dr_l = dt.Select("CHARGESTYPE = 'LOCAL SMS'", "");
                                foreach (var item in dr_l)
                                {
                                    str_rate = item["VALUE"].ToString();
                                    break;
                                }
                            }
                            else
                            {
                                DataRow[] dr_l = dt.Select("CHARGESTYPE = 'INTL SMS'", "");
                                foreach (var item in dr_l)
                                {
                                    str_rate = item["VALUE"].ToString();
                                    break;
                                }

                            }
                            //==============================================================================
                            Label txtlbl_units = (Label)GridView1.Rows[ct].FindControl("lblunits");
                            Label txtllbl_dcost = (Label)GridView1.Rows[ct].FindControl("lbldcost");
                            txtr.Text = str_rate == "" ? "0" : str_rate; // dt.Rows[0]["LOCALSMS"].ToString();
                            decimal dval = Convert.ToDecimal(txtlbl_units.Text) * Convert.ToDecimal(str_rate == "" ? "0" : str_rate);
                            txtllbl_dcost.Text = dval.ToString();
                        }
                    }

                }

                if (txt_dv.Text == "DATA" && txt_dv != null || txt_dtime.Text == "DATA PAY PER USE" && txt_dtime != null || txt_orgin.Text == "DATA" && txt_orgin != null || txt_daytime.Text == "SMS" && txt_daytime != null)
                {
                    for (int ct = i; ct < (GridView1.Rows.Count); ct++)
                    {
                        i++;
                        TextBox txtr = (TextBox)GridView1.Rows[ct].FindControl("txtrate");
                        if (Information.IsNumeric(txtr.Text) == true)
                        {
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            //==============================================================================
                            //Label txt_num = (Label)GridView1.Rows[ct].FindControl("lblnum");
                            //string str_te = "";
                            //string strnumv = txt_num.Text.Substring(0, 2);

                            string str_rate = "";

                            //DataRow[] dr = dtrowdata.Select("isdcode = '" + strnumv + "'", "");
                            string strcut = "";
                            //foreach (var item in dr)
                            //{
                            //    strcut = item["country"].ToString();
                            //    break;
                            //}

                            string rateval = "";
                            if (strcut != ddlcountry.SelectedItem.ToString())
                            {
                                Label units = (Label)GridView1.Rows[ct].FindControl("lblunits");
                                string[] stmbcharges = DropDownListBoltion.SelectedItem.ToString().Split(' ');

                                string mbdata = "";
                                if (stmbcharges[1] == "GB")
                                {
                                    mbdata = (Convert.ToDecimal(stmbcharges[0]) * 1024).ToString();
                                }
                                else 
                                    mbdata= stmbcharges[0].ToString();

                                if (Convert.ToDecimal(units.Text) > Convert.ToDecimal(mbdata))
                                {
                                    decimal _ddata = 0;
                                    decimal _dvalue = 0;
                                    DataRow[] dr_l = dt.Select("CHARGESTYPE = 'DATA PAY PER USE'", "");
                                    if (dr_l.Length > 0)
                                    {
                                        _dvalue = Convert.ToDecimal( dr_l[0]["VALUE"]);

                                    }
                                    _ddata = Convert.ToDecimal(units.Text) - Convert.ToDecimal(stmbcharges[0]);
                                    rateval = _dvalue.ToString();
                                    str_rate = (_dvalue * _ddata).ToString();
                                         
                                }
                                else
                                {

                                    rateval = "0";
                                    str_rate = "0";
                                }
                            }


                            //==============================================================================
                            Label txtlbl_units = (Label)GridView1.Rows[ct].FindControl("lblunits");
                            Label txtllbl_dcost = (Label)GridView1.Rows[ct].FindControl("lbldcost");
                            txtr.Text = rateval == "" ?  "0" : rateval; // dt.Rows[0]["LOCALSMS"].ToString();
                         //  decimal dval = Convert.ToDecimal(str_rate) * Convert.ToDecimal(str_rate == "" ? "0" : str_rate);
                            txtllbl_dcost.Text = str_rate;
                           
                        }
                    }

                }

            }
           

        }
        protected void ddltariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListBoltion.Items.Clear();
            TextBox1.Text = "";
            datawithminats();
            boltion();
          
            

        }


        public void datawithminats()
        {
            TextboxExample.Text = "";
            DropDownExTextboxExample.Items.Clear();
            DropDownExTextboxExample1.Items.Clear();
            BILLDAL bal = new BILLDAL();
            DataTable dt = bal.tariff_dataFill(ddltariff.SelectedItem.ToString());
            DataRow[] dtt_rif = dt.Select("CHARGESTYPE ='DATA PAY PER USE' ");
            foreach (var item in dtt_rif)
            {
                DropDownExTextboxExample1.Items.Add(item["A"].ToString());
                DropDownExTextboxExample.Items.Add(item["VALUE"].ToString());
                TextboxExample.Text = item["VALUE"].ToString();
            }

            DataRow[] dr_l1 = dt.Select("CHARGESTYPE = 'RENTAL'", "");
            if (dr_l1.Length > 0)
            {
                System.Web.HttpContext.Current.Session["rntl"] = dr_l1[0]["VALUE"].ToString();
            }
            else
                System.Web.HttpContext.Current.Session["rntl"] = 0;

            DataRow[] dr_lfree = dt.Select("CHARGESTYPE = 'FREE VALUE'", "");
            if (dr_lfree.Length > 0)
            {
                System.Web.HttpContext.Current.Session["Free_Value"] = dr_lfree[0]["VALUE"].ToString();
            }
            else
                System.Web.HttpContext.Current.Session["Free_Value"] = 0;
        

        //==================================================================== Bolton====================
            DataRow[] dr_l18 = dt.Select("CHARGESTYPE = 'BOLTONS'", "");
            if (dr_l1.Length > 0)
            {
                System.Web.HttpContext.Current.Session["rntl8"] = dr_l18[0]["VALUE"].ToString();
            }
            else
                System.Web.HttpContext.Current.Session["rntl8"] = 0;

        //=============================================================================End
        }
       
        protected void DropDownListBoltion_SelectedIndexChanged(object sender, EventArgs e)
        {

            TextBox1.Text = "";

           // DropDownList1.Items.Clear();
                SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;");
                SqlCommand cmd = new SqlCommand();
                con.Open();
                string str = "select value from AddTarif where country ='" + ddlcountry.SelectedItem.ToString() + "' and tariff_code='" + ddltariff.SelectedItem.ToString() + "' and chargestype='BOLTONS' and A='" + DropDownListBoltion.SelectedItem.ToString() + "'";

                cmd = new SqlCommand(str, con);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();
                da.Fill(ds);
                TextBox1.Text = ds.Tables[0].Rows[0][0].ToString();
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{

                //    DropDownList1.Items.Add(ds.Tables[0].Rows[i][0].ToString());

                //}

                con.Close();


           
            freeVackvalue();



        }
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        void boltion()
        {


            try
            {

                // DropDownList2.Items.Clear();
                SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;");
                SqlCommand cmd = new SqlCommand();
                con.Open();
                string str = "select A from AddTarif where country ='" + ddlcountry.SelectedItem.ToString() + "' and tariff_code='" + ddltariff.SelectedItem.ToString() + "' and chargestype='BOLTONS'";
               
                cmd = new SqlCommand(str, con);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();
                da.Fill(ds);


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    DropDownListBoltion.Items.Add(ds.Tables[0].Rows[i][0].ToString());

                }



                con.Close();
            
            
            
            }
            catch
            { }

            boltion2();
        
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void boltion2()
        {
            
          

            try
            {

                // DropDownList2.Items.Clear();
                SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;");
                SqlCommand cmd = new SqlCommand();
                con.Open();
                string str = "select value from AddTarif where country ='" + ddlcountry.SelectedItem.ToString() + "' and tariff_code='" + ddltariff.SelectedItem.ToString() + "' and chargestype='BOLTONS'";

                cmd = new SqlCommand(str, con);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();
                da.Fill(ds);
                TextBox1.Text = ds.Tables[0].Rows[0][0].ToString();
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{

                //    DropDownList1.Items.Add(ds.Tables[0].Rows[i][0].ToString());

                //}

                con.Close();


            }
            catch
            { }
            freeVackvalue();

        }

     ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        class ExampleData
        {

            public string DATE { get; set; }
            public string TIME { get; set; } 
            public string ORIGIN { get; set; }
            public string DAYDATE { get; set; }
            
            public string DATETIME { get; set; }
            public string NUMBER { get; set; }
            public string TYPE { get; set; }
            public string UNITS { get; set; }
            public string DURATION { get; set; }
            public string RATE { get; set; }
            public string COST { get; set; }


            //public override string ToString()
            //{
            //    return KillingOccurance.PadRight(8, ' ') + "\t\t\t | " + NumberResults.ToString().PadLeft(3, ' ');
            //}
        }
        internal class Example_Data
        {
            public string DATE { get; set; }
            public string TIME { get; set; }
            public string ORIGIN { get; set; }
            public string DAYDATE { get; set; }

            public string DATETIME { get; set; }
            public string NUMBER { get; set; }
            public string TYPE { get; set; }
            public string UNITS { get; set; }
            public string DURATION { get; set; }
            public string RATE { get; set; }
            public string COST { get; set; }


            //public override string ToString()
            //{
            //    return KillingOccurance.PadRight(8, ' ') + "\t\t\t | " + NumberResults.ToString().PadLeft(3, ' ');
            //}
        }

        protected void DropDownExTextboxExample_SelectedIndexChanged(object sender, EventArgs e)
        {
          
          //  boltion();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                decimal a = Convert.ToDecimal(TextboxExample.Text == "" ? "0" : TextboxExample.Text);
                decimal b = Convert.ToDecimal(TextBox1.Text == "" ? "0" : TextBox1.Text);
                decimal sum = b - a;
                TextBox23.Text = sum.ToString();
                fillallvalue();
            }
            catch { }
            
        }
        void freeVackvalue()
        {


            try
            {

                // DropDownList2.Items.Clear();
                SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;");
                SqlCommand cmd = new SqlCommand();
                con.Open();
                // string str = "select value from AddTarif where country ='" + ddlcountry.SelectedItem.ToString() + "' and tariff_code='" + ddltariff.SelectedItem.ToString() + "' and chargestype='BOLTONS'";
                string st = "select VALUE from addtarif where A='TALK VALUE' and CHARGESTYPE='FREE VALUE' and TARIFF_CODE='" + ddltariff.SelectedItem.ToString() + "' and country='" + ddlcountry.SelectedItem.ToString() + "'";

                cmd = new SqlCommand(st, con);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();
                da.Fill(ds);
                Label1.Text = ds.Tables[0].Rows[0][0].ToString();

                con.Close();


            }
            catch
            { }
        }

        protected void DropDownExTextboxExample_SelectedIndexChanged1(object sender, EventArgs e)

        {
           

        }



    }
}