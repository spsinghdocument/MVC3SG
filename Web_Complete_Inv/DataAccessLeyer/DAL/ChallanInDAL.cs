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
    public  class ChallanInDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public ChallanInDAL()
       {
           inv = new inventoryforwebappEntities();
           DGM = new bl.Class1();
       }

        public string ChallaninValidate(string f, string Challanno, string cDate, string SimNo, string Country,string loginusercode, string branchcode,string status)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));
                int t = inv.pChallanin_Proc(cDate, SimNo, Challanno, f, Country, "NA", "NA", loginusercode, branchcode, status, opmg);

                Msg = Convert.ToString(opmg.Value);
            }
            catch (Exception)
            {
                Msg = "Please Check All Field .....";

            }
            return Msg;
        }

        public List<ChallanInSimNo> GetSim_challanout(string Countryname, string country)
        {
            List<ChallanInSimNo> st =
                inv.challanouts.Where(em => em.country ==country && em.status == "NOT IN USE" &&  em.others.Contains(Countryname)).Select(c => new ChallanInSimNo
                {
                    others = c.others
                }).ToList();
            return st;

        }

        public List<ChallanInSimNo> GetSim_clientmaster(string Countryname , string country)
        {
            List<ChallanInSimNo> st =
                inv.ClientMasters.Where(s => s.Country == country && s.status == "ACTIVE" && s.Sim_No.Contains(Countryname)).Select(c => new ChallanInSimNo
                {
                    Sim_No = c.Sim_No
                }).ToList();
            return st;
        }

        public List<AlotUser> GetUsername_OnselectDAL(string execcode)
        {
            List<AlotUser> st =
                inv.User_Name.Where(s => s.UserName == execcode).Select(c => new AlotUser
                {
                    user_name = c.User_Code
                }).ToList();
            return st;
        }

        public string ChallaninOTHREValidate(string f, string Challanno, string cDate, string SimNo, string Country, string loginusercode, string branchcode, string status,string HAND_SET,string handover_c)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));
                int t = inv.challanINothersproc(f, Challanno, cDate, Country, SimNo, HAND_SET, handover_c, "", "", "", status, branchcode, "");
                   // inv.challanINothersproc(f, Challanno, cDate, Country, SimNo, HAND_SET, handover_c,"" , "", status, branchcode, "");
             //  int t = inv.pChallanin_Proc(cDate, SimNo, Challanno, f, Country, "NA", "NA", loginusercode, branchcode, status, opmg);

                Msg = Convert.ToString(opmg.Value);
            }
            catch (Exception)
            {
                Msg = "Please Check All Field .....";

            }
            return Msg;
        }
    }
}
