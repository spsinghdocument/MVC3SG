using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;
using BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
  public  class InventoryDAL
    {
        inventoryforwebappEntities inv;     
        bl.Class1 DGM;

        public InventoryDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }
        public List<DeviceModelGrid1> searchcountry(string usertype, string QryDevice, string stloninname, string branchcode)
        {
            string subqry = "";
            //List<PurchaseEntry> lstpurent = null;
            if (usertype == "PARTNER")
            {
                subqry = QryDevice + " and branchcode ='" + branchcode + "' ";               
            }
            else if (usertype == "EXECUTIVE")
            {
                subqry = QryDevice + " and branchcode ='" + branchcode + "'  and loginusercode = '" + stloninname + "' ";               
            }
            else if (usertype == "ADMIN")
            {
                subqry = QryDevice;
            }

            DGM.DeviceModelGrid1 = inv.ExecuteStoreQuery<bl.DeviceModelGrid1>(subqry).ToList();
            return DGM.DeviceModelGrid1.ToList();
          


        }
    }
}
