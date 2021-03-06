﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinWeb.Models
{
    public class StateResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
    }

    public class StateObj
    {
        public int StateId { get; set; }
        public string StateTitle { get; set; }
        public string StateDesc { get; set; }
        public bool IsStateActive { get; set; }
    }
}
