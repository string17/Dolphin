using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.CustomObjects
{
    public class IncidentViewModel
    {
        public int IncidentId { get; set; }
        public string TerminalId { get; set; }
        public string UserName { get; set; }
        public int IncidentDesc { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public string TSNum { get; set; }
        public string TerminalRef { get; set; }
        public string TAlias { get; set; }
        public string TEng { get; set; }
        public DateTime IncidentDateTime { get; set; }
        public int IncidentStatus { get; set; }
        public string BrandName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CustomerRespTime { get; set; }
        public string CustomerRestTime { get; set; }
    }
}