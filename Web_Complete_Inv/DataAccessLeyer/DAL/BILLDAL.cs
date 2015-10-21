using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;
using DataAccessLeyer.EF;
using System.Data;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace DataAccessLeyer.DAL
{
   public class BILLDAL
    {
       inventoryforwebappEntities inv;

       bl.Class1 DGM;
       public BILLDAL()
       {
           DGM = new bl.Class1();
           inv = new inventoryforwebappEntities();
       }
       public List<vafsearchdata> withcafno(string strquery)
        {
            DGM.vafsearchdata = inv.ExecuteStoreQuery<bl.vafsearchdata>(strquery).ToList();
            return DGM.vafsearchdata.ToList();
        }

       public string file_Data(string f , string date , string username ,string user , string billNo , string accountCode ,string country , string agreementno , string mobileno ,
           string billdate , string duedate , string billperiod ,string currency1 , string currency2 , string rental ,string useges , string aopfubs ,
           string totalamount , string amountininr , string servicetax , string amountpayable, string afterduedate, string conversuin ,string excelFile,string branchcode , string loginusercode)
       {
         int t =  inv.billsummaryproc_Insert(f, 1, date, username, user, billNo, accountCode, country, agreementno, mobileno, billdate, duedate,
               billperiod, currency1, currency2, rental, useges, aopfubs, totalamount, amountininr, servicetax, amountpayable, afterduedate, conversuin, excelFile,branchcode,loginusercode);
         if (t > 0)
         {
             return "Record Saved Successfully....";
         }
         else
           return "Please Chech All Field Carefully...";
       }

       public string PROFILEDATA(string PROFILE_NAME, string A, string B, string C, string D, string E, string F, string G, string H, string BRANCHCODE, string LOGINUSERCODE)
       {
           string Msg =   "";
           try
           {
               int T = inv.PROFILE_TABLE_PROC("Insert", 1, PROFILE_NAME, A, B, C, D, E, F, G, H, BRANCHCODE, LOGINUSERCODE);
               if (T > 0)
               {
                   Msg = "RECORD INSERT SUCCESSFULLY";
               }
               else
               {
                   Msg = "PLEASE CHECK ALL FIELD";
               }
           }
           catch (Exception EX)
           {
               Msg = EX.Message;
               
           }
          return Msg;
       }

       public List<CustAcno> Tariffcodefill_withcountry(string country , string type, string usercode, string branchcode)
       {
           List<CustAcno> custd = null;
           if (type == "ADMIN")
           {
               custd = inv.ADDTARIFs.Where(em => em.COUNTRY == country).Distinct()
                 .Select(c => new CustAcno
                 {
                     tariffcode = c.TARIFF_CODE
                 }).Distinct().ToList();
           }
           else if (type == "PARTNER")
           {
               custd = inv.ADDTARIFs.Where(em => em.COUNTRY == country).Distinct()
                 .Where(em => em.B == branchcode).Select(c => new CustAcno
                 {
                     tariffcode = c.TARIFF_CODE
                 }).Distinct().ToList();
           }
           //else if (type == "EXECUTIVE")
           //{
           //    custd = inv.ADDTARIFs.Where(em => em.COUNTRY == country).Distinct()
           //     .Where(em => em.B == branchcode && em.C == usercode).Select(c => new CustAcno
           //     {
           //         tariffcode = c.TARIFF_CODE
           //     }).Distinct().ToList();
           //}
           else if (type == "EXECUTIVE")
           {
               custd = inv.ADDTARIFs.Where(em => em.COUNTRY == country).Distinct()
              .Select(c => new CustAcno
                {
                    tariffcode = c.TARIFF_CODE
                }).Distinct().ToList();
           }
           return custd;
       }

       //public List<TARIFRET> tarifupload(string tcode )
       public System.Data.DataTable tarifupload(string tcode)
       {
           System.Data.DataTable dt = new System.Data.DataTable();


           //string strquery = "select distinct (select distinct VALUE from ADDTARIF where TARIFF_CODE = '" + tcode + "' and CHARGESTYPE = 'RENTAL' ) as RENTAL," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'VALIDITY') as VALIDITY," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'INCOMING CALL') as INCOMING," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'FREE VALUE') as FREEVAL," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'LOCAL OUTGOING CALL') as LOCALOUTGOINGCALL," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'INDIA OUTGOING CALL') as INDIAOUTGOINGCALL," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'OTHER COUNTRIES OUTGOING CALL') as OTHERCOUNTRIESOUTGOINGCALL," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'LOCAL SMS') as LOCALSMS," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'INTL TEXT') as INTLSMS," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'CUG CALLING') as CUGCALLING," +
           //     " (select distinct VALUE    from ADDTARIF where TARIFF_CODE =  '" + tcode + "' and CHARGESTYPE = 'DATA PAY PER USE') as DATAPAYPERUSE " +
           //      " from ADDTARIF where TARIFF_CODE = '" + tcode + "'";
           string strquery = "select * from addtarif where tariff_code = '" + tcode + "'";
           using(SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
           using(SqlCommand cmd = new SqlCommand(strquery, con))
           using (SqlDataAdapter da = new SqlDataAdapter(cmd))
           {
               da.Fill(dt);
               //try
               //{
               //    DGM.TARIFRET = inv.ExecuteStoreQuery<bl.TARIFRET>(strquery).ToList();
               //}
               //catch (Exception ex)
               //{

               //    throw ex.InnerException;
               //}
           }

           return dt; // DGM.TARIFRET.ToList();
       }
       public System.Data.DataTable  country_name()
       {
           System.Data.DataTable dtv = new System.Data.DataTable();
           string strvqry = "select country , isdcode from countrylist";
            using(SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(strvqry, con))
           using (SqlDataAdapter da = new SqlDataAdapter(cmd))
           {
               da.Fill(dtv);
           }
            return dtv;
       }

       public System.Data.DataTable tariff_dataFill(string tcode)
       {
           System.Data.DataTable dtv = new System.Data.DataTable();
           string strvqry = "select * from addtarif where tariff_code = '"+tcode+"'";
           using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString))
           using (SqlCommand cmd = new SqlCommand(strvqry, con))
           using (SqlDataAdapter da = new SqlDataAdapter(cmd))
           {
               da.Fill(dtv);
           }
           return dtv;
       }
    }
}
