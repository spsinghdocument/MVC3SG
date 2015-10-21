using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresantationAccessLeyer.Models
{
    public class Add_Tariffcls : List<ADD_TARIFFCLS1>
    {
        public string ImageUrl { get; set; }
        public string TARIFFCODE  { get; set; }
        public string COUNTRY { get; set; }
        public string RENTAL { get; set; }
        public string FREEVALUE { get; set; }
        public string INCOMING { get; set; }
        public string LOCALOUTGOING { get; set; }
        public string INTCALL { get; set; }
        public string LOCALSMS { get; set; }
        public string INTSMS { get; set; }
        public string DATAUSE { get; set; }
        public string VALIDITY { get; set; }
        public string CUG { get; set; }
        public string DATAPER { get; set; }
        public string CURRENCY { get; set; }
    }
}