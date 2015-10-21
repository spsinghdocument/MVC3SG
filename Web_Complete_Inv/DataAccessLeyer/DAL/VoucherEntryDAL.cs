using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessLeyer.DAL
{
    public class VoucherEntryDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        public inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public VoucherEntryDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);
        }

        public string VoucherSaveDataEdit(string f, String typ, String amount, String Chequeno2, String BANK2, String DESCRIPTION2, String Balance, int voucherid)
        {
            //SqlConnection con = new SqlConnection("data source=103.21.58.192;initial catalog=inventory_MVC; user id=sgmayank;password=F@$tf0warD;");
            con.Open();
            SqlCommand cmd = new SqlCommand("update vouchertable set type='" + typ + "',Amount='" + amount + "',Chequeno= '" + Chequeno2 + "',Bank='" + BANK2 + "',Description='" + DESCRIPTION2 + "',netbalance='" + Balance + "' where voucher_id = " + voucherid + "", con);

            cmd.ExecuteNonQuery();
            con.Close();
            return "save";

            //return inv.vouchertableproc_Insert(f, typ, amount, Chequeno2, BANK2, DESCRIPTION2, Balance, voucherid);
        }
        public int VoucherSaveData(string f, String Voucherno, String Date, int Sno, String cafno, String Acno, String Name, String type, String paymentmode,
         decimal Amount, String Chequeno, String Bank, String Description, decimal netbalance, String executivecode, String branchcode)
        {


            return inv.vouchertableproc_Insert(f, 1, Voucherno, Date, Sno, cafno, Acno, Name, type, paymentmode,
             Amount, Chequeno, Bank, Description, netbalance, executivecode, branchcode);
        }


        public string voucher_autobillno(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.Voucherautobillno_proc(branchcode, "CHALLAN", usercode, obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return Msg;
        }

              public List<vouchertableproc_SaveAndSearch_Result> VouchersearchData(String Voucherno, String Date, int Sno, String cafno, String executivecode, String branchcode)
        {
            ObjectResult<vouchertableproc_SaveAndSearch_Result> ob = null;
            try
            {
                ob = inv.vouchertableproc_SaveAndSearch("Insert", 1, Voucherno, Date, 1, cafno, "", "", "", "", 0, "", "", "", 0, executivecode, branchcode);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            return ob.ToList();

        }

        public List<ClientMaster_Prop> search_cafno(string cafno)
        {
            string strqry = "select User_Name ,customeracno ,Payment_Mode ,Country ,Amount ,Cheque_No ,Bank from clientmaster where cafno = '" + cafno + "' ";
            DGM.ClientMaster_Prop = inv.ExecuteStoreQuery<bl.ClientMaster_Prop>(strqry).ToList();
            return DGM.ClientMaster_Prop.ToList();
        }

        public string delteDAL(int voucherid)
        {
            // newvoucher
            inv.newvoucher("delete", voucherid);
            return "DELETE";
        }

  
        /// ////////////////////////////////////////////////////////////////////////////////////////// All voucher ////////////////////////////////////////////
        public string AllVoucherdelteDAL(string ALvoucher_Id)
        {
            // newvoucher
            inv.allvoDelete("Aldeletedelete", ALvoucher_Id);
           
            
            // con.Open();
            //SqlCommand cmd = new SqlCommand("delete from vouchertable where Voucherno= = " + ALvoucher_Id + "", con);

            //cmd.ExecuteNonQuery();
            //con.Close();
         
            return "DELETE";
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////End ////////////////////////////////
      

        public List<Voucher_PropNew> searchwith_cafno(string cafno, string branchcode, string type)
        {
            string strqry = "";
            switch (type)
            { 
                case "ADMIN" :
                    strqry = "select * from vouchertable where cafno = '" + cafno + "' ";
                    break;
                case "PARTNER" :
                    strqry = "select * from vouchertable where cafno = '" + cafno + "' and branchcode = '"+branchcode+"' ";
                    break;
                case "EXECUTIVE" :
                    strqry = "select * from vouchertable where cafno = '" + cafno + "' and branchcode = '" + branchcode + "' ";
                    break;
           }
            try {
                DGM.Voucher_PropNew = inv.ExecuteStoreQuery<bl.Voucher_PropNew>(strqry).ToList();
            }
            catch (Exception e)
            { }
         
            return DGM.Voucher_PropNew.ToList();
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public List<Voucher_PropNew> searchwith_voucherno(string voucherno, string branchcode, string type)
        {
            string strqry = "";
            switch (type)
            {
                case "ADMIN":
                    strqry = "select * from vouchertable where voucherno = '" + voucherno + "' ";
                    break;
                case "PARTNER":
                    strqry = "select * from vouchertable where voucherno = '" + voucherno + "' and branchcode = '" + branchcode + "' ";
                    break;
                case "EXECUTIVE":
                    strqry = "select * from vouchertable where voucherno = '" + voucherno + "' and branchcode = '" + branchcode + "' ";
                    break;
            }
            try
            {
                DGM.Voucher_PropNew = inv.ExecuteStoreQuery<bl.Voucher_PropNew>(strqry).ToList();
            }
            catch (Exception e)
            { }

            return DGM.Voucher_PropNew.ToList();
        }

   //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
}
