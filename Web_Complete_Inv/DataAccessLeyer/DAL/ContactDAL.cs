using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
   public class ContactDAL
    {
       inventoryforwebappEntities inv;
       bl.Class1 DGM;
       public ContactDAL()
       {
           inv = new inventoryforwebappEntities();
           DGM = new bl.Class1();
       }
       public string insertdalcust(string   NAME,string  ADDRESS,string  MOBILENO,string  EMAILID,string  PRODUCT, string   COMMENT ) 
       {
           string Msg = "";
         int t =  inv.contacttableproc("Insert", 1,NAME, ADDRESS, MOBILENO, EMAILID,PRODUCT, COMMENT,"NOT ACTIVE");
         if (t > 0)
         {
             Msg = "Your Query Sent Successfully..";
         }
           else
             Msg = " Query Not Sent..";
         return Msg;
       }


       public List<CONTACTUSDAL> Contactusdal()
       {
           string Qry = "Select NAME,MOBILENO,EMAILID,PRODUCT,COMMENT FROM contacttable where showstatus = '"+"NOT ACTIVE"+"' ";
           DGM.CONTACTUSDAL = inv.ExecuteStoreQuery<bl.CONTACTUSDAL>(Qry).ToList();
           return DGM.CONTACTUSDAL;
       }

       public List<CONTACTUSDAL> Contactussearchdal(string mobval)
       {
           string Qry = "Select NAME,MOBILENO,EMAILID,PRODUCT,COMMENT FROM contacttable where MOBILENO = '" + mobval + "' ";
           DGM.CONTACTUSDAL = inv.ExecuteStoreQuery<bl.CONTACTUSDAL>(Qry).ToList();
           return DGM.CONTACTUSDAL;
       }

      
       public string Contactusdeletedal(string mobval)
       {
           string Msg = "";
           string Qry = "UPDATE contacttable SET Showstatus = 'ACTIVE'  where MOBILENO = '" + mobval + "' ";
           int T  = inv.ExecuteStoreCommand(Qry);
           if (T > 0)
           {
               Msg = "RECORD DELETED SUCCESSFULLY";
           }
          
           return Msg;
       }
       
    }
}
