using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
  public  class TotalFundDAL
    {
      inventoryforwebappEntities inv;
      bl.Class1 DGM;

      public TotalFundDAL()
      {
          inv  = new inventoryforwebappEntities();
          DGM = new bl.Class1();
      }
        public List<plantableproc> filldatavalue(string plancode)
        {
            DGM.plantableproc = inv.ExecuteStoreQuery<bl.plantableproc>("Select TALKTIMEDATA as TALKTIMEDATA from PlanTable where PLAN_CODE = '" + plancode + "'").ToList();
            return DGM.plantableproc;
        }
    }
}
