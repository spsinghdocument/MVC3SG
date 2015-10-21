using System;
namespace PresantationAccessLeyer.Models
{
    public class Customer
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
        public string executivenameto { get; set; }
        public string DATETIME { get; set; }
        public string NUMBER { get; set; }
        public string TYPE { get; set; }
        public string DURATION { get; set; }
        public string UNITS { get; set; }
        public string RATE { get; set; }
        public string COST { get; set; }
    }
}