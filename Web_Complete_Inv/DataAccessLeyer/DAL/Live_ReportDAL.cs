using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;

namespace DataAccessLeyer.DAL
{
    public class Live_ReportDAL
    {
        inventoryforwebappEntities inv;
        public Live_ReportDAL()
        {
            inv = new inventoryforwebappEntities();
        }

        public List<live_stockreport_proc_Result> livefun(string country)
        {
            
            return inv.live_stockreport_proc(country).ToList();
        }
    }
}
