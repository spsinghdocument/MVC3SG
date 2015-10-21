using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using bl = BusinessAccessLeyer;

using System.Data.Objects;
using BusinessAccessLeyer;


namespace DataAccessLeyer.DAL
{
   public class RolesettingDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public RolesettingDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public List<rolesettingprop> searchdata_Role(string Query)
        {
            DGM.rolesettingprop = inv.ExecuteStoreQuery<bl.rolesettingprop>(Query).ToList();
            return DGM.rolesettingprop.ToList();
        }

        public string insertsolename(string f, long id, string rolesettingname, string rolesettingcode, string usercode, string branchcode, string cmpid)
        {
            string Msg = string.Empty;
            try
            {
                int t = inv.rolenametable_proc_insert(f, id, rolesettingname, rolesettingcode, usercode, branchcode, cmpid);
                if (t > 0)
                {
                    Msg = "Record Inserted Successfully..";
                }
            }
            catch (Exception)
            {

                Msg = "Please Check Field..";
            }
            return Msg;
        }

        public List<rolesettings_proc_insert_Result> AllPermissionInsert(string f, long id, string formname, bool insert, bool view, bool edit, bool delete, bool all, string rolsetingname, string usercode, string branchcode) 
        {
            List<rolesettings_proc_insert_Result> roleset = null;
            try
            {
              roleset=   inv.rolesettings_proc_insert(f, id, formname, insert, view, edit, delete, all, rolsetingname, usercode, branchcode).ToList();
           
            }
            catch (Exception)
            {

                roleset = null;
            }
            return roleset;
        }


        public List<rolenameproc> GetRoleName(string branchcode)
        {
            List<rolenameproc> st =
                inv.RoleSettings_Tab.Where(s => s.branchcode == branchcode).Select(c => new rolenameproc
                {
                    rolename = c.rolesettingname
                }).ToList();
            return st;

        }
    }
}
