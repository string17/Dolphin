using DolphinWeb.Models;
using DolphinWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DolphinWeb.Controllers
{
    public class DolphinController : BaseController
    {
        private readonly AppLogic _service;
        private readonly AuditTrail auditTrail;
        private readonly SecurityLogic securityLogic;
        private static string ipaddress = new AuditTrail().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditTrail().DetermineCompName(ipaddress);

        public DolphinController()
        {
            _service = new AppLogic();
            auditTrail = new AuditTrail();
            securityLogic = new SecurityLogic();
        }


        [AllowAnonymous]
        //[Route("Index/{SystemName,SystemIP}")]
        public ActionResult Index()
        {
            ViewBag.SuccessMsg = TempData["Profile"];
            ViewBag.SuccessMsg = TempData["Success"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserObj param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            try
            {
                var isValid = _service.ValidateUser(param.Username, param.Password, ComputerDetails, ipaddress);
                if (isValid != null)
                {
                    if (isValid.RespCode.Equals("00"))
                    {
                        FormsAuthentication.SetAuthCookie(param.Username, false);
                        return RedirectToAction("Dashboard", "Dolphin");
                    }

                    else if (isValid.RespCode.Equals("01"))
                    {
                        string Id = securityLogic.EncryptPassword(param.Username);
                        TempData["ChangePassword"] = "Kindly change your passowrd";
                        string NewURL = "http://localhost:51310/dolphin/resetpassword?Id=" + Id;
                        Response.Redirect(NewURL, true);
                    }
                    else
                    {
                        ViewBag.ErrorMsg = isValid.RespMessage;
                        return View();
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "This service is not available";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
            return View();
        }



        public ActionResult Login(string Username, string Password)
        {
            //if (!ModelState.IsValid)
            //{
            var codes = Username + " " + Password;
            return Json(codes, JsonRequestBehavior.AllowGet);

        }


        public ActionResult LockAccount()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LockAccount(UserObj user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return View();
        }


        public ActionResult Logout()
        {
            TempData["ProfileMsg"] = TempData["ProfileMsg"];
            bool userLogout=_service.TerminateSession(User.Identity.Name, ComputerDetails, ipaddress);
            if (userLogout)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Dolphin");
            }
            else
            {
                return View();
            }
            
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgotPassword(UserObj param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var forgotPwd = _service.ForgotPassword(param.Email, ComputerDetails, ipaddress);
            if (forgotPwd.RespCode.Equals("00"))
            {
                ViewBag.SuccessMsg = forgotPwd.RespMessage;
            }
            else
            {
                ViewBag.ErrorMsg = forgotPwd.RespMessage;
            }
            return View();
        }



        [HttpGet]
        [Route("ResetPassword/{Id}")]
        public ActionResult ResetPassword(string Id)
        {
            ViewBag.SuccessMsg = TempData["ChangePassword"];
            string Username = securityLogic.DecryptPassword(Id);
            var userDetails = _service.GetUserInfo(Username, ComputerDetails, ipaddress);
            ViewBag.User = userDetails;
            return View();
        }

        [Route("ResetPassword/{Id}")]
        public ActionResult ResetPassword(UserObj param, string Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (param.Password.Any("!@#$%^&*".Contains) && param.Password.Length >= 6)
            {
                if(param.Password == param.ConfirmPassword)
                {
                    var changePassword = _service.ResetPassword(Id,param.Password,ComputerDetails,ipaddress);
                    if (changePassword.RespCode.Equals("00"))
                    {
                        TempData["Success"] = "Kindly Login with the new password";
                        return RedirectToAction("login");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = changePassword.RespMessage;
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "The two password are not equal";
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Password must contain special character and min of six in length";
            }
            return View();
        
        }


        public ActionResult Dashboard()
        {
            return View();
        }

       
        public ActionResult ModifyProfile()
        {
            return View();
        }


       
    }
}