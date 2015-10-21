using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using System.Data.Objects;
using BusinessAccessLeyer;
using bl =  BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
  public  class CompanyProfileDAL
    {
      inventoryforwebappEntities inv;
      bl.Class1 DGM;
      public CompanyProfileDAL()
      {
          inv = new inventoryforwebappEntities();
          DGM = new bl.Class1();
      }

      public string insertcompany(string f, long cmpid , string CompanyName, string Address, string ManagingDirector, string ContactNo, string CustSupportNo,
        string PunchLine, string EmailID, string WebSite, string bankdetails, string panno, string servicetax, string loginusercode)
      {
          string Msg = string.Empty;
          ObjectParameter opmg = new ObjectParameter("Msg" , typeof(string));
        int t =  inv.InsertCompanyProfile_Insert(f, cmpid , CompanyName, "", Address, ManagingDirector, ContactNo, CustSupportNo, PunchLine,
              EmailID, WebSite, bankdetails, panno, servicetax, loginusercode,opmg);
          Msg = Convert.ToString(opmg.Value);
          return Msg;
      }


      public List<CompanyProfile_P> CompanySearch(string QryDevice)
      {
          DGM.CompanyProfile_P = inv.ExecuteStoreQuery<bl.CompanyProfile_P>(QryDevice).ToList();
          return DGM.CompanyProfile_P.ToList();
      }

    }
}
