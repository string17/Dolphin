using DolphinWeb.Models;
using DolphinWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DolphinWeb.Controllers
{
    public class RoleController : Controller
    {
        private readonly AppLogic _service;
        public RoleController()
        {
            _service = new AppLogic();
        }

        // GET: Role
        public ActionResult NewRole()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NewRole(RoleObj param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var success = _service.InsertRole(param);
            if (success != null)
            {
                if (success.RespCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = success.RespMessage;
                    return RedirectToAction("listrole");
                }
                else
                {
                    ViewBag.SuccessMsg = success.RespMessage;
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
            ViewBag.Roles = _service.GetAllRole();
            return View();
        }


        [HttpGet]
        [Route("ModifyRole/{Id}")]
        public ActionResult ModifyRole(int Id)
        {
            ViewBag.Message = "Roles";
            ViewBag.Roles = _service.GetRoleDetails(Id);
            return View();
        }

   
        [Route("ModifyRole/{Id}")]
        public ActionResult ModifyRole(RoleObj param, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var success = _service.ModifyRole(param.RoleName, param.RoleDesc, param.IsRoleActive, Id);
            if (success !=null)
            {
                if (success.RespCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = success.RespMessage;
                    return RedirectToAction("listrole");
                }
                else
                {
                    ViewBag.ErrorMsg = success.RespMessage;
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