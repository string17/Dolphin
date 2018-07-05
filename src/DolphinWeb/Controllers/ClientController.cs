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
    public class ClientController : BaseController
    {
        private readonly Services _dolphinApi;
        private readonly AuditService _auditService;
        private readonly EncodingCharacters _encodingService;
        private readonly UploadAttachment _uploadFile;
        private static string ipaddress = new AuditService().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditService().DetermineCompName(ipaddress);

        public ClientController()
        {
            _dolphinApi = new Services();
            _auditService = new AuditService();
            _encodingService = new EncodingCharacters();
            _uploadFile = new UploadAttachment();
        }
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult NewClient()
        {
            ViewBag.Message = "Client Account";
            return View();
        }



        [HttpPost]
        public ActionResult NewClient(ClientRequest param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var client = new ClientRequest();
                client.ClientAlias = param.ClientAlias;
                client.ClientBanner = _uploadFile.UploadBanner(param.BannerFile);
                client.ClientName = param.ClientName;
                client.CreatedBy = User.Identity.Name;
                client.CreatedOn = DateTime.Now;
                client.IsClientActive = param.IsClientActive;
                client.RespTimeIn = param.RespTimeIn;
                client.RestTimeIn = param.RestTimeIn;
                client.RespTimeOut = param.RespTimeOut;
                client.RestTimeOut = param.RestTimeOut;
                client.SystemIp = ipaddress;
                client.Computername = ComputerDetails;
                var success = _dolphinApi.InsertClient(client);
                if (success.ResponseCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = success.ResponseMessage;
                    return RedirectToAction("listclient");
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
            return View();
        }



        public ActionResult ListClient()
        {
            ViewBag.Message = "Clients";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.Clients = _dolphinApi.GetAllClient();
            return View();
        }



        [HttpGet]
        [Route("Modifyclient/{ClientId}")]
        public ActionResult ModifyClient(int ClientId)
        {
            ViewBag.Message = "Client";
            ViewBag.Client = _dolphinApi.GetClientDetails(ClientId);
            return View();
        }



        [Route("Modifyclient/{ClientId}")]
        public ActionResult ModifyClient(ClientRequest param, int ClientId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "Client";
            var banner = _uploadFile.UploadBanner(param.BannerFile, param.ClientBanner);
            var client = new ClientRequest();
            client.ClientId = ClientId;
            client.ClientAlias = param.ClientAlias;
            client.ClientBanner = banner;
            client.ClientName = param.ClientName;
            client.CreatedBy = User.Identity.Name;
            client.CreatedOn = DateTime.Now;
            client.IsClientActive = param.IsClientActive;
            client.RespTimeIn = param.RespTimeIn;
            client.RestTimeIn = param.RestTimeIn;
            client.RespTimeOut = param.RespTimeOut;
            client.RestTimeOut = param.RestTimeOut;
            client.CreatedBy = User.Identity.Name;
            client.SystemIp = ipaddress;
            client.Computername = ComputerDetails;
            var success = _dolphinApi.ModifyClient(client);
            if (success!=null)
            {
                if (success.ResponseCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = success.ResponseMessage;
                    return RedirectToAction("listclient");
                }
                else
                {
                    ViewBag.ErrorMsg = success.ResponseMessage;
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Unable to create record";
            }
            return View();
        }
    }
}