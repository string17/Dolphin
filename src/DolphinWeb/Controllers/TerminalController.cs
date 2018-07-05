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
    public class TerminalController : BaseController
    {
        private readonly Services _dolphinApi;
        private readonly AuditService _auditService;
        private readonly EncodingCharacters _encodingService;
        private readonly UploadAttachment _uploadFile;
        private static string ipaddress = new AuditService().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditService().DetermineCompName(ipaddress);

        public TerminalController()
        {
            _dolphinApi = new Services();
            _auditService = new AuditService();
            _encodingService = new EncodingCharacters();
            _uploadFile = new UploadAttachment();
        }
        // GET: Terminal
        public ActionResult Index()
        {
            return View();
        }

        // GET: Terminal
        public ActionResult NewTerminal()
        {
            ViewBag.Message = "Terminal";
            string RoleName = "Engineer";
            ViewBag.Clients = _dolphinApi.GetOnlyClients();
            ViewBag.States = _dolphinApi.GetAllStates();
            ViewBag.Engineers = _dolphinApi.GetAllUserByRole(RoleName);
            ViewBag.Brands = _dolphinApi.GetAllBrand();
            return View();
        }

        [HttpPost]
        public ActionResult NewTerminal(TerminalRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "Terminal";
            string RoleName = "Engineer";
            ViewBag.Clients = _dolphinApi.GetOnlyClients();
            ViewBag.States = _dolphinApi.GetAllStates();
            ViewBag.Engineers = _dolphinApi.GetAllUserByRole(RoleName);
            ViewBag.Brands = _dolphinApi.GetAllBrand();
            try
            {
                var request = new TerminalRequest();
                request.BrandId = param.BrandId;
                request.TerminalId = param.TerminalId;
                request.TerminalRef = param.TerminalRef;
                request.TerminalNo = param.TerminalNo;
                request.SerialNo = param.SerialNo;
                request.StateId = param.StateId;
                request.ClientId = param.ClientId;
                request.Engineer = param.Engineer;
                request.IsUnderSupport = param.IsUnderSupport;
                request.IsTerminalActive = param.IsTerminalActive;
                request.Location = param.Location;
                request.TerminalAlias = param.TerminalAlias;
                request.CreatedBy = User.Identity.Name;
                request.CreatedOn = DateTime.Now;
                request.ModifiedBy = param.ModifiedBy;
                request.ModifiedOn = param.ModifiedOn;
                request.SystemIp = ipaddress;
                request.Computername = ComputerDetails;
                var success = _dolphinApi.InsertTerminal(request);
                if (success != null)
                {
                    if (success.ResponseCode.Equals("00"))
                    {
                        TempData["SuccessMsg"] = success.ResponseMessage;
                        return RedirectToAction("listterminal");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.ResponseMessage;
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "Unable to complete operation";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex;
            }

            return View();
        }


        // GET: Terminal
        public ActionResult UploadTerminal()
        {
            ViewBag.Message = "Terminal";
            return View();
        }


        [HttpPost]
        public ActionResult UploadTerminal(HttpPostedFileBase TerminalFile)
        {
            ViewBag.Message = "Upload Terminal";

            if (ModelState.IsValid)
            {
                var files = System.Web.HttpContext.Current.Request.Files["TerminalFile"];
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
                    //string Filepath = "~/TerminalFile/" + result1;
                    int rowCount = 0;
                    List<TerminalBulkRequest> _data = new List<TerminalBulkRequest>();
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
                        if (three == null && five == null && six == null && seven == null && eight == null && nine == null && eleven == null)
                        {
                            ViewBag.ErrorMsg = "Kindly fill the empty fields";
                            return View();
                        }

                        try
                        {
                            var request = new TerminalBulkRequest();
                            request.TerminalRef = (row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                            request.SerialNo = three.ToString(); //(row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                            request.RegionName = ten.ToString();
                            request.TerminalNo = two.ToString();
                            request.ClientName = one.ToString();
                            request.BrandName = five.ToString();
                            request.Engineer = eleven.ToString();
                            request.Location = seven.ToString();
                            request.TerminalAlias = eight.ToString();
                            request.Support = six.ToString();
                            request.State = nine.ToString();
                            request.CreatedOn = DateTime.Now;
                            request.CreatedBy = User.Identity.Name;
                            request.ModifiedBy = "";
                            request.ModifiedOn = DateTime.Now;
                            _data.Add(request);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMsg = ex.Message;
                            return View();
                        }

                    }
   
                    var success=_dolphinApi.UploadTerminal(_data);
                    if (success != null)
                    {
                        if (success.ResponseCode.Equals("00"))
                        {
                            TempData["SuccessMsg"] = success.ResponseMessage;
                            return RedirectToAction("listterminal");
                        }
                        else
                        {
                            ViewBag.ErrorMsg = success.ResponseMessage;
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Unable to complete operation at the moment";
                    }
              
                    //ViewBag.SuccessMsg = ;
                    //return View();

                }
                ModelState.Clear();
            }
            return View();
        }


        // GET: Terminal
        [HttpGet]
        [Route("ModifyTerminal/{TerminalId}")]
        public ActionResult ModifyTerminal(int TerminalId)
        {
            ViewBag.Message = "Terminal";
            string RoleName = "Engineer";
            var request = new TerminalRequest();
            request.TerminalId = TerminalId;
            request.Computername = ComputerDetails;
            request.SystemIp = ipaddress;
            ViewBag.Terminal = _dolphinApi.GetTerminalDetails(request);
            ViewBag.Clients = _dolphinApi.GetOnlyClients();
            ViewBag.States = _dolphinApi.GetAllStates();
            ViewBag.Engineers = _dolphinApi.GetAllUserByRole(RoleName);
            ViewBag.Brands = _dolphinApi.GetAllBrand();
            return View();
        }


        [Route("ModifyTerminal/{TerminalId}")]
        public ActionResult ModifyTerminal(TerminalRequest param, string TerminalId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "Terminal";
            string RoleName = "Engineer";
            ViewBag.Clients = _dolphinApi.GetOnlyClients();
            ViewBag.States = _dolphinApi.GetAllStates();
            ViewBag.Engineers = _dolphinApi.GetAllUserByRole(RoleName);
            ViewBag.Brands = _dolphinApi.GetAllBrand();
            try
            {
                var terminal = new TerminalRequest();
                terminal.BrandId = param.BrandId;
                terminal.TerminalId = param.TerminalId;
                terminal.TerminalRef = param.TerminalRef;
                terminal.TerminalNo = param.TerminalNo;
                terminal.SerialNo = param.SerialNo;
                terminal.StateId = param.StateId;
                terminal.ClientId = param.ClientId;
                terminal.Engineer = param.Engineer;
                terminal.IsUnderSupport = param.IsUnderSupport;
                terminal.IsTerminalActive = param.IsTerminalActive;
                terminal.Location = param.Location;
                terminal.TerminalAlias = param.TerminalAlias;
                terminal.CreatedBy = User.Identity.Name;
                terminal.CreatedOn = DateTime.Now;
                terminal.ModifiedBy = param.ModifiedBy;
                terminal.ModifiedOn = param.ModifiedOn;
                terminal.SystemIp = ipaddress;
                terminal.Computername = ComputerDetails;
                var success = _dolphinApi.ModifyTerminal(terminal);
                if (success != null)
                {
                    if (success.ResponseCode.Equals("00"))
                    {
                        TempData["SuccessMsg"] = success.ResponseMessage;
                        return RedirectToAction("listterminal");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.ResponseMessage;
                    }
                }
                else
                {
                    ViewBag.ErrorMsg = "Unable to complete operation";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex;
            }

            return View();

        }

        // GET: Terminal
        public ActionResult ListTerminal()
        {
            ViewBag.Message = "Terminals";
            ViewBag.Terminals = _dolphinApi.GetAllTerminal();
            return View();
        }


        public ActionResult GetTerminalDetails(int TerminalId)
        {
            var request = new TerminalRequest();
            request.TerminalId = TerminalId;
            request.Computername = ComputerDetails;
            request.SystemIp = ipaddress;
            var terminal = _dolphinApi.GetTerminalDetails(request);
            return Json(terminal, JsonRequestBehavior.AllowGet);
        }
    }
}