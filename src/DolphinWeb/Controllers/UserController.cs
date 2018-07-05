using DolphinServices.ApplicationLogic;
using DolphinServices.Infrastructure;
using DolphinServices.Request;
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
        private readonly Services _dolphinApi;
        private readonly AuditService _auditService;
        private readonly EncodingCharacters _encodingService;
        private readonly UploadAttachment _uploadFile;
        private static string ipaddress = new AuditService().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditService().DetermineCompName(ipaddress);

        public UserController()
        {
            _dolphinApi = new Services();
            _auditService = new AuditService();
            _encodingService = new EncodingCharacters();
            _uploadFile = new UploadAttachment();
        }


        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            ViewBag.Message = "User Info";
            ViewBag.Clients = _dolphinApi.GetAllClient();
            ViewBag.Roles = _dolphinApi.GetAllRole();
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(UserDetailsRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Clients = _dolphinApi.GetAllClient();
            ViewBag.Roles = _dolphinApi.GetAllRole();
            if (param.Password.Any("!@#$%^&*".Contains) && param.Password.Length >= 6)

            {
                try
                {
                    string UserImage = _uploadFile.UploadImage(param.ImgFile);
                    var request = new UserDetailsRequest();
                    request.FirstName = param.FirstName.ToUpper();
                    request.MiddleName = param.MiddleName.ToUpper();
                    request.LastName = param.LastName.ToUpper();
                    request.Email = param.Email.ToUpper();
                    request.UserName = param.UserName.ToUpper();
                    request.Password = _encodingService.EncryptCharacter(param.Password);
                    request.PhoneNo = param.PhoneNo;
                    request.ClientId = param.ClientId;
                    request.RoleId = param.RoleId;
                    request.Sex = param.Sex.ToUpper();
                    request.UserImg = UserImage;
                    request.IsUserActive = param.IsUserActive;
                    request.CreatedBy = User.Identity.Name.ToUpper();
                    request.CreatedOn = DateTime.Now;
                    request.Computername = ComputerDetails;
                    request.SystemIp = ipaddress;
                    var success = _dolphinApi.InsertUserRecord(request);
                    if (success.ResponseCode.Equals("00"))
                    {
                        TempData["SuccessMsg"] = success.ResponseMessage;
                        return RedirectToAction("listuser");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.ResponseMessage;
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
            ViewBag.UserDetails = _dolphinApi.GetUserDetails(UserId);
            ViewBag.Clients = _dolphinApi.GetAllClient();
            ViewBag.Roles = _dolphinApi.GetAllRole();
            return View();
        }



        [Route("ModifyUser/{UserId}")]
        public ActionResult ModifyUser(UserDetailsRequest param, int UserId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ViewBag.Message = "User Account";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.UserDetails = _dolphinApi.GetUserDetails(UserId);
            ViewBag.Clients = _dolphinApi.GetAllClient();
            ViewBag.Roles = _dolphinApi.GetAllRole();

            if (param.Password.Any("!@#$%^&*".Contains) && param.Password.Length >= 6)
            {
                try
                {
                    string UserImage = _uploadFile.UploadImage(param.ImgFile, param.UserImg1);
                    var request = new UserDetailsRequest();
                    request.UserId = UserId;
                    request.FirstName = param.FirstName.ToUpper();
                    request.MiddleName = param.MiddleName.ToUpper();
                    request.LastName = param.LastName.ToUpper();
                    request.Email = param.Email.ToUpper();
                    request.UserName = param.UserName.ToUpper();
                    request.Password = _encodingService.EncryptCharacter(param.Password);
                    request.PhoneNo = param.PhoneNo;
                    request.ClientId = param.ClientId;
                    request.Sex = param.Sex.ToUpper();
                    request.RoleId = param.RoleId;
                    request.UserImg = UserImage;
                    request.IsUserActive = param.IsUserActive;
                    request.CreatedBy = User.Identity.Name.ToUpper();
                    request.CreatedOn = DateTime.Now;
                    request.Computername = ComputerDetails;
                    request.SystemIp = ipaddress;
                    var success = _dolphinApi.ModifyUserRecord(request);
                    if (success!=null)
                    {
                        if (success.ResponseCode.Equals("00"))
                        {
                            TempData["SuccessMsg"] = "Account successfully updated";
                            return RedirectToAction("listuser");
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
            ViewBag.Users = _dolphinApi.GetAllUser();
            return View();
        }


        public ActionResult BulkRecord()
        {
            ViewBag.Message = "User Accounts";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.Clients = _dolphinApi.GetAllClient();
            ViewBag.Roles = _dolphinApi.GetAllRole();
            return View();
        }

        [HttpPost]
        public ActionResult BulkRecord(HttpFileCollectionBase UserRecord)
        {
            ViewBag.Message = "Upload User";
            ViewBag.Clients = _dolphinApi.GetAllClient();

            if (ModelState.IsValid)
            {
                var files =System.Web.HttpContext.Current.Request.Files["UserRecord"];
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
                    List<UserDetailsBulkRequest> newDet = new List<UserDetailsBulkRequest>();
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
                            var request = new UserDetailsBulkRequest();
                            request.LastName = one.ToString();
                            request.FirstName = two.ToString();
                            request.MiddleName = (row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                            request.UserName = one.ToString()+ i;
                            request.Email = four.ToString();
                            request.Password =  _encodingService.EncryptCharacter(five.ToString());
                            request.PhoneNo = six.ToString();
                            request.Sex = seven.ToString();
                            request.RoleName = eight.ToString();
                            request.ClientName = ten.ToString();
                            request.UserStatus = eleven.ToString();
                            request.CreatedOn = DateTime.Now;
                            request.CreatedBy = User.Identity.Name;
                            request.ModifiedBy = "";
                            request.ModifiedOn = DateTime.Now;
                            request.SystemIp = ipaddress;
                            request.Computername = ComputerDetails;
                            newDet.Add(request);

                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMsg = ex.Message;
                            return View();
                        }

                    }
                    //context.InsertBulk(newDet);
                    var success=_dolphinApi.UploadUserRecord(newDet);
                    if (success !=null)
                    {
                        if (success.ResponseCode.Equals("00"))
                        {
                            TempData["SuccessMsg"] = success.ResponseMessage;
                            return RedirectToAction("listuser");
                        }
                        else
                        {
                            ViewBag.ErrorMsg = success.ResponseMessage;
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

        public ActionResult GetUserDetails(int UserId)
        {
            var user = _dolphinApi.GetUserDetails(UserId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}