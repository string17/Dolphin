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
                TempData["SuccessMsg"] = TempData["SuccessMsg"];
                //TempData["UserName"] = TempData["UserName"];
                TempData["ChangePassword"] = TempData["ChangePassword"];
                FormsAuthentication.SignOut();
                RedirectToAction("Index", "Dolphin", new { SystemIP = SystemIP, SystemName = SystemName });
                                
            }
            else if(!string.IsNullOrEmpty(User.Identity.Name))
            {
                var request = new LoginRequest();
                request.UserName = User.Identity.Name;
                request.SystemIp = SystemIP;
                request.Computername = SystemName;
                ViewBag.LayoutModel = _service.GetMenu(request);
                var userDetails = _service.GetUserInfo(request);
                ViewBag.UserData = userDetails;
            }

            //if (string.IsNullOrEmpty(User.Identity.Name))
            //{
            //    //TempData["ProfileMsg"] = TempData["ProfileMsg"];
            //    TempData["Success"] = TempData["Success"];
            //    //TempData["UserName"] = TempData["UserName"];
            //    TempData["ChangePassword"] = TempData["ChangePassword"];
            //    FormsAuthentication.SignOut();
            //    RedirectToAction("Index", "Dolphin", new { SystemIP = SystemIP, SystemName = SystemName });
            //}


            //ViewBag.LayoutModel = _service.GetMenu(User.Identity.Name, SystemName, SystemIP);
            //var userDetails = _service.GetUserInfo(User.Identity.Name, SystemName, SystemIP);
            //ViewBag.UserData = userDetails;
        }
    }
}