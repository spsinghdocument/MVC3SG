using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using DataAccessLeyer.EF;
using System.Data.Objects;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
   public  class SimWiseSearchDal
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public SimWiseSearchDal()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public List<SimWiseSearch_Data_Result> simwisesearch_proc(string Simno, string UserType, string Branchcode, string Usercode)
        {
            return inv.SimWiseSearch_Data(Simno, UserType, Branchcode, Usercode).ToList();
        }

        public List<SimWiseSearchProc> PurchaseSearch_PUKDATA(string Query)
        {
            DGM.SimWiseSearchProc = inv.ExecuteStoreQuery<bl.SimWiseSearchProc>(Query).ToList();
            return DGM.SimWiseSearchProc.ToList();
        }


        public List<PurchaseSimWiseSearch> SearchPlanDAL(string option , string empName, string type, string usercode, string branchcode)
        {
            List<PurchaseSimWiseSearch> custd = null;

            if (option == "SIM NO SEARCH")
            {
                if (type == "ADMIN")
                {
                    custd = inv.PurchaseEntries
                       .Where(em => em.SimNo.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                       {
                           simno = c.SimNo
                       }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.PurchaseEntries
                      .Where(em => em.branchcode == branchcode && em.SimNo.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                      {
                          simno = c.SimNo
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.PurchaseEntries
                      .Where(em => em.branchcode == branchcode && em.loginusercode == usercode && em.SimNo.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                      {
                          simno = c.SimNo
                      }).ToList();
                }
            }
            else if (option == "PUK SEARCH")
            {
                if (type == "ADMIN")
                {
                    custd = inv.PurchaseEntries
                       .Where(em => em.PUK.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                       {
                           puk = c.PUK
                       }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.PurchaseEntries
                      .Where(em => em.branchcode == branchcode && em.PUK.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                      {
                          puk = c.PUK
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.PurchaseEntries
                      .Where(em => em.branchcode == branchcode && em.loginusercode == usercode && em.PUK.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                      {
                          puk = c.PUK
                      }).ToList();
                }
            }
            else if (option == "MOBILE NO. SEARCH")
            {
                if (type == "ADMIN")
                {
                    custd = inv.PurchaseEntries
                       .Where(em => em.PhoneNo.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                       {
                           mobileno = c.PhoneNo
                       }).ToList();
                }
                else if (type == "PARTNER")
                {
                    custd = inv.PurchaseEntries
                      .Where(em => em.branchcode == branchcode && em.PhoneNo.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                      {
                          mobileno = c.PhoneNo
                      }).ToList();
                }
                else if (type == "EXECUTIVE")
                {
                    custd = inv.PurchaseEntries
                      .Where(em => em.branchcode == branchcode && em.loginusercode == usercode && em.PhoneNo.StartsWith(empName)).Select(c => new PurchaseSimWiseSearch
                      {
                          mobileno = c.PhoneNo
                      }).ToList();
                }
            }
            return custd;
        }
    }
}
