using DolphinWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DolphinWeb.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppLogic _service;
        private readonly SecurityLogic securityLogic;
        private readonly FileLogic file;
        private static string ipaddress = new AuditTrail().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditTrail().DetermineCompName(ipaddress);

        public MenuController()
        {
            _service = new AppLogic();
            securityLogic = new SecurityLogic();
            file = new FileLogic();
        }

        // GET: Menu
        public ActionResult NewRoleMenu()
        {
            return View();
        }

        public ActionResult RoleMenu()
        {
            return View();
        }


        public ActionResult ModifyRoleMenu()
        {
            return View();
        }
    }
}