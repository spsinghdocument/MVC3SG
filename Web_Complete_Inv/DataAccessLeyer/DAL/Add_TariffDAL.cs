using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bl = BusinessAccessLeyer;
using DataAccessLeyer.EF;
using System.Data.Objects;
using BusinessAccessLeyer;

namespace DataAccessLeyer.DAL
{
   public class Add_TariffDAL
    {
       inventoryforwebappEntities inv;
       bl.Class1 DGM;
       public Add_TariffDAL()
       {
           inv = new inventoryforwebappEntities();
           DGM = new bl.Class1();
       }

       public List<AddTariff_Prop> searchdata_addtariff( string Query)
       {
           DGM.AddTariff_Prop = inv.ExecuteStoreQuery<bl.AddTariff_Prop>(Query).ToList();
           return DGM.AddTariff_Prop.ToList();   
       }

       public List<CustAcno> Tariffcode_fill(string empname , string type, string usercode, string branchcode)
       {
           List<CustAcno> custd = null;
           if (type == "ADMIN")
           {
               custd = inv.ADDTARIFs.Where(em => em.TARIFF_CODE.StartsWith(empname)).Distinct()
                 .Select(c => new CustAcno
                 {
                     tariffcode = c.TARIFF_CODE
                 }).Distinct().ToList();
           }
           else if (type == "PARTNER")
           {
               custd = inv.ADDTARIFs
                 .Where(em => em.B == branchcode && em.TARIFF_CODE.StartsWith(empname)).Distinct().Select(c => new CustAcno
                 {
                     tariffcode = c.TARIFF_CODE
                 }).Distinct().ToList();
           }
           else if (type == "EXECUTIVE")
           {
               custd = inv.ADDTARIFs
                 .Where(em => em.B == branchcode && em.C == usercode && em.TARIFF_CODE.StartsWith(empname)).Distinct().Select(c => new CustAcno
                 {
                     tariffcode = c.TARIFF_CODE
                 }).Distinct().ToList();
           }
           return custd;
       }
    }
}
