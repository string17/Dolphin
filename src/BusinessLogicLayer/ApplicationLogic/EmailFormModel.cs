﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApplicationLogic
{
    public class EmailFormModel
    {
        public string FromName { get; set; }

        public string FromEmail { get; set; }

        public string Message { get; set; }
        public string Receiver { get; set; }
    }
}
