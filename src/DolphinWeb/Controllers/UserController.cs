using DolphinWeb.Models;
using DolphinWeb.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DolphinWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly AppLogic _service;
        private readonly SecurityLogic securityLogic;
        private readonly FileLogic file;
        private static string ipaddress = new AuditTrail().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditTrail().DetermineCompName(ipaddress);

        public UserController()
        {
            _service = new AppLogic();
            securityLogic = new SecurityLogic();
            file = new FileLogic();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            ViewBag.Message = "User Info";
            ViewBag.Clients = _service.GetAllClient();
            ViewBag.Roles = _service.GetAllRole();
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(UserDetails param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Clients = _service.GetAllClient();
            ViewBag.Roles = _service.GetAllRole();
            if (param.Password.Any("!@#$%^&*".Contains) && param.Password.Length >= 6)

            {
                try
                {
                    string UserImage = file.UploadImage(param.UserImg);
                    var user = new UserInfo();
                    user.FirstName = param.FirstName;
                    user.MiddleName = param.MiddleName;
                    user.LastName = param.LastName;
                    user.Email = param.Email;
                    user.UserName = param.UserName;
                    user.Password = securityLogic.EncryptPassword(param.Password);
                    user.PhoneNo = param.PhoneNo;
                    user.ClientId = param.ClientId;
                    user.RoleId = param.RoleId;
                    user.Sex = param.Sex;
                    user.UserImg = UserImage;
                    user.IsUserActive = param.IsUserActive;
                    user.CreatedBy = User.Identity.Name;
                    user.CreatedOn = DateTime.Now;
                    user.Computername = ComputerDetails;
                    user.SystemIp = ipaddress;
                    var success = _service.InsertUserRecord(user);
                    if (success.RespCode.Equals("00"))
                    {
                        TempData["SuccessMsg"] = success.RespMessage;
                        return RedirectToAction("listuser");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.RespMessage;
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "The password must contain special and minimum of six characters";
            }

            return View();
        }


        [HttpGet]
        [Route("ModifyUser/{UserId}")]
        public ActionResult ModifyUser(int UserId)
        {
            ViewBag.Message = "User Account";
            ViewBag.SuccessMsg=TempData["SuccessMsg"];
            ViewBag.UserDetails = _service.GetUserDetails(UserId);
            ViewBag.Clients = _service.GetAllClient();
            ViewBag.Roles = _service.GetAllRole();
            return View();
        }



        [Route("ModifyUser/{UserId}")]
        public ActionResult ModifyUser(UserDetails param, int UserId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ViewBag.Message = "User Account";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            //ViewBag.UserDetails = _service.GetUserDetails(UserId);
            ViewBag.Clients = _service.GetAllClient();
            ViewBag.Roles = _service.GetAllRole();

            if (param.Password.Any("!@#$%^&*".Contains) && param.Password.Length >= 6)
            {
                try
                {
                    string UserImage = file.UploadImage(param.UserImg, param.UserImg1);
                    var user = new UserInfo();
                    user.UserId = UserId;
                    user.FirstName = param.FirstName;
                    user.MiddleName = param.MiddleName;
                    user.LastName = param.LastName;
                    user.Email = param.Email;
                    user.UserName = param.UserName;
                    user.Password = securityLogic.EncryptPassword(param.Password);
                    user.PhoneNo = param.PhoneNo;
                    user.ClientId = param.ClientId;
                    user.Sex = param.Sex;
                    user.RoleId = param.RoleId;
                    user.UserImg = UserImage;
                    user.IsUserActive = param.IsUserActive;
                    user.CreatedBy = User.Identity.Name;
                    user.CreatedOn = DateTime.Now;
                    user.Computername = ComputerDetails;
                    user.SystemIp = ipaddress;
                    var success = _service.ModifyUserRecord(user);
                    if (success!=null)
                    {
                        if (success.RespCode.Equals("00"))
                        {
                            TempData["SuccessMsg"] = "Account successfully updated";
                            return RedirectToAction("listuser");
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
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "The password must contain special and minimum of six characters";
            }

            return View();
        }


        public ActionResult ListUser()
        {
            ViewBag.Message = "User Accounts";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.Users = _service.GetAllUser();
            return View();
        }


        public ActionResult BulkRecord()
        {
            ViewBag.Message = "User Accounts";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.Clients = _service.GetAllClient();
            ViewBag.Roles = _service.GetAllRole();
            return View();
        }

        [HttpPost]
        public ActionResult BulkRecord(UserDetails param)
        {
            ViewBag.Message = "Upload User";
            ViewBag.Clients = _service.GetAllClient();

            if (ModelState.IsValid)
            {
                var files = param.UserFile; //System.Web.HttpContext.Current.Request.Files["TerminalDoc"];
                var supportedTypes = new[] { "xlsx" };
                var fileExt = System.IO.Path.GetExtension(files.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ViewBag.ErrorMsg = "File Extension Is InValid - Only Upload Excel format";
                    return View();
                }

                else
                {
                    string result1 = DateTime.Now.Millisecond + files.FileName;
                    int rowCount = 0;
                    List<UserDetails> newDet = new List<UserDetails>();
                    XSSFWorkbook workBook = new XSSFWorkbook(files.InputStream);
                    var worksheet = workBook.GetSheetAt(0);
                    rowCount = worksheet.PhysicalNumberOfRows;
                    for (int i = 1; i < rowCount; i++)
                    {
                        var row = worksheet.GetRow(i);
                        var one = row.GetCell(0, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var two = row.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var three = row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var four = row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var five = row.GetCell(4, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var six = row.GetCell(5, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var seven = row.GetCell(6, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var eight = row.GetCell(7, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var nine = row.GetCell(8, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var ten = row.GetCell(9, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var eleven = row.GetCell(10, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        if (one ==null && two == null && four==null && five == null && six == null && seven == null && eight == null && ten == null && eleven == null)
                        {
                            ViewBag.ErrorMsg = "Kindly fill the empty fields";
                            return View();
                        }

                        try
                        {
                            var userDetails = new UserDetails();
                            userDetails.LastName = one.ToString();
                            userDetails.FirstName = two.ToString();
                            userDetails.MiddleName = (row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                            userDetails.UserName = one.ToString()+ i;
                            userDetails.Email = four.ToString();
                            userDetails.Password =  securityLogic.EncryptPassword(five.ToString());
                            userDetails.PhoneNo = six.ToString();
                            userDetails.Sex = seven.ToString();
                            userDetails.RoleName = eight.ToString();
                            userDetails.ClientName = ten.ToString();
                            userDetails.UserStatus = eleven.ToString();
                            userDetails.CreatedOn = DateTime.Now;
                            userDetails.CreatedBy = User.Identity.Name;
                            userDetails.ModifiedBy = "";
                            userDetails.ModifiedOn = DateTime.Now;
                            userDetails.SystemIp = ipaddress;
                            userDetails.Computername = ComputerDetails;
                            newDet.Add(userDetails);

                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMsg = ex.Message;
                            return View();
                        }

                    }
                    //context.InsertBulk(newDet);
                    var success=_service.UploadUserRecord(newDet);
                    if (success !=null)
                    {
                        if (success.RespCode.Equals("00"))
                        {
                            TempData["SuccessMsg"] = success.RespMessage;
                            return RedirectToAction("listuser");
                        }
                        else
                        {
                            ViewBag.ErrorMsg = success.RespMessage;
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMsg = " Unable to perform the specified operation";
                    }
                   
                }

                ModelState.Clear();
            }
            return View();
        }
    }
}