using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;


namespace DataAccessLeyer.DAL
{
   public  class ClientMasterDAL
    {
       inventoryforwebappEntities inv;
       bl.Class1 DGM;
        public ClientMasterDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public string insertclientmastervalidate(string f , string date, string cafno, string customeracno, Int64 fkchallanout, string pdffile, string User_Name, string Father_Name,
            string Category, string Payment_Mode, Decimal Amount, string Bank, string Cheque_No, string Sim_No,
            string Plan, string Validity, string Country, string Start_Date, string End_Date, string Sim_Return_Date,
            string Tarrif_Detail, string Executive_Name, string Executive_Code, string branchcode ,string status, string tfcode,string strredio , string itemname , string imeino,
           string amtstatus , string neft,string remark)
        {
            string Msg = string.Empty;
            ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));

            try
            {
                inv.IUDclientmaster(f, date, cafno, customeracno, 0, pdffile, User_Name, Father_Name,
                       Category, Payment_Mode, Amount, Bank, Cheque_No, Sim_No, "Sim", Plan, Validity, Country,
                       Start_Date, End_Date, Sim_Return_Date, Tarrif_Detail, Executive_Name, Executive_Code, branchcode, status,
                       tfcode, strredio,itemname ,imeino ,amtstatus ,neft,remark, opmg);
                Msg = Convert.ToString(opmg.Value);
            }
            catch (Exception ex)
            {
                Msg =  ex.Message;
            }
            return Msg;
        }

        public string updatedocdal(string query)
        {
            int t = 0; string Msg = "";
            try
            {
                t = inv.ExecuteStoreCommand(query);
                
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            if (t > 0)
            {
                Msg = "File Upload Successfully..";
            }
            else
                Msg = "File Not Uploaded..";
            return Msg;

           

        }

        public List<simsearch_Proc_SimSearch_Result> SimSearch(string type , string country, string execcode, string branchcode)
        {
            List<simsearch_Proc_SimSearch_Result> lstsh = null;
            try
            {
                lstsh = inv.simsearch_Proc_Search(type, country, execcode, branchcode).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return lstsh;
        }

        public List<CLIENTMASTER_PROPERTY> searchauitocomcustname(string Ac_no)
        {
            string strqry = "select distinct cl.User_Name,cd.customeracno ,cl.Father_Name,cd.customername from ClientMaster cl " +
                            " inner join customerdetailstable cd on cl.customeracno = cd.customeracno where  cd.customeracno = '" + Ac_no + "'";
            try{
            DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strqry).ToList();
            if (DGM.CLIENTMASTER_PROPERTY.Count == 0)
            {
                strqry = "select distinct  customername from customerdetailstable  where  customeracno = '" + Ac_no + "'";

                DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strqry).ToList();
            }
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();

            //List<string> custd = inv.customerdetailstables
            //     .Where(em => em.customeracno == Ac_no).Select<customerdetailstable, string>(em => em.customername).ToList();
            //return custd;

        }

        public List<string> SearchValidity(string simno , string branchcode , string usertype)
        {
            List<string> custd = null;
            try
            {

                custd = inv.PurchaseEntries
                   .Where(em => em.SimNo == simno && em.branchcode == branchcode).Select<PurchaseEntry, string>(em => em.Validity).ToList();
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return custd;

        }

        public List<CustAcno> searchautocomcustnameDAL(string empName , string type , string usercode , string  branchcode)
        {
            List<CustAcno> custd = null;
            try
            {
                if (type == "ADMIN")
                {
                    custd = inv.customerdetailstables
                       .Where(em => em.customeracno.Contains(empName)) .Select(c => new CustAcno
                           {
                               customeracno = c.customeracno
                           }).ToList();
                   

                }
                else if (type == "PARTNER")
                {
                    custd = inv.customerdetailstables
                      .Where(em => em.branchcode == branchcode && em.customeracno.StartsWith(empName)).Select(c => new CustAcno
                      {
                          customeracno = c.customeracno
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.customerdetailstables
                      .Where(em => em.branchcode == branchcode && em.loginusercode == usercode && em.customeracno.Contains(empName)).Select(c => new CustAcno
                      {
                          customeracno = c.customeracno
                      }).ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return custd;
        }


        public List<CustAcno> SearchCafDAL(string empName, string type, string usercode, string branchcode)
        {
            List<CustAcno> custd = null;
            try
            {
                if (type == "ADMIN")
                {
                    custd = inv.ClientMasters
                       .Where(em => em.cafno.Contains(empName)).Select(c => new CustAcno
                       {
                           cafno = c.cafno
                       }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.ClientMasters
                      .Where(em => em.branchcode == branchcode && em.cafno.StartsWith(empName)).Select(c => new CustAcno
                      {
                          cafno = c.cafno
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.ClientMasters
                      .Where(em => em.branchcode == branchcode  && em.cafno.Contains(empName)).Select(c => new CustAcno
                      {
                          cafno = c.cafno
                      }).ToList();
                }
            }
            catch (Exception ex)
            {                
                throw ex.InnerException ; 
            }
            return custd;
        }

     
       /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<CustAcno> SearchVOUCHERDAL(string empName, string type, string usercode, string branchcode)
        {
            List<CustAcno> custd = null;
            try
            {
                if (type == "ADMIN")
                {
                  //  custd = inv.vouchertables.Where(em => em.Voucherno >= 3);
                   // custd = inv.vouchertables.Select(em => new { em.Voucherno, });
                //        db.sps.Select(em =>
                //new { em.id, em.firstname });
                    custd = inv.vouchertables
                    .Where(em => em.Voucherno.Contains(empName)).Select(c => new CustAcno
                    {
                        Voucherno = c.Voucherno,
                        //cafno = c.cafno,
                        //Date=c.Date

                    }).Distinct().ToList();
                   
                }
                else if (type == "PARTNER")
                {
                    custd = inv.vouchertables
                      .Where(em => em.branchcode == branchcode && em.Voucherno.StartsWith(empName)).Select(c => new CustAcno
                      {
                          Voucherno = c.Voucherno
                      }).Distinct().ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.vouchertables
                      .Where(em => em.branchcode == branchcode && em.executivecode == usercode && em.Voucherno.Contains(empName)).Select(c => new CustAcno
                      {
                          Voucherno = c.Voucherno
                      }).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return custd;
        }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<CustAcno> SearchPlanDAL(string empName, string type, string usercode, string branchcode)
        {
            List<CustAcno> custd = null;
            try
            {
                if (type == "ADMIN")
                {
                    custd = inv.PlanTables
                      .Select(c => new CustAcno
                       {
                           plancode = c.PLAN_CODE
                       }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.PlanTables
                      .Where(em => em.BRANCHCODE == branchcode).Select(c => new CustAcno
                      {
                          plancode = c.PLAN_CODE
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.PlanTables
                      .Where(em => em.BRANCHCODE == branchcode && em.LOGINUSER == usercode).Select(c => new CustAcno
                      {
                          plancode = c.PLAN_CODE
                      }).ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return custd;
        }


        public List<CustAcno> SearchSimFromChallanOutMaster(string empName, string country, string type, string usercode, string branchcode)
        {
            List<CustAcno> custd = null;
            try
            {
                if (type == "ADMIN")
                {
                    custd = inv.challanouts.Where(em => em.status == "NOT IN USE" && em.country == country && em.others.StartsWith(empName))
                      .Select(c => new CustAcno
                      {
                          simno = c.others
                      }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.challanouts
                      .Where(em => em.status == "NOT IN USE" && em.country == country && em.branchcode == branchcode && em.others.StartsWith(empName)).Select(c => new CustAcno
                      {
                          simno = c.others
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.challanouts
                      .Where(em => em.status == "NOT IN USE" && em.country == country && em.branchcode == branchcode && em.executivenameto == usercode && em.others.StartsWith(empName)).Select(c => new CustAcno
                      {
                          simno = c.others
                      }).ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return custd;
        }




        public List<CustAcno> Tariffcodefill(string type, string usercode, string branchcode, string county)
        {
            List<CustAcno> custd = null;
            try
            {
                if (type == "ADMIN")
                {
                    custd = inv.ADDTARIFs.Where(c => c.COUNTRY == county)
                      .Select(c => new CustAcno
                      {
                          tariffcode = c.TARIFF_CODE
                      }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.ADDTARIFs.Where(c => c.COUNTRY == county)
                      .Where(em => em.B == branchcode).Select(c => new CustAcno
                      {
                          tariffcode = c.TARIFF_CODE
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.ADDTARIFs.Where(c => c.COUNTRY == county)
                      .Where(em => em.B == branchcode && em.C == usercode).Select(c => new CustAcno
                      {
                          tariffcode = c.TARIFF_CODE
                      }).ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException;
            }
            return custd;
        }



        public string autotfno(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.Clientmasterautotfno_proc(branchcode, "CLIENTMASTERTF", branchcode, obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return Msg;
        }

        public List<CLIENTMASTER_PROPERTY> all_search(string option  , string cname , string c_name , string usertype ,string loginuser , string branchcode)
        {

             string strquery = "";
            if(usertype == "ADMIN")
            {
                if (option == "All")
             {
                 strquery = "SELECT distinct max(cl.cafno) as cafno, max(cl.customeracno) as customeracno,max(cust.customername) as customername,"+
                 "max(cl.pdffile) as  pdffile,max(cl.User_Name) as User_Name,max(cl.Father_Name) as Father_Name,max(cl.Payment_Mode) as Payment_Mode,max(cl.Amount) as Amount,max(cl.Bank) as Bank,max(cl.Cheque_No) as Cheque_No,max(cl.Sim_No) as Sim_No," +
                "max(cl.mobileno) as mobileno,max(cl.[Plan]) as [Plan],max(cl.Validity) as Validity,max(cl.Country) as Country,max(cl.Start_Date) as Start_Date,max(cl.End_Date) as End_Date,max(cl.Sim_Return_Date) as Sim_Return_Date," +
                             "max(cl.Tarrif_Detail) as  Tarrif_Detail,max(cl.Executive_Name) as Executive_Name,max(cl.Executive_Code) as Executive_Code,max(cl.branchcode) as branchcode,max(cl.tfno) as tfno " +
                       " FROM ClientMaster cl inner join  customerdetailstable cust on cl.customeracno = cust.customeracno   group by cl.customeracno ORDER BY customeracno";

             }
             else
             {
                 strquery =  "SELECT distinct max(cl.cafno) as cafno, max(cl.customeracno) as customeracno,max(cust.customername) as customername,"+
                 "max(cl.pdffile) as  pdffile,max(cl.User_Name) as User_Name,max(cl.Father_Name) as Father_Name,max(cl.Payment_Mode) as Payment_Mode,max(cl.Amount) as Amount,max(cl.Bank) as Bank,max(cl.Cheque_No) as Cheque_No,max(cl.Sim_No) as Sim_No," +
                "max(cl.mobileno) as mobileno,max(cl.[Plan]) as [Plan],max(cl.Validity) as Validity,max(cl.Country) as Country,max(cl.Start_Date) as Start_Date,max(cl.End_Date) as End_Date,max(cl.Sim_Return_Date) as Sim_Return_Date," +
                             "max(cl.Tarrif_Detail) as  Tarrif_Detail,max(cl.Executive_Name) as Executive_Name,max(cl.Executive_Code) as Executive_Code,max(cl.branchcode) as branchcode,max(cl.tfno) as tfno " +
                     " FROM ClientMaster cl inner join  customerdetailstable cust on cl.customeracno = cust.customeracno  where cl." + cname + " = '" + c_name + "' ORDER BY customeracno";

             }
            }
            else
            {
                if (option == "All")
             {
                 strquery = "SELECT distinct max(cl.cafno) as cafno, max(cl.customeracno) as customeracno,max(cust.customername) as customername,"+
                 "max(cl.pdffile) as  pdffile,max(cl.User_Name) as User_Name,max(cl.Father_Name) as Father_Name,max(cl.Payment_Mode) as Payment_Mode,max(cl.Amount) as Amount,max(cl.Bank) as Bank,max(cl.Cheque_No) as Cheque_No,max(cl.Sim_No) as Sim_No," +
                "max(cl.mobileno) as mobileno,max(cl.[Plan]) as [Plan],max(cl.Validity) as Validity,max(cl.Country) as Country,max(cl.Start_Date) as Start_Date,max(cl.End_Date) as End_Date,max(cl.Sim_Return_Date) as Sim_Return_Date," +
                             "max(cl.Tarrif_Detail) as  Tarrif_Detail,max(cl.Executive_Name) as Executive_Name,max(cl.Executive_Code) as Executive_Code,max(cl.branchcode) as branchcode,max(cl.tfno) as tfno " +
                       " FROM ClientMaster cl inner join  customerdetailstable cust on cl.customeracno = cust.customeracno  where cl.branchcode = '" + branchcode + "' group by cl.customeracno";

             }
             else
             {
                 strquery =  "SELECT distinct max(cl.cafno) as cafno, max(cl.customeracno) as customeracno,max(cust.customername) as customername,"+
                 "max(cl.pdffile) as  pdffile,max(cl.User_Name) as User_Name,max(cl.Father_Name) as Father_Name,max(cl.Payment_Mode) as Payment_Mode,max(cl.Amount) as Amount,max(cl.Bank) as Bank,max(cl.Cheque_No) as Cheque_No,max(cl.Sim_No) as Sim_No," +
                "max(cl.mobileno) as mobileno,max(cl.[Plan]) as [Plan],max(cl.Validity) as Validity,max(cl.Country) as Country,max(cl.Start_Date) as Start_Date,max(cl.End_Date) as End_Date,max(cl.Sim_Return_Date) as Sim_Return_Date," +
                             "max(cl.Tarrif_Detail) as  Tarrif_Detail,max(cl.Executive_Name) as Executive_Name,max(cl.Executive_Code) as Executive_Code,max(cl.branchcode) as branchcode,max(cl.tfno) as tfno " +
                     " FROM ClientMaster cl inner join  customerdetailstable cust on cl.customeracno = cust.customeracno  where cl." + cname + " = '" + c_name + "'";

             }
            }
            try
            {
                DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strquery).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();
        }

        public List<CLIENTMASTER_PROPERTY> AC_Search(string option, string acno, string usertype, string loginuser, string branchcode)
        {
            string strquery = "";
            if (option == "ALL")
            {
                strquery = "select * from clientmaster where 1=1 ";
            }
            else
            {
                strquery = "SELECT  cafno,  customeracno,   pdffile,  User_Name,  Father_Name,  Payment_Mode,  Amount,  Bank,  Cheque_No,  Sim_No," +
                            "  mobileno,  [Plan],  Validity,  Country,  Start_Date,  End_Date,  Sim_Return_Date," +
                            "   Tarrif_Detail,  Executive_Name,  Executive_Code,  branchcode,  tfno " +
                      " FROM ClientMaster    where  customeracno = '" + acno + "'";

            }
            try
            {
                DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strquery).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();
        }

        public List<CLIENTMASTER_PROPERTY> Active_Postpond( string usertype, string loginuser, string branchcode)
        {
            string strquery = "";
            if (usertype == "ADMIN")
            {
                strquery = "SELECT  cafno,clmasterdate ,   customeracno,   pdffile,  User_Name,  Father_Name,  Payment_Mode,  Amount,  Bank,  Cheque_No,  Sim_No," +
                                "  mobileno,  [Plan],  Validity,  Country,  Start_Date,  End_Date,  Sim_Return_Date," +
                                "   Tarrif_Detail,  Executive_Name,  Executive_Code,  branchcode,  tfno ,REMARK as REMARK  " +
                          " FROM ClientMaster    where  status = 'NOT IN USE' order by  customeracno";

            }
            else if (usertype ==  "PARTNER")
            {
                 strquery = "SELECT  cafno,clmasterdate ,   customeracno,   pdffile,  User_Name,  Father_Name,  Payment_Mode,  Amount,  Bank,  Cheque_No,  Sim_No," +
                                "  mobileno,  [Plan],  Validity,  Country,  Start_Date,  End_Date,  Sim_Return_Date," +
                                "   Tarrif_Detail,  Executive_Name,  Executive_Code,  branchcode,  tfno ,REMARK as REMARK  " +
                          " FROM ClientMaster    where  status = 'NOT IN USE' and branchcode = '" + branchcode + "' order by  customeracno";

            }
          
            try{
            DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strquery).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();
        }
        public List<CLIENTMASTER_PROPERTY> Active_Postpond2(string ser, string usertype, string loginuser, string branchcode)
        {
            string strquery = "";
            if (usertype == "ADMIN")
            {
                strquery = "SELECT  cafno,clmasterdate ,   customeracno,   pdffile,  User_Name,  Father_Name,  Payment_Mode,  Amount,  Bank,  Cheque_No,  Sim_No," +
                                "  mobileno,  [Plan],  Validity,  Country,  Start_Date,  End_Date,  Sim_Return_Date," +
                                "   Tarrif_Detail,  Executive_Name,  Executive_Code,  branchcode,  tfno ,REMARK as REMARK  " +
                          " FROM ClientMaster    where  status = 'NOT IN USE' and Sim_No='" + ser + "' order by  customeracno";

            }
            else if (usertype == "PARTNER")
            {
                strquery = "SELECT  cafno,clmasterdate ,   customeracno,   pdffile,  User_Name,  Father_Name,  Payment_Mode,  Amount,  Bank,  Cheque_No,  Sim_No," +
                               "  mobileno,  [Plan],  Validity,  Country,  Start_Date,  End_Date,  Sim_Return_Date," +
                               "   Tarrif_Detail,  Executive_Name,  Executive_Code,  branchcode,  tfno ,REMARK as REMARK  " +
                         " FROM ClientMaster    where  status = 'NOT IN USE' and branchcode = '" + branchcode + "' and Sim_No='" + ser + "' order by  customeracno";

            }

            try
            {
                DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strquery).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();
        }

        public List<CLIENTMASTER_PROPERTY> sim_no_Search(string simno, string usertype, string loginuser, string branchcode)
        {
            string strquery = "";

            strquery = "SELECT  cafno,  customeracno,   pdffile,  User_Name,  Father_Name,  Payment_Mode,  Amount,  Bank,  Cheque_No,  Sim_No," +
                        "  mobileno,  [Plan],  Validity,  Country,  Start_Date,  End_Date,  Sim_Return_Date," +
                        "   Tarrif_Detail,  Executive_Name,  Executive_Code,  branchcode,  tfno " +
                  " FROM ClientMaster    where Sim_No = '" + simno + "' and  status = 'NOT IN USE'";


            try{
            DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strquery).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();
        }

        public int updateremark(string  remark , string cafno , string status )
        {
            string sta_update = string.Empty;
            if (status == "ACTIVE")
            {
                sta_update = "IN USE";
            }
            else
            {
                sta_update = "NOT IN USE";
            }

            string strquery = "";
            strquery = "update clientmaster set remark = '" + remark + "' , status = '" + sta_update + "' where cafno  = '" + cafno + "' ";
            return inv.ExecuteStoreCommand(strquery);            
        }


        public List<CLIENTMASTER_PROPERTY> cafno_search(string cafno,  string branchcode)
        {
            string strquery = "";

            strquery = "SELECT  cafno,sim_no,  customeracno,Country,remark FROM ClientMaster    where cafno = '" + cafno + "' and  status = 'NOT IN USE'";

            try
            {
                DGM.CLIENTMASTER_PROPERTY = inv.ExecuteStoreQuery<bl.CLIENTMASTER_PROPERTY>(strquery).ToList();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return DGM.CLIENTMASTER_PROPERTY.ToList();
        }

    }
}
