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
  public  class SimReplacementDAL
    {
         inventoryforwebappEntities inv;
       bl.Class1 DGM;
       public SimReplacementDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

       public int InsertData(string f , string date , string currentsimno , string cafno ,string name ,string alotnewsim ,string loginuser )
       {
         return  inv.simreplacementtabproc_insert(f, 1, date, currentsimno, cafno, name, alotnewsim, loginuser);
       }
    }
}
