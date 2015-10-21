using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLeyer.EF;
using BusinessAccessLeyer;
using bl = BusinessAccessLeyer;


namespace DataAccessLeyer.DAL
{
   public class PaymentReportDAL
    {
        inventoryforwebappEntities inv;
        bl.Class1 DGM;
        public PaymentReportDAL()
        {
            inv = new inventoryforwebappEntities();
            DGM = new bl.Class1();
        }
        public List<PaymentGroupReport> ilist = new List<PaymentGroupReport>();
        public List<PaymentGroupReport> reportdal(string f, string userid, string usertupe, string branchcode, string vdate, int pageIndex)
        {
            List<Paymentsheet_Result>ab=new List<Paymentsheet_Result>();

            try
                {

                    int pagesize = 50;
                    int pageSkipe = (pageIndex - 1) * pagesize;
                    PaymentGroupReport EmpGM = new PaymentGroupReport();
                    EmpGM.EmpList = inv.Paymentsheet(f, usertupe, branchcode, userid, vdate).OrderBy(em=>em.ACCOUNTNO).Skip(pageSkipe).Take(pagesize).ToList();
                    EmpGM.noofPage = (inv.Paymentsheet(f, usertupe, branchcode, userid, vdate).Count() / pagesize) + 1;

          //  ab= inv.Paymentsheet(f, usertupe, branchcode, userid, vdate).ToList() ;

                    ilist.Add(EmpGM);
            }
                catch
            {}
            return ilist.ToList() ;
        }

        public List<CustAcno> Search_ACNO(string empName,string opt1, string type, string usercode, string branchcode)
        {
            List<CustAcno> custd = null;
        if (opt1 == "EXECUTIVE WISE") {
           
            if (type == "ADMIN")
            {
                custd = inv.vouchertables
                   .Where(em => em.executivecode.Contains(empName)).Select(c => new CustAcno
                   {
                       customeracno = c.executivecode
                   }).Distinct().ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.vouchertables
                  .Where(em => em.branchcode == branchcode && em.executivecode.StartsWith(empName)).Select(c => new CustAcno
                  {
                      customeracno = c.executivecode
                  }).Distinct().ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.vouchertables
                  .Where(em => em.branchcode == branchcode && em.executivecode == usercode && em.executivecode.Contains(empName)).Select(c => new CustAcno
                  {
                      customeracno = c.executivecode
                  }).Distinct().ToList();
            }
            
        }
       
        else if (opt1 == "BRANCH WISE") {
            if (type == "ADMIN")
            {
                custd = inv.vouchertables
                   .Where(em => em.branchcode.Contains(empName)).Select(c => new CustAcno
                   {
                       customeracno = c.branchcode
                   }).Distinct().ToList();
            }      
            
        }
        
        
        else if (opt1 == "ACNO WISE") {
            if (type == "ADMIN")
            {
                custd = inv.vouchertables
                   .Where(em => em.Acno.Contains(empName)).Select(c => new CustAcno
                   {
                       customeracno = c.Acno
                   }).Distinct().ToList();
            }
            else if (type == "PARTNER")
            {
                custd = inv.vouchertables
                  .Where(em => em.branchcode == branchcode && em.Acno.StartsWith(empName)).Select(c => new CustAcno
                  {
                      customeracno = c.Acno
                  }).Distinct().ToList();
            }
            else if (type == "EXECUTIVE")
            {
                custd = inv.vouchertables
                  .Where(em => em.branchcode == branchcode && em.executivecode == usercode && em.Acno.Contains(empName)).Select(c => new CustAcno
                  {
                      customeracno = c.Acno
                  }).Distinct().ToList();
            }
            
        }
       
           
           
            return custd;
        }


        public List<Voucher_Prop> SearchAcDAL(string EID, string type, string branchcode, string loginuser)
        {
            string SubQry = string.Empty;
            string QryDevice = string.Empty;
            //QryDevice = "select distinct Date, paymentmode,cafno,voucherno, Amount, Chequeno, Bank, Description, netbalance, executivecode, branchcode, " +
            //             "(select SUM(netbalance) from vouchertable where Acno = '" + EID + "')  as total,ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno ='" + EID + "'  and  [type] = 'CREDIT' )  ,0) as credit,(ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno ='" + EID + "'  and [type] = 'DEBIT'  )  ,0)) AS debit,type     from vouchertable where Acno='" + EID + "' ";
          
            switch (type)
            {
                case "ADMIN":
                    QryDevice = "select distinct Voucher_date, voucher_cafno  ,Voucher_no ," +
"(ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'CREDIT' and voucherno =Voucher_no   )  ,0)) AS V_CAMOUNT , " +
" (ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'DEBIT'  and voucherno =Voucher_no  )  ,0)) as V_DAMOUNT , " +

"  (((ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'DEBIT' and voucherno =Voucher_no )  ,0))))  " +
 " -((ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'CREDIT'  and voucherno =Voucher_no )  ,0))) as V_NETVALUE " +
 " from  (SELECT distinct vouchertable.Date as Voucher_date,vouchertable.cafno as voucher_cafno ,vouchertable.Voucherno as Voucher_no ,vouchertable.Acno as ACCOUNTNO    " +
" FROM vouchertable where Acno = '" + EID + "' ) as test group by Voucher_date,voucher_cafno,Voucher_no,  ACCOUNTNO  ";
                    break;

                case "PARTNER":
                    QryDevice = "select distinct Voucher_date, voucher_cafno  ,Voucher_no ," +
 "(ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'CREDIT' and voucherno =Voucher_no   )  ,0)) AS V_CAMOUNT , " +
 " (ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'DEBIT'  and voucherno =Voucher_no  )  ,0)) as V_DAMOUNT , " +

 "  (((ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'DEBIT' and voucherno =Voucher_no )  ,0))))  " +
  " -((ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'CREDIT'  and voucherno =Voucher_no )  ,0))) as V_NETVALUE " +
  " from  (SELECT distinct vouchertable.Date as Voucher_date,vouchertable.cafno as voucher_cafno ,vouchertable.Voucherno as Voucher_no ,vouchertable.Acno as ACCOUNTNO    " +
 " FROM vouchertable where Acno = '" + EID + "'  and branchcode ='" + branchcode + "' ) as test group by Voucher_date,voucher_cafno,Voucher_no,  ACCOUNTNO  ";
                    break;
                case "EXECUTIVE":
                    QryDevice = "select distinct Voucher_date, voucher_cafno  ,Voucher_no ," +
    "(ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'CREDIT' and voucherno =Voucher_no   )  ,0)) AS V_CAMOUNT , " +
    " (ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'DEBIT'  and voucherno =Voucher_no  )  ,0)) as V_DAMOUNT , " +

    "  (((ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'DEBIT' and voucherno =Voucher_no )  ,0))))  " +
     " -((ISNULL((select SUM (Amount) as  BALANCE from vouchertable where acno =ACCOUNTNO and [type] = 'CREDIT'  and voucherno =Voucher_no )  ,0))) as V_NETVALUE " +
     " from  (SELECT distinct vouchertable.Date as Voucher_date,vouchertable.cafno as voucher_cafno ,vouchertable.Voucherno as Voucher_no ,vouchertable.Acno as ACCOUNTNO    " +
    " FROM vouchertable where Acno = '" + EID + "'  and branchcode ='" + branchcode + "' ) as test group by Voucher_date,voucher_cafno,Voucher_no,  ACCOUNTNO  ";

                    break;
            }


            DGM.Voucher_Prop = inv.ExecuteStoreQuery<bl.Voucher_Prop>(QryDevice).ToList();
            return DGM.Voucher_Prop.ToList();

        }
    }
  public class PaymentGroupReport
   {
       public List<Paymentsheet_Result> EmpList { get; set; }
       public int noofPage { get; set; }
   }
}
