using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;
using BusinessAccessLeyer;


namespace DataAccessLeyer.DAL
{
   public class SettingDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public SettingDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public string insert_data(string prifixtypr ,string formname , string prifixnumber , string  stratwith,string  branchcode )
        {
            string Msg = string.Empty;
            try
            {
                int t = inv.prefixsettings_proc_insert(1, prifixtypr, formname, prifixnumber, stratwith, branchcode, "ACTIVE", "", "", "");
                if (t > 0)
                {
                    Msg = "Record Insert Successfully..";
                }
            }
            catch (Exception ex)
            {
                
                Msg = "Please Insert Field..";
            }
            return Msg;
        }

        public List<rolesettingprop> Permission(string formname  , string rolename)
        {
            string Query = "select * from Rollsetting_subtab where rolsetingname = '" + rolename + "' and formname = '" + formname + "' ";

            DGM.rolesettingprop = inv.ExecuteStoreQuery<bl.rolesettingprop>(Query).ToList();
            return DGM.rolesettingprop.ToList();   
        }
    }
}
