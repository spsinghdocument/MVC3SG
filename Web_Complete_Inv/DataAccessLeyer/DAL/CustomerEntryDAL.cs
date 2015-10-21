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
    public class CustomerEntryDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public CustomerEntryDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }

        public string insertcustomertable(string f, string customeracno, string customername, string customeraddress,
          string emailid, string website, string passportno, string mobileno, string alternateno, string execusercode, string branchcode)
        {
            string Msg = string.Empty;
            ObjectParameter opmg = new ObjectParameter("Msg", typeof(string));
            inv.customerdetailstableproc_Insert(f, 0, customeracno, customername, customeraddress,
                emailid, website, passportno, mobileno, alternateno, execusercode, branchcode, opmg);
            Msg = Convert.ToString(opmg.Value);
            return Msg;
        }

        
        public List<CustomerEntrySearch> customerserach(string Query)
        {
            DGM.CustomerEntrySearch = inv.ExecuteStoreQuery<bl.CustomerEntrySearch>(Query).ToList();
            return DGM.CustomerEntrySearch.ToList();
        }

        public string autobillno(string branchcode, string usercode)
        {
            string Msg = string.Empty;
            try
            {
                ObjectParameter obmg = new ObjectParameter("Msg", typeof(string));
                inv.customerautobillno_proc_search(branchcode, "CUSTOMER", usercode, obmg);
                Msg = Convert.ToString(obmg.Value);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return Msg;
        }

        public List<AlotUser> customername(string empName, string usertype, string branchcode, string sessid)
        {
            List<AlotUser> custd = null;
            switch (usertype)
            {

                case "ADMIN":
                    custd = inv.customerdetailstables
                     .Where(em => em.customername.Contains(empName)).Select(em1 => new AlotUser
                     {
                         user_name = em1.customername
                     }).ToList();
                    break;

                case "PARTNER":
                    custd = inv.customerdetailstables
                     .Where(em => em.branchcode == branchcode && em.customername.Contains(empName)).Select(em1=>new AlotUser
                         {
                             user_name = em1.customername
                         }) .ToList();
                    break;

                case "EXECUTIVE":
                    custd = inv.customerdetailstables
                     .Where(em => em.branchcode == branchcode && em.loginusercode == sessid && em.customername.Contains(empName))
                     .Select(em1=> new AlotUser
                         {
                             user_name = em1.customername
                         }) .ToList();
                    break;
            }
            return custd;
        }

        public List<AlotUser> customeracno(string empName, string usertype, string branchcode, string sessid)
        {
            List<AlotUser> custd = null;
            switch (usertype)
            {

                case "ADMIN":
                    custd = inv.customerdetailstables
                     .Where(em => em.customeracno.Contains(empName)).Select(em1 => new AlotUser
                     {
                         user_name = em1.customeracno
                     }).ToList();
                    break;

                case "PARTNER":
                    custd = inv.customerdetailstables
                     .Where(em => em.branchcode == branchcode && em.customeracno.Contains(empName)).Select(em1 => new AlotUser
                     {
                         user_name = em1.customeracno
                     }).ToList();
                    break;

                case "EXECUTIVE":
                    custd = inv.customerdetailstables
                     .Where(em => em.branchcode == branchcode && em.loginusercode == sessid && em.customeracno.Contains(empName))
                     .Select(em1 => new AlotUser
                     {
                         user_name = em1.customeracno
                     }).ToList();
                    break;
            }
            return custd;
        }

        public void customernamesearch()
        {

        }
    }
}
