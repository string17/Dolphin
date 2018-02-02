using BLL.ApplicationLogic;
using BusinessLogicLayer.ApplicationLogic;
using DataAccessLayer.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace dolphin.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ProfileMsg = TempData["ProfileMsg"];
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLogin User)
        {
            string ErrorMsg = "";
            try
            {
                User.Username = User.Username.ToUpper();
                User.Password = User.Password;
                UserManagement userManagement = new UserManagement();
                if (!userManagement.DoesUsernameExists(User.Username))
                {
                    ErrorMsg = "Invalid Username";
                    //LogManager.ErrorLog(ErrorMsg);
                    LogManager.InsertLog(Constants.AuditActionType.Login, ErrorMsg, User.Username);
                    ViewBag.ErrorMsg = "Invalid Username";
                    
                    return View();
                }
                User.Password = userManagement.base64Encode(User.Password);// Encode password

                if (!userManagement.DoesPasswordExists(User.Username,User.Password))
                {
                    ViewBag.ErrorMsg = "Invalid Password";
                    return View();
                }
                //Company company = new Company();
                             
                var ActiveUser = userManagement.getUserByUsername(User.Username);
                if (ActiveUser != null)
                {
                    var AccountStatus = ActiveUser.UserStatus;
                    if (AccountStatus==true)
                    {
                        var userDetails = userManagement.getUserByUsername(User.Username);
                        Company company = new Company();
                        var CompanyDetails = company.getCompanyId(userDetails.CustomerId);
                        if (CompanyDetails.CustomerStatus==true)
                        {
                            var newUser = userManagement.getFreshUser(User.Username);
                            if (newUser != 0)
                            {
                                FormsAuthentication.SetAuthCookie(ActiveUser.UserName, false);
                                LogManager.InsertLog(Constants.AuditActionType.Login, "Successfully Login", User.Username);
                                InsertAudit(Constants.AuditActionType.Login, "Successfully Login", User.Username);
                                return RedirectToAction("Dashboard", "Dolphin");
                            }
                            else
                            {
                                int Id = ActiveUser.UserId;
                                TempData["ChangePassword"] = "Kindly change your passowrd";
                                string NewURL = "http://localhost:26248/account/reset_password?Id=" + Id;
                                Response.Redirect(NewURL, true);
                            }
                            
                        }
                        else
                        {
                            ViewBag.ErrorMsg = "Your institution is not active";
                            return View();
                        }
                     }
                    else
                    {
                        ViewBag.ErrorMsg = "You are not an active user";
                        return View();
                    }
                 }
               
                return View(User);
            }
            catch(Exception ex)
            {
                ErrorMsg = "Login details are wrong";
                ViewBag.ErrorMsg = "Login details are wrong.";
                //LogManager.InsertLog(Constants.AuditActionType.Login, ErrorMsg , User.Username);
                LogManager.ErrorLoging(ex);
                return View();
            }

        }


        public ActionResult Forgot_password()
        {
            ViewBag.Title = "Forgot Password";
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Forgot_password(UserLogin User)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User.Username = User.Username.ToUpper();
            UserManagement usermgt = new UserManagement();
            var extAccount = usermgt.getUserByUsername(User.Username);
            if (extAccount != null)
            {
                EmailFormModel emailModel = new EmailFormModel();
                long Id = extAccount.UserId;
                string txtRecipient = extAccount.UserName.ToLower();
                string PasswordUrl = WebConfigurationManager.AppSettings["BaseURL"];
                var body = "Kindly click on this link  to reset your password. </br>" + PasswordUrl + "Account/Reset_password?Id=" + Id;
                var message = new MailMessage();
                message.To.Add(new MailAddress(txtRecipient));  // replace with valid value 
                message.From = new MailAddress(WebConfigurationManager.AppSettings["SenderEmailAddress"]);  // replace with valid value
                message.Subject = "Update your Password";
                message.Body = string.Format(body, emailModel.FromEmail, emailModel.Message);
                message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Host = WebConfigurationManager.AppSettings["EmailHost"];
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential();
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["EmailPort"]);
                        smtp.Send(message);
                        ViewBag.SuccessMsg = "Email sent.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMsg = ex.Message;
                        return View();
                    }

                }

                //ViewBag.SuccessMsg = "Email sent.";
            }
            else
            {
                ViewBag.ErrorMsg = "This Email does not exist on this system";
                return View();
            }
            return View();
        }


        [HttpGet]
        [Route("Reset_password/{Id}")]
        public ActionResult Reset_password(int Id)
        {
            ViewBag.Message = "Reset Password";
            ViewBag.SuccessMsg = TempData["ChangePassword"];
            int UserId = Id;
            UserManagement existingUser = new UserManagement();
            var userDetails = existingUser.modifyPassword(UserId);
            ViewBag.User = userDetails;
            return View();
        }

        [Route("Reset_password/{Id}")]
        public ActionResult Reset_password(UserLogin User, int? Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var editUser = new DolUser();
            editUser.UserId = Id.GetValueOrDefault();
            editUser.UserPWD = User.Password;
           
            if (User.Password.Any("!@#$%^&*".Contains) && User.Password.Length >= 6)
            {
                if (User.Password == User.ConfirmPassword)
                {
                    UserManagement userService = new UserManagement();
                    var ExtDetails = userService.getUserById(editUser.UserId);
                    editUser.UserPWD = userService.base64Encode(editUser.UserPWD);
                    if (ExtDetails.UserPWD != editUser.UserPWD)
                    {
                        try
                        {
                            editUser.UserId = Convert.ToInt32(Id);
                            //editUser.UserPWD = User.Password;
                            bool ValidatePassword = false;
                            ValidatePassword = userService.UpdatePassword(editUser.UserPWD, Id);
                            InsertAudit(Constants.AuditActionType.PasswordChanged, editUser.UserName + "Changed password", ExtDetails.UserName);
                            TempData["SuccessMsg"] = "Kindly login with the new password";
                            return RedirectToAction("login");

                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMsg = ex.Message;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "The new password must not match the old password";
                        return View();
                    }
                 
                }
                else
                {
                    ViewBag.ErrorMsg = "The confirm password must match the new password";
                    return View();
                }

            }
            else
            {
                ViewBag.ErrorMsg = "The password must contain special and minimum of six characters";
                return View();
            }
        }

    }
}




    
