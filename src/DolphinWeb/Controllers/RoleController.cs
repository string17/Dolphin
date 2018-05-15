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
            bool Role = _service.InsertRole(param);
            if (Role)
            {
                ViewBag.SuccessMsg = "Successful";
            }
            else
            {
                ViewBag.SuccessMsg = "Failed";
            }
            return View();
        }

        public ActionResult ListRole()
        {
            ViewBag.Message = "Roles";
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
            bool success = _service.ModifyRole(param.Title, param._Desc, param.IsRoleActive, Id);
            if (success)
            {
                //ViewBag.SuccessMsg = "Role successfully modified";
                TempData["SuccessMsg"] = "Role successfully modified";
                return RedirectToAction("listrole");
            }
            else
            {
                ViewBag.ErrorMsg = "Role modification failed";
            }
            return View();
        }
    }
}