using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
   public class PurchaseEntryOthersDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;

        public PurchaseEntryOthersDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public string purchaseentryotherstableinsert(string f, long sno, string BillNo, string Date, string ItemType, string itemname,
                                     string loginusercode, string SimAlotexeccode, string IMEINO,string validity, string  country , string branchcode, string partnername)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.PurchaseEntryotherstabproc_Insert(f, sno, BillNo, Date, ItemType, itemname,
                    loginusercode, SimAlotexeccode, IMEINO,ItemType=="VOUCHER" ?validity : "",ItemType=="VOUCHER" ? country :"",  branchcode, partnername, "NOT IN USE", obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                Msg = "Please Ckeck Field ....";
               
            }
            return Msg;
        }

        public List<PurchaseEnteryItem> purchase_imeinoserach(string IMEINO)
        {
            string Query = "Select IMEINO from PurchaseEntryotherstab where IMEINO like  '" + IMEINO + "%' ";
            DGM.PurchaseEnteryItem = inv.ExecuteStoreQuery<bl.PurchaseEnteryItem>(Query).ToList();
            return DGM.PurchaseEnteryItem.ToList();
        }

        public List<PurchaseEnteryItem> purchaseitemwiseserach(string Query)
        {
            DGM.PurchaseEnteryItem = inv.ExecuteStoreQuery<bl.PurchaseEnteryItem>(Query).ToList();
            return DGM.PurchaseEnteryItem.ToList();
        }
        public List<PurchaseEnteryItem> purchase_itemnmeserach(string IMEINO)
        {
            string Query = "Select distinct(itemname) from PurchaseEntryotherstab where itemname like  '" + IMEINO + "%' ";
            DGM.PurchaseEnteryItem = inv.ExecuteStoreQuery<bl.PurchaseEnteryItem>(Query).ToList();
            return DGM.PurchaseEnteryItem.ToList();
        }
       /// <summary>
       /// search for challan out
       /// </summary>
       /// <param name="IMEINO"></param>
       /// <returns></returns>
        public List<PurchaseEnteryItem> purchase_imeinoserach_INCHALLAN(string IMEINO)
        {
            string Query = "Select distinct(itemname) from PurchaseEntryotherstab where itemname like  '" + IMEINO + "%' and itemtype <> 'VOUCHER' and status = 'NOT IN USE' ";
            DGM.PurchaseEnteryItem = inv.ExecuteStoreQuery<bl.PurchaseEnteryItem>(Query).ToList();
            return DGM.PurchaseEnteryItem.ToList();
        }     

        public List<PurchaseEnteryItem> purchase_vouchernoserach_Voucher(string IMEINO)
        {
            string Query = "Select itemname from PurchaseEntryotherstab where itemname like  '" + IMEINO + "%' and itemtype = 'VOUCHER' and status = 'NOT IN USE' ";
            DGM.PurchaseEnteryItem = inv.ExecuteStoreQuery<bl.PurchaseEnteryItem>(Query).ToList();
            return DGM.PurchaseEnteryItem.ToList();
        }

        public string autobillno_item(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.purchaseautobillnoItem_proc(branchcode, "PURCHASE ITEM", branchcode, obmg);
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
