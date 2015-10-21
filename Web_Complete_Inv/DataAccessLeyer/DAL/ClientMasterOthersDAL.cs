using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
   public class ClientMasterOthersDAL
    {
        inventoryforwebappEntities inv;
        public ClientMasterOthersDAL()
        {
            inv = new inventoryforwebappEntities();
        }

        public string insertclientmasterothervalidate(string f, string cafno, string customeracno, Int64 fkchallanout, string User_Name, string Father_Name,
            string Category, string Payment_Mode, Decimal Amount, string Bank, string Cheque_No, string itemname,
            string imeino, string Plan,string Executive_Name, string Executive_Code, string branchcode , string status,string itemtype )
        {
            string Msg = string.Empty;
            ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));

            inv.ClientMasterothersproc(f, cafno, customeracno, 0, User_Name, Father_Name,
                Category, Payment_Mode, Amount, Bank, Cheque_No, itemname, imeino, Plan, Executive_Name, Executive_Code, branchcode, status,itemtype, opmg);
            Msg = Convert.ToString(opmg.Value);
            return Msg;
        }


        public List<SearchItemType_Prop> searchautocomitemnamenameDAL(string empName, string itemtype, string type, string usercode, string branchcode)
        {
            List<SearchItemType_Prop> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.challanoutothers
                   .Where(em => em.itemtype  == itemtype && em.status == "NOT IN USE" && em.itemname.Contains(empName)).Select(c => new SearchItemType_Prop
                   {
                       itemname = c.itemname
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.challanoutothers
                  .Where(em => em.itemtype == itemtype && em.status == "NOT IN USE" && em.branchcode == branchcode && em.itemname.StartsWith(empName)).Select(c => new SearchItemType_Prop
                  {
                      itemname = c.itemname
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.challanoutothers
                  .Where(em => em.itemtype == itemtype && em.status == "NOT IN USE" && em.branchcode == branchcode && em.executivenameto == usercode && em.itemname.Contains(empName)).Select(c => new SearchItemType_Prop
                  {
                      itemname = c.itemname
                  }).ToList();
            }
            return custd;
        }



        public List<SearchItemType_Prop> FillImeiNo(string empName, string itemtype, string type, string usercode, string branchcode)
        {
            List<SearchItemType_Prop> custd = null;
            if (type == "ADMIN")
            {
                custd = inv.challanoutothers
                   .Where(em => em.itemtype == itemtype && em.itemname == empName && em.status == "NOT IN USE" ).Select(c => new SearchItemType_Prop
                   {
                       imeino = c.Imeino
                   }).ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.challanoutothers
                  .Where(em => em.itemtype == itemtype && em.status == "NOT IN USE" && em.branchcode == branchcode && em.itemname==empName).Select(c => new SearchItemType_Prop
                  {
                      imeino = c.Imeino
                  }).ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.challanoutothers
                  .Where(em => em.itemtype == itemtype && em.status == "NOT IN USE" && em.branchcode == branchcode && em.executivenameto == usercode && em.itemname == empName).Select(c => new SearchItemType_Prop
                  {
                      imeino = c.Imeino
                  }).ToList();
            }
            return custd;
        }
    }
}
