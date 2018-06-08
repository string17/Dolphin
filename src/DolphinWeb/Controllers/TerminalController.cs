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
    public class TerminalController : Controller
    {
        private readonly AppLogic _service;
        private readonly SecurityLogic securityLogic;
        private readonly FileLogic file;
        private static string ipaddress = new AuditTrail().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditTrail().DetermineCompName(ipaddress);

        public TerminalController()
        {
            _service = new AppLogic();
            securityLogic = new SecurityLogic();
            file = new FileLogic();
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
            ViewBag.Clients = _service.GetOnlyClients();
            ViewBag.States = _service.GetAllStates();
            ViewBag.Engineers = _service.GetAllEngineers();
            ViewBag.Brands = _service.GetAllBrand();
            return View();
        }

        [HttpPost]
        public ActionResult NewTerminal(TerminalObj param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "Terminal";
            ViewBag.Clients = _service.GetOnlyClients();
            ViewBag.States = _service.GetAllStates();
            ViewBag.Engineers = _service.GetAllEngineers();
            ViewBag.Brands = _service.GetAllBrand();
            try
            {
                var terminal = new TerminalObj();
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
                var success = _service.InsertTerminal(terminal);
                if (success != null)
                {
                    if (success.RespCode.Equals("00"))
                    {
                        TempData["SuccessMsg"] = success.RespMessage;
                        return RedirectToAction("listterminal");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.RespMessage;
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
            ViewBag.Clients = _service.GetAllClient();
            return View();

        }


        [HttpPost]
        public ActionResult UploadTerminal(TerminalData param)
        {
            ViewBag.Message = "Upload Terminal";
            ViewBag.Clients = _service.GetAllClient();

            if (ModelState.IsValid)
            {
                var files = param.TerminalFile; //System.Web.HttpContext.Current.Request.Files["TerminalDoc"];
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
                    List<TerminalData> newDet = new List<TerminalData>();
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
                        if (two == null && five == null && six == null && seven == null && eight == null && nine == null && eleven == null)
                        {
                            ViewBag.ErrorMsg = "Kindly fill the empty fields";
                            return View();
                        }

                        try
                        {
                            var terminal = new TerminalData();
                            terminal.TerminalRef = (row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                            terminal.SerialNo = three.ToString();
                            terminal.RegionName = nine.ToString();
                            terminal.TerminalNo = two.ToString();
                            //terminal.ClientName = one.ToString();
                            terminal.BrandName = five.ToString();
                            terminal.Engineer = eight.ToString();
                            terminal.Location = six.ToString();
                            terminal.TerminalAlias = seven.ToString();
                            terminal.State = eight.ToString();
                            terminal.CreatedOn = DateTime.Now;
                            terminal.CreatedBy = User.Identity.Name;
                            terminal.ModifiedBy = "";
                            terminal.ModifiedOn = DateTime.Now;
                            newDet.Add(terminal);

                        }
                        catch (Exception ex)
                        {
                            ViewBag.ErrorMsg = ex.Message;
                            return View();
                        }

                    }
                    //context.InsertBulk(newDet);
                    _service.UploadTerminal(newDet);
                    ModelState.Clear();
                    ViewBag.SuccessMsg = newDet.Count + " Records upload successfully";
                    return View();

                }

            }
            return View();
        }

        // GET: Terminal
        [HttpGet]
        [Route("ModifyTerminal/{TerminalNo}")]
        public ActionResult ModifyTerminal(string TerminalNo)
        {
            ViewBag.Message = "Terminal";
            ViewBag.Clients = _service.GetOnlyClients();
            ViewBag.States = _service.GetAllStates();
            ViewBag.Engineers = _service.GetAllEngineers();
            ViewBag.Brands = _service.GetAllBrand();
            ViewBag.Terminal = _service.GetTerminalDetails(TerminalNo, ComputerDetails, ipaddress);
            return View();
        }


        [Route("ModifyTerminal/{TerminalRef}")]
        public ActionResult ModifyTerminal(TerminalObj param, string TerminalRef)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "Terminal";
            ViewBag.Clients = _service.GetOnlyClients();
            ViewBag.States = _service.GetAllStates();
            ViewBag.Engineers = _service.GetAllEngineers();
            ViewBag.Brands = _service.GetAllBrand();
            try
            {
                var terminal = new TerminalObj();
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
                var success = _service.ModifyTerminal(terminal);
                if (success != null)
                {
                    if (success.RespCode.Equals("00"))
                    {
                        TempData["SuccessMsg"] = success.RespMessage;
                        return RedirectToAction("listterminal");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = success.RespMessage;
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
            ViewBag.Terminals = _service.GetAllTerminal();
            return View();
        }
    }
}