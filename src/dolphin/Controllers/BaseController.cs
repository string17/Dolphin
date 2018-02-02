using BLL.ApplicationLogic;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace dolphin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var service = new MenuManagement();
            var userManagement = new UserManagement();
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                TempData["ProfileMsg"] = TempData["ProfileMsg"];
                FormsAuthentication.SignOut();
                RedirectToAction("Login", "Account");
            }

            ViewBag.LayoutModel = service.getMenuByUsername(User.Identity.Name);
            var userDetails = userManagement.getMenuByUsername(User.Identity.Name);
            ViewBag.UserData = userDetails;
        }

        public void InsertAudit(Constants.AuditActionType action_type, string action, string performed_by)
        {
            AuditService service = new AuditService();
            AuditTrail audit = new AuditTrail();
            audit.UserName = performed_by; //System.Web.HttpContext.Current.User.Identity.Name;
            audit.UserActivity = action_type.ToString();
            audit.Comments = action;
            audit.DateLog = DateTime.Now;
            audit.SystemName = System.Environment.UserName;
            audit.SystemIP = System.Web.HttpContext.Current.Request.Params["REMOTE_ADDR"] ?? System.Web.HttpContext.Current.Request.UserHostAddress;
            service.insertAudit(audit);
            return;
        }

        public string GetIP()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        
    }
}