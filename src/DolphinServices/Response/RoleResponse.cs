using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinServices.Response
{
    public class RoleResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public bool IsRoleActive { get; set; }
    }
}
