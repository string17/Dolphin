using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccessLayer.CustomObjects
{
    public class EngineerViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserPWD { get; set; }
        public string PhoneNos { get; set; }
        public string UserImg { get; set; }
        public int CustomerId { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int TerminalNum { get; set; }
    
    }
}
