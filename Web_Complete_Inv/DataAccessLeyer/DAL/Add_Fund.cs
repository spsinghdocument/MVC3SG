using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using System.Data.Objects;

namespace DataAccessLeyer.DAL
{
  public  class Add_Fund
    {
      inventoryforwebappEntities inv;
      public Add_Fund()
      {
          inv = new inventoryforwebappEntities();
      }

      public int updatefund(string data , string usercode)
      {

           ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));
           return inv.UserNameInsert("UPDATEDATAVAL", "", "", "", usercode, "", "", "", "", "", "", "", "", "", data, opmg);
         
      }

      public List<updatedataval_proc_Result> updateresultDAL(string f , string usercode , string branchcode , string usertype)
      {
          return inv.updatedataval_proc(f, usercode, branchcode, usertype).ToList();
      }
    }
}
