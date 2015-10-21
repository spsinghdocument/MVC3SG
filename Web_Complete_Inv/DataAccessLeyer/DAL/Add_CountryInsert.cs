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
   public class Add_CountryInsert
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;

        public Add_CountryInsert()
        {
            inv = new inventoryforwebappEntities();
              DGM = new bl.Class1();
        }
      
        public string AddCountryDal(string f  , string Country, string isdcode, string Indiacalling, string countrycode, string currency, string impnotice, string importantnotice)
        {
            string Msg = string.Empty;
            ObjectParameter Opmsg = new ObjectParameter("Msg", typeof(string));
            int t = inv.Add_CountryInsert_tab(f, Country, isdcode, Indiacalling, countrycode, currency, impnotice, importantnotice, Opmsg);
            Msg = Convert.ToString(Opmsg.Value);
            return Msg; 
        }

        public List<Country_Search> countrysearch(string QryDevice)
        {
            DGM.Country_Search = inv.ExecuteStoreQuery<bl.Country_Search>(QryDevice).ToList();
            return DGM.Country_Search.ToList();
        }
           

        public List<CountryModel> GetCountry()
        {
             List<CountryModel> Coun =  null;
            try
            {

                Coun =
             inv.CountryLists.Select(c => new CountryModel
             {
                 CountryName = c.Country
             }).ToList();
            }
            catch (Exception)
            {
                
               
            }
            return Coun;
           
        }

       
    }
}
