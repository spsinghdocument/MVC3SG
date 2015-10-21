using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
  public  class PlanDAL
    {
        inventoryforwebappEntities inv;
         bl.Class1 DGM;
         public PlanDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

         public string Plan_InsertDAL(string f , string plancode, string planname, string plantype, string country, string planvalue , string talktimedata , string branchcode , string loginuser)                      
         {
             string Msg = String.Empty;

             ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
             try
             {
                 inv.PLAN_PROC_INSERT(f, 1, plancode, planname, plantype, country, planvalue, talktimedata, branchcode, loginuser, obmg);
             }
             catch (Exception ex)
             {
                 
                 throw ex.InnerException;
             }
             Msg = Convert.ToString(obmg.Value);
             return Msg;
         }
    }
}
