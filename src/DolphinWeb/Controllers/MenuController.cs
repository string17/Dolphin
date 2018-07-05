using DolphinServices.ApplicationLogic;
using DolphinServices.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DolphinWeb.Controllers
{
    public class MenuController : BaseController
    {
        private readonly Services _dolphinApi;
        private readonly AuditService _auditService;
        private readonly EncodingCharacters _encodingService;
        private readonly UploadAttachment _uploadFile;
        private static string ipaddress = new AuditService().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditService().DetermineCompName(ipaddress);

        public MenuController()
        {
            _dolphinApi = new Services();
            _auditService = new AuditService();
            _encodingService = new EncodingCharacters();
            _uploadFile = new UploadAttachment();
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