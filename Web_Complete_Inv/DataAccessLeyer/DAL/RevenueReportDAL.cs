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
   public class RevenueReportDAL
    {
       inventoryforwebappEntities inv;
       public RevenueReportDAL()
       {
           inv = new inventoryforwebappEntities();
       }

       public void Revenue_Saerch(string qry)
       {
           inv.ExecuteStoreQuery<bl.DeviceModelGrid>(qry).ToList();
       }
    }
}
