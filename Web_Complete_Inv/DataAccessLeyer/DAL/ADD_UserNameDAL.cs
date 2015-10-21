using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;

namespace DataAccessLeyer.DAL
{
   public class ADD_UserNameDAL
    {
        inventoryforwebappEntities inv;
        public ADD_UserNameDAL()
        {
            inv = new inventoryforwebappEntities();
        }

        public string createuser(string f, string date, string Branchcode, string partnername, string User_Code, string UserType,
       string UserName, string password, string newpassword, string address, string mobileno, string emailid, string rolesetting, string valueassign)
        {
            string Msg = string.Empty;
            ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));
            inv.UserNameInsert(f, date, Branchcode, partnername, User_Code, UserType, UserName, password, newpassword,
                  address, mobileno, emailid, rolesetting, valueassign,"", opmg);
            Msg = Convert.ToString(opmg.Value);
            return Msg;
            // return 1;

        }


        public string autouserid(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.Executiveautobillno_proc(branchcode, "USER", branchcode, obmg);
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
