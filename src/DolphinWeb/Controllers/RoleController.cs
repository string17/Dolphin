using DolphinServices.ApplicationLogic;
using DolphinServices.Infrastructure;
using DolphinServices.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DolphinWeb.Controllers
{
    public class RoleController : BaseController
    {
        private readonly Services _dolphinApi;
        private readonly AuditService _auditService;
        private readonly EncodingCharacters _encodingService;
        private readonly UploadAttachment _uploadFile;
        private static string ipaddress = new AuditService().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditService().DetermineCompName(ipaddress);
        public RoleController()
        {
            _dolphinApi = new Services();
        }

        // GET: Role
        public ActionResult NewRole()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NewRole(RoleRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var request = new RoleRequest();
            request.RoleName = param.RoleName;
            request.RoleDesc = param.RoleDesc;
            request.IsRoleActive = param.IsRoleActive;
            request.Computername = ComputerDetails;
            request.SystemIp = ipaddress;
            var success = _dolphinApi.InsertRole(param);
            if (success != null)
            {
                if (success.ResponseCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = success.ResponseMessage;
                    return RedirectToAction("listrole");
                }
                else
                {
                    ViewBag.SuccessMsg = success.ResponseMessage;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Unsuccessful operation";
            }
            return View();
        }

        public ActionResult ListRole()
        {
            ViewBag.Message = "Roles";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.Roles = _dolphinApi.GetAllRole();
            return View();
        }


        [HttpGet]
        [Route("ModifyRole/{Id}")]
        public ActionResult ModifyRole(int Id)
        {
            ViewBag.Message = "Roles";
            ViewBag.Roles = _dolphinApi.GetRoleDetails(Id);
            return View();
        }

   
        [Route("ModifyRole/{Id}")]
        public ActionResult ModifyRole(RoleRequest param, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var request = new RoleRequest();
            request.RoleName = param.RoleName;
            request.RoleDesc = param.RoleDesc;
            request.IsRoleActive = param.IsRoleActive;
            request.RoleId = Id;
            request.SystemIp = ipaddress;
            request.Computername = ComputerDetails;
            var success = _dolphinApi.ModifyRole(request);
            if (success !=null)
            {
                if (success.ResponseCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = success.ResponseMessage;
                    return RedirectToAction("listrole");
                }
                else
                {
                    ViewBag.ErrorMsg = success.ResponseMessage;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Unsuccessful operation";
            }
            return View();
        }
    }
}