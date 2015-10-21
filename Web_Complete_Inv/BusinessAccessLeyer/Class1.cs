using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace BusinessAccessLeyer
{
    public class Class1
    {
        public List<DeviceModelGrid> DeviceModelGrid { get; set; }
        public List<DeviceModelGrid1> DeviceModelGrid1 { get; set; }
        public List<CountryModelstringItem> CountryModelstringItem { get; set; }
        public List<DeviceModelGridWithChallan> DeviceModelGridWithChallan { get; set; }
        public List<DeviceModelGridWithChallanAll> DeviceModelGridWithChallanAll { get; set; }
        public List<CountryModelstring> CountryModelstring { get; set; }
        public List<voucherentryBAL> voucherentryBAL { get; set; }
        public List<voucherentryBALData> voucherentryBALd { get; set; }
        public List<AddTariff_Prop> AddTariff_Prop { get; set; }
        public List<CompanyProfile_P> CompanyProfile_P { get; set; }
        public List<Country_Search> Country_Search { get; set; }
        public List<PurchaseEnteryItem> PurchaseEnteryItem { get; set; }

        public List<rolesettingprop> rolesettingprop { get; set; }

        public List<CustomerEntrySearch> CustomerEntrySearch { get; set; }

        public List<CLIENTMASTER_PROPERTY> CLIENTMASTER_PROPERTY { get; set; }
        public List<LoginProp> LoginProp { get; set; }
        public List<RolenameProp> RolenameProp { get; set; }
        public List<ClientMaster_Prop> ClientMaster_Prop { get; set; }

        public List<SimWiseSearchProc> SimWiseSearchProc { get; set; }
        public List<Voucher_Prop> Voucher_Prop { get; set; } 
        public List<Voucher_PropNew> Voucher_PropNew { get; set; }
        public List<vafsearchdata> vafsearchdata { get; set; }
        public List<arlst> arlst {get; set; }
        public List<TARIFRET> TARIFRET {get;set; }
        public List<plantableproc> plantableproc { get; set; }
        public List<CONTACTUSDAL> CONTACTUSDAL { get; set; }
        public List<MOBILETOPUP_DATA> MOBILETOPUP_DATA { get; set; }
        public List<SIMREQUEST_PROP> SIMREQUEST_PROP { get; set; }
        public List<BILLEXCEL_PROP> BILLEXCEL_PROP { get; set; }
        
    }


    public class DeviceModelGrid1
    {

        public string itemtype { get; set; }
        public string simno { get; set; }
        public string PhoneNo { get; set; }
        public string simcode { get; set; }
        public string puk { get; set; }

    }

    public class vafsearchdata
    {
        public string customeraddress { get; set; }
        public string Country { get; set; }
        public string mobileno { get; set; }
        public string emailid { get; set; }
        public string customeracno { get; set; }
        public string customername { get; set; }
        
    }

    public class RolenameProp
    {
        public string  RoleSetting { get; set; }

    }

    public class SearchItemType_Prop
    {
        public string itemname { get; set; }
        public string imeino { get; set; }

    }

    public class   CustAcno
    {
         public string  customeracno { get; set; }
         public string plancode { get; set; }
         public string tariffcode { get; set; }
         public string cafno { get; set; }
         public string Voucherno { get; set; }
         public string Date { get; set; }
         public string simno { get; set; }
    }


    public class PurchaseSimWiseSearch
    {
        public string simno { get; set; }
        public string puk { get; set; }
        public string mobileno { get; set; }
    }

    public class LoginProp
    {

        public bool insert { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }
        public bool view { get; set; }
        public string formname { get; set; }

    }

    public class SimWiseSearchProc
    {
        public string PUK { get; set; }
        public string MobileNo { get; set; }
        public DateTime SimDate { get; set; }
        public string Sim_No { get; set; }
        public string Country { get; set; }
        public string TName { get; set; }
        public string LoginCode { get; set; }
       

    }

    public class DeviceModelGrid
    {
        public Int64 sno { get; set; }
        public string billno { get; set; }
        public string Validity { get; set; }
        public string itemtype { get; set; }
        public string simno { get; set; }
        public string country { get; set; }
        public string PhoneNo { get; set; }
        public string apn { get; set; }
        public string simcode { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string puk { get; set; }
        public string challanno { get; set; }

    }

    public class ClientMaster_Prop
    {

        public string User_Name { get; set; }
        public string customeracno { get; set; }
        public string Payment_Mode { get; set; }
        public string Country { get; set; }
        public decimal Amount { get; set; }
        public string Cheque_No { get; set; }
        public string Bank { get; set; }
       

    }
    public class DeviceModelGridWithChallan
    {
        public string challanno { get; set; }
        public string itemtype { get; set; }
        public string others { get; set; }
        public string country { get; set; }
        public string PhoneNo { get; set; }
        public string simcode { get; set; }
        public string puk { get; set; }
        public string executivenameto { get; set; }


    }


    public class DeviceModelGridWithChallanAll
    {

        public string challanno { get; set; }
        public DateTime cdate { get; set; }
        public string itemtype { get; set; }
        public string country { get; set; }
        public string executivename { get; set; }
        public string executivenameto { get; set; }


    }
    public class CountryModel
    {
        public string CountryName { get; set; }
    }

    public class SimModel
    {
        public string Simno { get; set; }
    }

     public class ChallanOutOthersProp
    {
         public string itemname { get; set; }
         public string imeino { get; set; }
    }
    

    public class ChallanInSimNo
    {
        public string others { get; set; }
        public string Sim_No { get; set; }
    }

    public class AlotUser
    {
        public string user_name { get; set; }
        public string usercode { get; set; }
        public string simno { get; set; }
        public string challanno { get; set; }
        
    }

    public class rolenameproc
    {
        public string rolename { get; set; }
    }

    public class CountryModelstring
    {
        public string others { get; set; }
    }


    public class CountryModelstringItem
    {
        public string challanno { get; set; }
        public DateTime cdate { get; set; }
        public string itemtype { get; set; }
        public string itemname { get; set; }
        public string imeino { get; set; }
    }

    public class voucherentryBAL
    {
        public string Voucherno { get; set; }
        public string Date { get; set; }
        public string cafno { get; set; }
        public string Acno { get; set; }
        public string Name { get; set; }
        public string type { get; set; }
        public string paymentmode { get; set; }
        public decimal Amount { get; set; }
        public string Chequeno { get; set; }
        public string Bank { get; set; }
        public string Description { get; set; }
        public decimal netbalance { get; set; }
        public string executivecode { get; set; }
        public string branchcode { get; set; }
    }
    public class voucherentryBALData
    {
        public DataTable datat { get; set; }
    }

    public class AddTariff_Prop
    {
        public Int64 SNO { get; set; }
        public string TARIFF_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string PLANTYPE { get; set; }
        public string CURRENCY { get; set; }
        public string CHARGESTYPE { get; set; }
        public string VALUE { get; set; }
        public string TALK_VALUE { get; set; }
        public string PERMINCHARGES { get; set; }
        public string VALIDITY_DAYS { get; set; }
        public string COUNTRY_FROM { get; set; }
        public string COUNTRY_TO { get; set; }
        public string A { get; set; }

    }


    public class CompanyProfile_P
    {
        public Int64 cmpid { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public string panno { get; set; }
        public string TALK_VALUE { get; set; }
        public string servicetax { get; set; }


    }

    public class Country_Search
    {

        public string Country { get; set; }
        public string isdcode { get; set; }
        public string Indiacalling { get; set; }
        public string countrycode { get; set; }
        public string currency { get; set; }
        public string importantnotice { get; set; }
        public string impnotice { get; set; }

    }

    public class PurchaseEnteryItem
    {

        public long sno { get; set; }
        public string BillNo { get; set; }
        public DateTime Date { get; set; }
        public string ItemType { get; set; }
        public string itemname { get; set; }
        public string loginusercode { get; set; }
        public string SimAlotexeccode { get; set; }
        public string IMEINO { get; set; }

        public string status { get; set; }
        public string branchcode { get; set; }
        public string partnername { get; set; }


    }



    public class rolesettingprop
    {

        public long id { get; set; }
        public string formname { get; set; }
        //   public DateTime Date { get; set; }
        public string insert { get; set; }
        public string view { get; set; }
        public string edit { get; set; }
        public string delete { get; set; }
        public string all { get; set; }

        public string rolsetingname { get; set; }
        public string usercode { get; set; }
        public string branchcode { get; set; }


    }

    public class CustomerEntrySearch
    {
        public Int64 customerdetailstableid { get; set; }
        public string customeracno { get; set; }
        public string customername { get; set; }
        public string customeraddress { get; set; }
        public string emailid { get; set; }
        public string website { get; set; }
        public string passportno { get; set; }
        public string mobileno { get; set; }

        public string alternateno { get; set; }
        public string branchcode { get; set; }

    }

    public class CLIENTMASTER_PROPERTY
    {
        public DateTime clmasterdate { get; set; }
        public string cafno { get; set; } 
         public string tfno { get; set; }
         public string pdffile { get; set; }
         public string customeracno { get; set; }
        public string customername { get; set; }
        public string User_Name { get; set; }
        public string Father_Name { get; set; }
        public string Payment_Mode { get; set; }
        public decimal Amount { get; set; }
        public string Bank { get; set; }
        public string Cheque_No { get; set; }
        public string Sim_No { get; set; }
        public string country { get; set; }
        public string mobileno { get; set; }
        public string Plan { get; set; }
       
        public DateTime Start_Date { get; set; }
        public DateTime Sim_Return_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Validity { get; set; }
        
        public string Tarrif_Detail { get; set; }
        public string Executive_Name { get; set; }
        public string Executive_Code { get; set; }
        public string branchcode { get; set; }
        public string REMARK { get; set; }
       // public string customername { get; set; }
    }

    public class Voucher_Prop
    {
        public string Voucher_date { get; set; } 
     //   public int Sno{ get; set; }
        public string voucher_cafno { get; set; }
        public string Voucher_no { get; set; }
        public decimal V_CAMOUNT { get; set; }
        public decimal V_DAMOUNT { get; set; }
        public decimal V_NETVALUE { get; set; }
        public decimal ACCOUNTNO { get; set; } 
        public string Chequeno { get; set; } 
        public string Bank { get; set; }
        public string Description { get; set; }
        public decimal netbalance { get; set; } 
        public string executivecode { get; set; }
        public string branchcode { get; set; }
        public decimal total { get; set; }

        public string voucherno { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }


    }
    public class Voucher_PropNew
    {
        public Int64 voucher_id { get; set; }
        public string Date { get; set; }
        public int Sno { get; set; }
        public string cafno { get; set; }
        public string Acno { get; set; }
        public string Name { get; set; }
        public string type { get; set; }
        public string paymentmode { get; set; }
        public decimal Amount { get; set; }
        public string Chequeno { get; set; }
        public string Bank { get; set; }
        public string Description { get; set; }
        public decimal netbalance { get; set; }
        public string executivecode { get; set; }
        public string branchcode { get; set; }
        public decimal total { get; set; }

        public string voucherno { get; set; }

    }

    public class arlst
    {
        public DataTable lst { get; set; }
    }
    public class TARIFRET
    {
        public string FIXED { get; set; }
        public string RENTAL { get; set; }
        public string OTHERSCHG { get; set; }
       
    }

    public class plantableproc
    {
        public string TALKTIMEDATA { get; set; }
        
    }
    public class CONTACTUSDAL
    {
        public string NAME { get; set; }
        public string MOBILENO { get; set; }
        public string EMAILID { get; set; }
        public string PRODUCT { get; set; }
        public string COMMENT { get; set; }
    }

    public class MOBILETOPUP_DATA
    {
        public Int64 topid { get; set; }
        public DateTime request_date { get; set; }
        public string plancode { get; set; }
        public string country { get; set; }
        public string mobileno { get; set; }
        public string topup { get; set; }
        public string Clientname { get; set; }
        public string topup_data { get; set; }
        public string topup_sptopup { get; set; }
        public string total { get; set; }
        public string loginuser { get; set; }
        public string branchcode { get; set;}
        public string statu_s { get; set; }
    }

    public class SIMREQUEST_PROP
    {
        public Int64 requestid { get; set; }
        public string Date { get; set; }
        public string Country { get; set; }
        public string SimNo { get; set; }
        public string Priority { get; set; }
        public string Executive_Code { get; set; }
    }
    public class BILLEXCEL_PROP
    {
       
        public string cafno { get; set; }
        public string mobileno { get; set; }
        public string billno { get; set; }
        public string country { get; set; }     
        public string excelfilename { get; set; }
    }
}
