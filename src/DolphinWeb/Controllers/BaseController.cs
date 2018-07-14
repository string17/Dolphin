using DolphinServices.Infrastructure;
using DolphinServices.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DolphinWeb.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string SystemIP = System.Web.HttpContext.Current.Request.Params["REMOTE_ADDR"] ?? System.Web.HttpContext.Current.Request.UserHostAddress;
            string SystemName = System.Environment.UserName;
            var _service = new Services();
            
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                //TempData["ProfileMsg"] = TempData["ProfileMsg"];
                TempData["Success"] = TempData["Success"];
                //TempData["UserName"] = TempData["UserName"];
                TempData["ChangePassword"] = TempData["ChangePassword"];
                FormsAuthentication.SignOut();
                RedirectToAction("Index", "Dolphin", new { SystemIP = SystemIP, SystemName = SystemName });
            }
            else
            {
                ViewBag.LayoutModel = _service.GetMenu(User.Identity.Name).UserMenuDetails;
                var userInfo = _service.GetUserInfo(User.Identity.Name);
                ViewBag.UserData = userInfo.UserDetails;
            }

        }
    }
}