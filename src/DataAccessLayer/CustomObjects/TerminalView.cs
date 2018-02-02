using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.CustomObjects
{
    public class TerminalView
    {
        public int? Tid { get; set; }
        public string CustomerAlias { get; set; }
        public string TEng { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? RegId { get; set; }
        public string RegName { get; set; }
        public string TerminalId { get; set; }
        public string TSNum { get; set; }
        public string TerminalRef { get; set; }
        public string BrandName { get; set; }
        public string TLocation { get; set; }
        public bool? TerminalStatus { get; set; }
        public string TAlias { get; set; }
        public string RegionName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNos { get; set; }



    }
}