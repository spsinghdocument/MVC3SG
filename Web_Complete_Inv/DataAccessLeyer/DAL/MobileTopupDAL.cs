using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
   public class MobileTopupDAL
    {
        inventoryforwebappEntities inv; 
            bl.Class1 DGM;
        public MobileTopupDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }
        public string mobileTopDAL(string f, string country, string mobileno, string plancode, string topup, string topup_data, string topup_sptopup, string total, string loginuser, string branchcode)
        {
            string Msg = string.Empty;
            int t = 0;
            try
            {
                t = inv.mobile_topup_proc_insert(f, 1, country, mobileno,plancode , topup, topup_data, topup_sptopup, total, loginuser, branchcode);
                if (t > 0)
                {
                    Msg = "Record Insert Successfully";
                }
                else
                    Msg = "Please Check All Fields";
            }
            catch (Exception ex)
            {                
               Msg = ex.Message ;
            }
          return Msg;
        }

        public List<AlotUser> SearchSim_No_mobileTopUp(string empName, string country, string type, string usercode, string branchcode)
        {
            List<AlotUser> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.PurchaseEntries
                   .Where(em => em.status == "IN USE" && em.Country == country && em.PhoneNo.Contains(empName)).Select(c => new AlotUser
                   {
                       simno = c.PhoneNo
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.PurchaseEntries
                  .Where(em => em.status == "IN USE" && em.Country == country && em.branchcode == branchcode && em.PhoneNo.StartsWith(empName)).Select(c => new AlotUser
                  {
                      simno = c.PhoneNo
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.PurchaseEntries
                  .Where(em => em.status == "IN USE" && em.Country == country && em.branchcode == branchcode && em.PhoneNo.Contains(empName)).Select(c => new AlotUser
                  {
                      simno = c.PhoneNo
                  }).ToList();
            }
            return custd;
        }




        public List<AlotUser> fillcomboDAL(string empName, string country, string type, string usercode, string branchcode)
        {
            List<AlotUser> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.PlanTables
                   .Where(em => em.COUNTRY == country && em.PLAN_TYPE == "TOPUP").Select(c => new AlotUser
                   {
                       simno = c.PLAN_CODE
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.PlanTables
                  .Where(em => em.COUNTRY == country && em.BRANCHCODE == branchcode && em.PLAN_TYPE == "TOPUP").Select(c => new AlotUser
                  {
                      simno = c.PLAN_CODE
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.PlanTables
                  .Where(em => em.COUNTRY == country && em.BRANCHCODE == branchcode && em.PLAN_TYPE == "TOPUP").Select(c => new AlotUser
                  {
                      simno = c.PLAN_CODE
                  }).ToList();
            }
            return custd;
        }

        public List<plantableproc> filldatavalue(string plancode , string ptype)
        {

            string strqry = "";
            switch (ptype)
            {
                case "TOPUP" :
                    strqry = "Select TALKTIMEDATA as TALKTIMEDATA from PlanTable where PLAN_CODE = '" + plancode + "' and PLAN_TYPE = 'TOPUP'";
                    break;

                case "DATATOPUP":
                    strqry = "Select TALKTIMEDATA as TALKTIMEDATA from PlanTable where PLAN_CODE = '" + plancode + "' and PLAN_TYPE = 'DATATOPUP'";
                    break;

                case "SPDATATOPUP":
                    strqry = "Select TALKTIMEDATA as TALKTIMEDATA from PlanTable where PLAN_CODE = '" + plancode + "' and PLAN_TYPE = 'SPLDATATOPUP' and statu_s = 'NOT ACTIVE'";
                    break;                   
            }

            DGM.plantableproc = inv.ExecuteStoreQuery<bl.plantableproc>(strqry).ToList();
            return DGM.plantableproc;
        }



        public List<MOBILETOPUP_DATA> fillrequestDAL( string usertype , string branchcode)
        {
            string strqry = "";

            if (usertype == "ADMIN")
            {
                strqry = "select topid , request_date,plancode, country,mobileno,(select user_name from clientmaster where sim_no =" +
                       "(select simno from purchaseentry where phoneno = '22')) as Clientname,topup,topup_data,topup_sptopup,total,loginuser ,branchcode from mobile_topup where statu_s = 'NOT ACCEPT' ";
            }
            else if (usertype == "PARTNER")
            {
                strqry = "select topid , request_date,plancode, country,mobileno,(select user_name from clientmaster where sim_no =" +
                             "(select simno from purchaseentry where phoneno = '22' and branchcode = '" + branchcode + "')) as Clientname,topup,topup_data,topup_sptopup,total,loginuser ,branchcode from mobile_topup where statu_s = 'NOT ACCEPT' and branchcode ='"+branchcode+"' ";
            }
          
                 
            try
            {
                DGM.MOBILETOPUP_DATA = inv.ExecuteStoreQuery<bl.MOBILETOPUP_DATA>(strqry).ToList();
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return DGM.MOBILETOPUP_DATA;
        }
        public string updateacceptDAL(string id, string resultval, string loginuser , string tdata)
        {
            string Msg = "";
            string strqry = "update mobile_topup set statu_s = '" + resultval + "' where topid = '" + id + "'  " +
                           "update user_name set updatedata = " + tdata + "+cast(isnull((select updatedata from User_Name where User_Code ='" + loginuser + "'),0) as bigint) where User_Code = '" + loginuser + "'";
            int t = inv.ExecuteStoreCommand(strqry);
            if (t > 0)
            {
                Msg = resultval + " REQUEST";
            }
            return Msg;
        }


        public List<MOBILETOPUP_DATA> ExecChkVal(string execcode , string usertype , string branchcode)
        {
            string strqry = "";
            if (usertype == "ADMIN")
            {
                strqry = "select request_date,plancode ,branchcode,mobileno,topup,topup_data,topup_sptopup , statu_s from mobile_topup";
            }
            else if (usertype == "PARTNER")
            {
                strqry = "select request_date,plancode ,branchcode,mobileno,topup,topup_data,topup_sptopup , statu_s from mobile_topup where branchcode = '" + branchcode + "' ";
            }
         
          //  strqry = "select request_date,plancode ,branchcode,mobileno,topup,topup_data,topup_sptopup , statu_s from mobile_topup";

            try
            {
                DGM.MOBILETOPUP_DATA = inv.ExecuteStoreQuery<bl.MOBILETOPUP_DATA>(strqry).ToList();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            return DGM.MOBILETOPUP_DATA;
        }
    }
}
