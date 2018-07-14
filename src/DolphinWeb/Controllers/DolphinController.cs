using DolphinServices.ApplicationLogic;
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
    public class DolphinController : BaseController
    {
        private readonly Services _dolphinApi;
        private readonly AuditService _auditService;
        private readonly EncodingCharacters _encodingService;
        private static string ipaddress = new AuditService().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditService().DetermineCompName(ipaddress);

        public DolphinController()
        {
            _dolphinApi = new Services();
            _auditService = new AuditService();
            _encodingService = new EncodingCharacters();
        }


        [AllowAnonymous]
        [Route("Index/{SystemName,SystemIP}")]
        public ActionResult Index()
        {
            ViewBag.SuccessMsg = TempData["Profile"];
            ViewBag.SuccessMsg = TempData["Success"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var request = new LoginRequest();
                request.UserName = param.UserName;
                request.Password = param.Password;
                request.SystemIp = ipaddress;
                request.Computername = ComputerDetails;
                var result = _dolphinApi.ValidateUser(request);
                if (result == null)
                {
                    ViewBag.ErrorMsg = "This service is not available";
                }
                else if (result.ResponseCode.Equals("00"))
                {
                    FormsAuthentication.SetAuthCookie(param.UserName, false);
                    return RedirectToAction("Dashboard", "Dolphin");
                }

                else if (result.ResponseCode.Equals("01"))
                {
                    string Id = _encodingService.EncryptCharacter(param.UserName);
                    TempData["ChangePassword"] = result.ResponseMessage;
                    string NewURL = "http://localhost:51310/dolphin/resetpassword?Id=" + Id;
                    Response.Redirect(NewURL, true);
                }
                else
                {
                    ViewBag.ErrorMsg = result.ResponseMessage;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
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
            //var request = new LoginRequest();
            //request.UserName = User.Identity.Name;
            //request.SystemIp = ipaddress;
            //request.Computername = ComputerDetails;
            ViewBag.UserDetails = _dolphinApi.GetUserInfo(User.Identity.Name);
            return View();
        }


        [HttpPost]
        public ActionResult LockAccount(LoginRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var login = new LoginRequest();
                login.UserName = param.UserName;
                login.Password = param.Password;
                login.SystemIp = ipaddress;
                login.Computername = ComputerDetails;
                var success = _dolphinApi.ValidateUser(login);
                if (success != null)
                {
                    if (success.ResponseCode.Equals("00"))
                    {
                        FormsAuthentication.SetAuthCookie(param.UserName, false);
                        return RedirectToAction("Dashboard", "Dolphin");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.ResponseMessage;
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "This service is not available";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
            }
            return View();
        }


        public ActionResult Logout()
        {
            TempData["ProfileMsg"] = TempData["ProfileMsg"];
            var request = new LoginRequest();
            request.UserName = User.Identity.Name;
            request.SystemIp = ipaddress;
            request.Computername = ComputerDetails;
            var success = _dolphinApi.TerminateSession(request);
            if (success != null)
            {
                if (success.ResponseCode.Equals("00"))
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Index", "Dolphin");
                }
                else
                {
                    return View();
                }
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
        public ActionResult ForgotPassword(LoginRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var request = new LoginRequest();
            request.Email = param.Email;
            request.SystemIp = ipaddress;
            request.Computername = ComputerDetails;
            var success = _dolphinApi.ForgotPassword(request);
            if (success != null)
            {
                if (success.ResponseCode.Equals("00"))
                {
                    ViewBag.SuccessMsg = success.ResponseMessage;
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



        [HttpGet]
        [Route("ResetPassword/{Id}")]
        public ActionResult ResetPassword(string Id)
        {
            ViewBag.SuccessMsg = TempData["ChangePassword"];
            //string Username = _encodingService.DecryptCharacter(Id);
            //var userDetails = _dolphinApi.GetUserInfo();
            //ViewBag.User = userDetails;
            return View();
        }

        [Route("ResetPassword/{Id}")]
        public ActionResult ResetPassword(LoginRequest param, string Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (param.Password.Any("!@#$%^&*".Contains) && param.Password.Length >= 6)
            {
                if (param.Password == param.ConfirmPassword)
                {
                    var request = new LoginRequest();
                    request.UserName = _encodingService.DecryptCharacter(Id);
                    request.Password = param.Password;
                    request.SystemIp = ipaddress;
                    request.Computername = ComputerDetails;
                    var success = _dolphinApi.ResetPassword(param);
                    if (success != null)
                    {
                        if (success.ResponseCode.Equals("00"))
                        {
                            TempData["Success"] = success.ResponseMessage;
                            return RedirectToAction("login");
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