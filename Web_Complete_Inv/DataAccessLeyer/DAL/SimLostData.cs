using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;

namespace DataAccessLeyer.DAL
{
   public class SimLostData
    {
       inventoryforwebappEntities inv;
       public SimLostData()
       {
           inv = new inventoryforwebappEntities();
       }

       public string SimLostValidate(string date , decimal simno , string country , string resion , string branchcode , string loginuser )
       {
           int t = 0;
           string Msg = "";
           try
           {
               t = inv.SimLost_Insert("Insert", date, simno, country, resion, "YES", branchcode, "", loginuser);
               if (t > 0)
               {
                   Msg = "Record Inserted Successfully";
               }
               else
               {
                   Msg = "Sim Allready exist";
               }

           }
           catch (Exception ex)
           {

               Msg = "Sim Allready exist";
           }
           return Msg;
       }
    }
}
