using DolphinWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DolphinWeb.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AppLogic _service;
        public AccountController()
        {
            _service = new AppLogic();
        }
        // GET: Account



    }
}