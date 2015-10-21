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
   public class PurchaseEntryDal
    {
       inventoryforwebappEntities inv;
       bl.Class1 DGM;
       
      
       public PurchaseEntryDal()
       {
           inv = new inventoryforwebappEntities();
           DGM = new bl.Class1();
       }

       public string purchaseValidate(string f, long sno, string BillNo, string Date, string SimNo, string Country, string PUK, string Validity,
           string APN,string loginusercode,string 	SimAlotexeccode,string PhoneNo,string simcode ,string branchcode ,string  partnername ,  string username , string password)
       {
           string Msg = string.Empty;
           try
           {
               if (Convert.ToString(branchcode) == "")
               {
                   return "Please LogOut";
               }
               ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));
              int t =  inv.InsertPurchaseEntry_Insert(f, sno, BillNo, Date, Date, "SIM", SimNo, Country, PUK, Validity, loginusercode,
                                               SimAlotexeccode, APN, PhoneNo, simcode, "NOT IN USE", branchcode, partnername,username,password, opmg);
               Msg = Convert.ToString(opmg.Value);
           }
           catch (Exception)
           {
               Msg = "Please Check All Field .....";
               
           }
           return Msg;
       }

       public List<DeviceModelGrid> purchaseserach( string Query)
       {
           try
           {
               DGM.DeviceModelGrid = inv.ExecuteStoreQuery<bl.DeviceModelGrid>(Query).ToList();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return DGM.DeviceModelGrid.ToList();
       }

       public List<SimModel> GetSim(string Countryname)
       {
           List<SimModel> st =
               inv.PurchaseEntries.Where(s => s.Country == Countryname).Select(c => new SimModel
               {
                  Simno = c.SimNo
               }).ToList();
           return st;
         
       }


       public List<PurchaseEntry> searchcountry(string usertype, string country, string stloninname, string branchcode)
       {
           List<PurchaseEntry> lstpurent = null;
           if (usertype == "PARTNER")
           {
               lstpurent = inv.PurchaseEntries.Where(em => em.branchcode == branchcode && em.Country == country && em.status == "NOT IN USE" && em.status != "BLOCK").ToList();
           }
           else if (usertype == "EXECUTIVE")
           {
               lstpurent = inv.PurchaseEntries.Where(em => em.branchcode == branchcode && em.loginusercode == stloninname && em.Country == country && em.status == "NOT IN USE" && em.status != "BLOCK").ToList();
           }
           else if (usertype == "ADMIN")
           {
               lstpurent = inv.PurchaseEntries.Where(em => em.Country == country && em.status == "NOT IN USE" && em.status != "BLOCK").ToList();
           }

           return lstpurent;
       }

       public List<DeviceModelGrid> purchase_simserach(string simno , string utype , string  branchcode , string  loginuser)
       {
           string subqry = string.Empty;
           string Query = "Select SimNo , country from purchaseentry where simno like  '" + simno + "%' and  status <> 'BLOCK' ";
           switch (utype)
           {
               case "ADMIN" :
                   subqry = Query;
                   break;
               case "PARTNER":
                   subqry = Query + " and branchcode = '" + branchcode + "' and  status <> 'BLOCK' ";
                   break;
               case "EXECUTIVE":
                   subqry = Query + " and branchcode = '" + branchcode + "'   and  loginusercode = '" + loginuser + "' and  status <> 'BLOCK' ";
                   break;

           }

           DGM.DeviceModelGrid = inv.ExecuteStoreQuery<bl.DeviceModelGrid>(subqry).ToList();
           return DGM.DeviceModelGrid.ToList();
       }

       public List<DeviceModelGrid> purchase_Countryserach(string simno)
       {
           string Query = "Select distinct(country) from purchaseentry where country like  '" + simno + "%' and  status <> 'BLOCK' ";
           DGM.DeviceModelGrid = inv.ExecuteStoreQuery<bl.DeviceModelGrid>(Query).ToList();
           return DGM.DeviceModelGrid.ToList();
       }


       public List<DeviceModelGrid> purchase_billserach(string simno, string utype, string branchcode, string loginuser)
       {
           string subqry = string.Empty;
           string Query = "Select distinct(billno) from purchaseentry where billno like  '" + simno + "%' and  status <> 'BLOCK' ";

           switch (utype)
           {
               case "ADMIN":
                   subqry = Query;
                   break;
               case "PARTNER":
                   subqry = Query + " and branchcode = '" + branchcode + "' and  status <> 'BLOCK' ";
                   break;
               case "EXECUTIVE":
                   subqry = Query + " and branchcode = '" + branchcode + "'   and  loginusercode = '" + loginuser + "' and  status <> 'BLOCK' ";
                   break;

           }

           DGM.DeviceModelGrid = inv.ExecuteStoreQuery<bl.DeviceModelGrid>(subqry).ToList();
           return DGM.DeviceModelGrid.ToList();
       }

       public string autobillno(string branchcode , string usercode )
       {
           string Msg = string.Empty;
           try
           {
               ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
               inv.purchaseautobillno_proc(branchcode, "PURCHASE", branchcode, obmg);
               Msg = Convert.ToString(obmg.Value);
           }
           catch (Exception ex)
           {
               throw ex.InnerException;
           }
           return Msg;
       }

       public int count(string country ,string branchcode , string usertype)
       {
           int cut = 0;
           if (usertype == "ADMIN")
           {
              cut =  inv.PurchaseEntries.Where(em => em.Country == country && em.status == "NOT IN USE" ).Count();
           }
           else if (usertype == "PARTNER")
           {
               cut = inv.PurchaseEntries.Where(em => em.Country == country && em.branchcode == branchcode && em.status == "NOT IN USE").Count();
           }
           return cut;               
       }



    }
}
