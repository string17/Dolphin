using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.CustomObjects
{
    public class Terminal
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TerminalId { get; set; }
        public string TSNum { get; set; }
        public string TerminalRef { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string TLocation { get; set; }
        public string TAlias { get; set; }
        public int RegId { get; set; }
        public string RegionName { get; set; }
        public string TEng { get; set; }
        public bool TerminalStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}