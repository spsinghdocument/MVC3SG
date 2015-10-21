using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using DataAccessLeyer.EF;
using  BusinessAccessLeyer;
using bl = BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
  public  class ChallanOutOthersDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public ChallanOutOthersDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public List<CountryModelstringItem> purchaseentryotherstablesearchforchallanout(string type, string loginname, string itemname, string loginusercode, string branchcode)
        {
            //List<DeviceModelGrid> strret = null;

            string QryDevice = string.Empty;
          
          
            if (type == "PARTNER")
            {
                QryDevice = "select  itemname , IMEINO from PurchaseEntryotherstab where branchcode = '" + branchcode + "' and itemname = '" + itemname + "'  ";
             
            }
            else if (type == "EXECUTIVE")
            {

                QryDevice = "select  itemname , IMEINO from PurchaseEntryotherstab where branchcode = '" + branchcode + "' and itemname =  '" + itemname + "' and loginusercode = '" + loginusercode + "'   ";
             

            }
            else if (type == "ADMIN")
            {
                QryDevice = "select  itemname , IMEINO  from PurchaseEntryotherstab where itemname = '" + itemname + "' ";
              
            }

            DGM.CountryModelstringItem = inv.ExecuteStoreQuery<bl.CountryModelstringItem>(QryDevice + " and status = 'NOT IN USE'").ToList();
            return DGM.CountryModelstringItem.ToList();
      
        }


        public string challanoutotherstableentry(string challanno, string cdate, string sno, string itemtype,
                         string itemname, string Imeino, string executivenameto, string executivename, string branchcode, string partnername)
        {
            string Msg = String.Empty;

            ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
            inv.challanoutothersproc_Insert("Insert", challanno, cdate, sno, itemtype, itemname, Imeino,
                                             executivenameto, executivename, 0, branchcode, partnername, "NOT ACTIVE", obmg);
            Msg = Convert.ToString(obmg.Value);
            return Msg;
        }


        public List<ChallanOutOthersProp> GetItemName(string itemname, string type, string usertype, string branchcode, string loginuser)
        {       

                 List < ChallanOutOthersProp > st = null;

            if (usertype == "ADMIN")
            {
              st =  inv.PurchaseEntryotherstabs.Where(s => s.ItemType == type && s.itemname.Contains(itemname)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    itemname = c.itemname
                }).ToList();
            }
            else if (usertype == "PARTNER")
            {
              st =   inv.PurchaseEntryotherstabs.Where(s => s.branchcode == branchcode && s.ItemType == type && s.itemname.Contains(itemname)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    itemname = c.itemname
                }).ToList();
            }
            else if (usertype == "EXECUTIVE")
            {
              st =   inv.PurchaseEntryotherstabs.Where(s => s.branchcode == branchcode && s.loginusercode== loginuser && s.ItemType == type && s.itemname.Contains(itemname)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    itemname = c.itemname
                }).ToList();
            }
               
            return st;

        }

        public List<ChallanOutOthersProp> GetItemName_Search(string itemname, string usertype , string branchcode , string loginuser )
        {
            List<ChallanOutOthersProp> st = null;

            if (usertype == "ADMIN")
            {
                st = inv.challanoutothers.Where(s => s.itemname.Contains(itemname)).Distinct().Select(c => new ChallanOutOthersProp
                  {
                      itemname = c.itemname
                  }).ToList();
            }
          else  if (usertype == "PARTNER")
            {
                st = inv.challanoutothers.Where(s => s.branchcode == branchcode && s.itemname.Contains(itemname)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    itemname = c.itemname
                }).ToList();
            }
          else  if (usertype == "EXECUTIVE")
            {
                st = inv.challanoutothers.Where(s => s.branchcode == branchcode && s.executivenameto == loginuser && s.itemname.Contains(itemname)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    itemname = c.itemname
                }).ToList();
            }
            return st;

        }

        public List<ChallanOutOthersProp> GetImeiNo_Search(string imeino, string usertype, string branchcode, string loginuser)
        {
            List<ChallanOutOthersProp> st = null;

            if (usertype == "ADMIN")
            {
                st = inv.challanoutothers.Where(s => s.Imeino.Contains(imeino)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    imeino = c.Imeino
                }).ToList();
            }
            else if (usertype == "PARTNER")
            {
                st = inv.challanoutothers.Where(s => s.branchcode == branchcode && s.Imeino.Contains(imeino)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    imeino = c.Imeino
                }).ToList();
            }
            else if (usertype == "EXECUTIVE")
            {
                st = inv.challanoutothers.Where(s => s.branchcode == branchcode && s.executivenameto == loginuser && s.Imeino.Contains(imeino)).Distinct().Select(c => new ChallanOutOthersProp
                {
                    imeino = c.Imeino
                }).ToList();
            }
            return st;

        }

        public List<CountryModelstringItem> searchdatadal(string strqry)
        {
            DGM.CountryModelstringItem = inv.ExecuteStoreQuery<bl.CountryModelstringItem>(strqry).ToList();
            return DGM.CountryModelstringItem.ToList();
        }

        public string autobillno_item(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.challanoutitemcounter_proc(branchcode, "CHALLAN ITEM", branchcode, obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return Msg;
        }
      
    }
}
