using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;
using BusinessAccessLeyer;
using System.Data;
using System.Data.SqlClient;
using  System.Configuration;

namespace DataAccessLeyer.DAL
{
    public class ChallanDAL
    {
        //SqlConnection con;
        //SqlCommand cmd;
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        //DeviceModelGridWithChallan DGMWC;
        public ChallanDAL()
        {
          //  con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
            //DGMWC = new bl.DeviceModelGridWithChallan();
        }
        public string challanoutvalidate(string stchallanno, string stDate, string stCountry, string stItemType, string stSimNo,
            string stSimAlotexeccode, string stloginusercode, string branchcode, string givento)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obMg = new ObjectParameter("Msg", typeof(string));
                int t = inv.pChallanout_insert("Insert", stchallanno, stDate, stDate, stCountry, stItemType, stSimNo,
                    stSimAlotexeccode, stloginusercode, 0, branchcode,givento, obMg);
                Msg = Convert.ToString(obMg.Value);
            }
            catch (Exception ex)
            {
                Msg = "Record Not Saved";
            
            }
            return Msg;
        }

        public List<DeviceModelGrid> purchaseserachinchallan(string country , string type , string branchcode , string loginuser)
        {
            string SubQry = string.Empty;
            string QryDevice = string.Empty;
            QryDevice = "select itemtype, simno, country, PhoneNo, simcode, puk  from purchaseentry where country='" + country + "' and status ='NOT IN USE'";
            switch (type) 
            {
                case  "ADMIN" :
                    SubQry = QryDevice;
                    break;
                case "PARTNER":
                    SubQry = QryDevice + " and branchcode = '"+branchcode+"' " ;
                    break;
                case "EXECUTIVE":
                    SubQry = QryDevice + " and branchcode = '" + branchcode + "' and  loginusercode = '" + loginuser + "' ";
                    break;
            }


            DGM.DeviceModelGrid = inv.ExecuteStoreQuery<bl.DeviceModelGrid>(SubQry).ToList();
            return DGM.DeviceModelGrid.ToList();

        }

        public List<DeviceModelGrid> purchaseserachinchallan_withsimno(string simno, string type, string branchcode, string loginuser)
        {
            string SubQry = string.Empty;
            string QryDevice = string.Empty;
            QryDevice = "select itemtype, simno, country, PhoneNo, simcode, puk  from purchaseentry where simno='" + simno + "' and status ='NOT IN USE'";
            switch (type)
            {
                case "ADMIN":
                    SubQry = QryDevice;
                    break;
                case "PARTNER":
                    SubQry = QryDevice + " and branchcode = '" + branchcode + "' ";
                    break;
                case "EXECUTIVE":
                    SubQry = QryDevice + " and branchcode = '" + branchcode + "' and  loginusercode = '" + loginuser + "' ";
                    break;
            }


            DGM.DeviceModelGrid = inv.ExecuteStoreQuery<bl.DeviceModelGrid>(SubQry).ToList();
            return DGM.DeviceModelGrid.ToList();

        }

        public List<DeviceModelGridWithChallan> serachin_challanbill(string challanno, string type, string branchcode, string loginuser)
        {
            string SubQry = string.Empty;
            string QryDevice = string.Empty;
            QryDevice = "select c.challanno, p.itemtype, c.others, c.country,c.executivenameto, p.PhoneNo, p.simcode, p.puk  from purchaseentry p inner join challanout c on c.others = p.simno where c.challanno = '" + challanno.Trim() + "' and c.status <> 'BLOCK' ";
            switch (type)
            {
                case "ADMIN":
                    SubQry = QryDevice;
                    break;
                case "PARTNER":
                    SubQry = QryDevice + " and c.branchcode = '" + branchcode + "' ";
                    break;
                case "EXECUTIVE":
                    SubQry = QryDevice + " and c.branchcode = '" + branchcode + "' and c.executivename = '" + loginuser + "' ";
                    break;
            }
            // QryDevice = "select challanno, others from challanout where challanno='" + challanno + "'";
            
            DGM.DeviceModelGridWithChallan = inv.ExecuteStoreQuery<bl.DeviceModelGridWithChallan>(SubQry).ToList();
            return DGM.DeviceModelGridWithChallan.ToList();

        }

        public List<DeviceModelGridWithChallanAll> challanserach(string Query)
        {
            DGM.DeviceModelGridWithChallanAll = inv.ExecuteStoreQuery<bl.DeviceModelGridWithChallanAll>(Query).ToList();
            return DGM.DeviceModelGridWithChallanAll.ToList();

        }

        public List<challanout> challanserachothers(string empName)
        {
           return inv.challanouts.Where(em => em.others.Contains(empName)).ToList(); // .Select(em => em.others).ToList();
          

        }

        public List<AlotUser> GetAloatUser(string User_Code , string type , string branchcode , string loginuser )
        {
            List<AlotUser> st = null;
            switch(type)
            {
                case "ADMIN" :
                    {
                      st=  inv.User_Name.Select(c => new AlotUser
                        {
                            user_name = c.User_Code,
                            usercode = c.UserName
                        }).ToList();
                        break;
                    }
                case "PARTNER" :
                    {
                      st =  inv.User_Name.Where(em=>em.Branchcode==branchcode)  .Select(c => new AlotUser
                        {
                            user_name = c.User_Code,
                             usercode = c.UserName
                        }).ToList();
                        break;
                    }
                case "EXECUTIVE" :
                    {
                      st =  inv.User_Name.Where(em => em.Branchcode == branchcode && em.User_Code == loginuser).Select(c => new AlotUser
                        {
                            user_name = c.User_Code,
                             usercode = c.UserName
                        }).ToList();
                        break;
                    }
        }
            return st;

        }


        public List<AlotUser> SearchSim_No(string empName, string country, string type, string usercode, string branchcode)
        {
            List<AlotUser> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.PurchaseEntries
                   .Where(em =>em.status == "NOT IN USE" && em.Country == country && em.SimNo.Contains(empName)).Select(c => new AlotUser
                   {
                       simno = c.SimNo
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.PurchaseEntries
                  .Where(em => em.status == "NOT IN USE" && em.Country == country && em.branchcode == branchcode && em.SimNo.StartsWith(empName)).Select(c => new AlotUser
                  {
                      simno = c.SimNo
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.PurchaseEntries
                  .Where(em => em.status == "NOT IN USE" && em.Country == country && em.branchcode == branchcode && em.SimNo.Contains(empName)).Select(c => new AlotUser
                  {
                      simno = c.SimNo
                  }).ToList();
            }
            return custd;
        }

        public List<AlotUser> SearchChallan_No(string empName, string type, string usercode, string branchcode)
        {
            List<AlotUser> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.challanouts
                   .Where(em =>   em.challanno.Contains(empName)).Select(c => new AlotUser
                   {
                       challanno = c.challanno
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.challanouts
                  .Where(em => em.branchcode == branchcode && em.challanno.StartsWith(empName)).Select(c => new AlotUser
                  {
                      challanno = c.challanno
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.challanouts
                  .Where(em => em.branchcode == branchcode && em.challanno.Contains(empName)).Select(c => new AlotUser
                  {
                      challanno = c.challanno
                  }).ToList();
            }
            return custd;
        }


        public List<AlotUser> SearchSimNo_Challanout(string empName, string type, string usercode, string branchcode)
        {
            List<AlotUser> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.challanouts
                   .Where(em => em.others.Contains(empName)).Select(c => new AlotUser
                   {
                       simno = c.others
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.challanouts
                  .Where(em => em.branchcode == branchcode && em.others.StartsWith(empName)).Select(c => new AlotUser
                  {
                      simno = c.others
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.challanouts
                  .Where(em => em.branchcode == branchcode && em.others.Contains(empName)).Select(c => new AlotUser
                  {
                      simno = c.others
                  }).ToList();
            }
            return custd;
        }

        public List<AlotUser> SearchExecutiveCode(string empName, string type, string usercode, string branchcode)
        {
            List<AlotUser> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.challanouts
                   .Where(em => em.challanno.Contains(empName)).Select(c => new AlotUser
                   {
                       challanno = c.challanno
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.challanouts
                  .Where(em => em.branchcode == branchcode && em.challanno.StartsWith(empName)).Select(c => new AlotUser
                  {
                      challanno = c.challanno
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.challanouts
                  .Where(em => em.branchcode == branchcode && em.challanno.Contains(empName)).Select(c => new AlotUser
                  {
                      challanno = c.challanno
                  }).ToList();
            }
            return custd;
        }
        
        
        public string autobillno(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.Challanautobillno_proc(branchcode, "CHALLAN", usercode, obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return Msg;
        }

        public int count(string country, string branchcode, string usertype)
        {
            int cut = 0;
            if (usertype == "ADMIN")
            {
                cut = inv.challanouts.Where(em => em.country == country && em.status == "NOT IN USE").Count();
            }
            else if (usertype == "PARTNER")
            {
                cut = inv.challanouts.Where(em => em.country == country && em.branchcode == branchcode && em.status == "NOT IN USE").Count();
            }
            return cut;
        }

        public int count_client(string country, string branchcode, string usertype)
        {
            int cut = 0;
            if (usertype == "ADMIN")
            {
                cut = inv.ClientMasters.Where(em => em.Country == country && em.status == "ACTIVE").Count();
            }
            else if (usertype == "PARTNER")
            {
                cut = inv.ClientMasters.Where(em => em.Country == country && em.branchcode == branchcode && em.status == "ACTIVE").Count();
            }
            return cut;
        }
       
    }
}
