using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DataAccessLeyer.EF;
using System.Data.Objects;
using bl = BusinessAccessLeyer;
using BusinessAccessLeyer;


namespace DataAccessLeyer.DAL
{
   public class LoginDAL
    {
       inventoryforwebappEntities inv;
       bl.Class1 DGM;
    
       public LoginDAL()
       {
           inv = new inventoryforwebappEntities();
           DGM = new bl.Class1();
           //inv = new inventoryforwebappEntities();
       }

       public string DecodeFrom64(string encodedData)
       {
           System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
           System.Text.Decoder utf8Decode = encoder.GetDecoder();
           byte[] todecode_byte = Convert.FromBase64String(encodedData);
           int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
           char[] decoded_char = new char[charCount];
           utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
           string result = new String(decoded_char);
           return result;
       }

       public List<User_Name> LoginUserDal(string uname, string password)
       {

           return inv.User_Name.Where(em => em.User_Code == uname && em.Password == password).ToList();
       }

       public List<LoginProp> PermissionDAL(  string usertype , string branchcode , string usercode , string rolename )
       {
         //  string Qry = "select  * from Rollsetting_subtab  where branchcode='" + branchcode + "'  and rolsetingname = '" + rolename + "' ";

           string Qry = "select  * from Rollsetting_subtab  where  rolsetingname = '" + rolename + "' ";
           //string subqry = string.Empty;
           //if (usertype == "ADMIN")
           //{
           //    subqry = Qry;
           //}
           //else if (usertype == "PARTNER")
           //{
           //    subqry = Qry + " and branchcode = '" + branchcode + "'  ";
           //}
           //else if (usertype == "EXECUTIVE")
           //{
           //    subqry = Qry + " and branchcode = '" + branchcode + "'  and usercode = '" + usercode + "' ";
           //}

           DGM.LoginProp = inv.ExecuteStoreQuery<bl.LoginProp>(Qry).ToList();
           return DGM.LoginProp.ToList();    
       }

       public List<RolenameProp> SearchRoleNameDAL(string qry)
       {
           DGM.RolenameProp = inv.ExecuteStoreQuery<bl.RolenameProp>(qry).ToList();
           return DGM.RolenameProp.ToList();   
       }
    }
}
