using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl= BusinessAccessLeyer;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLeyer.DAL
{
   public class BILLINSERT_DAL
    {
       inventoryforwebappEntities inv; bl.Class1 DGM;

       public BILLINSERT_DAL()
       {
           inv = new inventoryforwebappEntities();
           DGM =  new bl.Class1();
       }

       public string insertmailsettingsDAL(string f ,string  SMTP_ADDRESS ,string PORT_NUMBER ,bool ENABLE_SSL ,string EMAIL_FROM,
           string PASSWORD,string SUBJECT ,string BODY ,string LOGINUSER ,string BRANCHCODE)
       {
           string msg = "";
         int t =   inv.MAILSETTINGTABLE_PROC(1, "Insert", SMTP_ADDRESS, PORT_NUMBER, ENABLE_SSL, EMAIL_FROM, PASSWORD, SUBJECT, BODY, LOGINUSER, BRANCHCODE, "a", "b");
         if (t > 0)
         {
             msg = "Record Insert Successfully..";
         }
         else
         {
             msg = "Please Check Field..";
         }
         return msg;
       }

       public string insertdatadal(string f , string  cafno,string billno,string  mobileno,string username,string country,string excelfilename,string branchcode)
       {
           string Msg = "";
           int t = 0;
           try
           {
                t = inv.billexcelproc(f, 1, cafno, billno, mobileno, username, country, excelfilename, "0", "0", "0", branchcode);
           }
           catch (Exception ex)
           {

               Msg = ex.InnerException.Message;
           }
          if (t > 0)
          {
              Msg = "Record Inserted Successfully..";
          }
          else
          {
              Msg = "Please Check Field";
          }
          return Msg;
       }

       public List<SENDMAIL_PROC_Result> sendmailDAL(string cafno , string branchcode)
       {
           return inv.SENDMAIL_PROC("Maildata", cafno, branchcode).ToList();
       }

       public List<BILLEXCEL_PROP> ExecllChkVal(string usertype , string branchcode)
       {
           string strqry = "";
           if (usertype == "ADMIN")
           {
               strqry = "select cafno,billno ,mobileno,country , excelfilename from billexcel";
           }
           else if (usertype == "PARTNER")
           {
               strqry = "select cafno,billno ,mobileno,country , excelfilename from billexcel where branchcode = '" + branchcode + "'";
           }

           else if (usertype == "EXECUTIVE")
           {
               strqry = "select cafno,billno ,mobileno,country , excelfilename from billexcel where branchcode = '" + branchcode + "'";
           }

          

           try
           {
               DGM.BILLEXCEL_PROP = inv.ExecuteStoreQuery<bl.BILLEXCEL_PROP>(strqry).ToList();
           }
           catch (Exception ex)
           {

               throw ex.InnerException;
           }
           return DGM.BILLEXCEL_PROP;
       }

       public DataTable settingval(string qry)
       {
           DataTable dt = new DataTable();
          using( SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;"))
          using (SqlCommand cmd = new SqlCommand(qry, con))
          using( SqlDataAdapter da =  new SqlDataAdapter(cmd))
          {
              da.Fill(dt);
          }
          return dt;
       }
           
    }
}
