using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;

using DataAccessLeyer.EF;
using System.Data.Objects;

namespace DataAccessLeyer.DAL
{
   public class NewSimRequersDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public NewSimRequersDAL()
        {         
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
           
        }
        public string Insert_Sim(string  date , string  counrty ,string noofsim, string priority , string exec_code , string branchcode  )
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.RequestSim_Insert("Insert", 1, date, counrty, noofsim, priority,branchcode, exec_code,"NOT OUT", obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                
                throw ex.InnerException ;
            }
            return Msg;
        }
        public int updatadal(string qry)
        {
           return inv.ExecuteStoreCommand(qry);
        }

        public List<SIMREQUEST_PROP> NewSimRequest_SearchDAL(string branchcode , string usertype)
        {
            string strqry = "";

            if (usertype == "ADMIN")
            {
                strqry = "select requestid, Date , Country ,SimNo ,Priority , Executive_Code from  newsimrequest where status_n='NOT OUT'";
            }
            else
                strqry = "select requestid, Date , Country ,SimNo ,Priority , Executive_Code from  newsimrequest where status_n='NOT OUT' and branchcode ='" + branchcode + "'";
            try
            {
                DGM.SIMREQUEST_PROP = inv.ExecuteStoreQuery<bl.SIMREQUEST_PROP>(strqry).ToList();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            return DGM.SIMREQUEST_PROP;
        }
    }
}
