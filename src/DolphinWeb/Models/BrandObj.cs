using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinWeb.Models
{
    public class BrandResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
    }

    public class BrandObj
    {
        public int BrandId { get; set; }
        public string BrandTitle { get; set; }
        public string BrandDesc { get; set; }
        public bool IsBrandActive { get; set; }
        public string UserName { get; set; }
        public string SystemName { get; set; }
        public string SystemIp { get; set; }
    }
}
