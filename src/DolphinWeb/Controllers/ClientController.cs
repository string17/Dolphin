using DolphinWeb.Models;
using DolphinWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DolphinWeb.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppLogic _service;
        private readonly AuditTrail auditTrail;
        private readonly SecurityLogic securityLogic;
        private readonly FileLogic file;
        private static string ipaddress = new AuditTrail().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditTrail().DetermineCompName(ipaddress);

        public ClientController()
        {
            _service = new AppLogic();
            auditTrail = new AuditTrail();
            securityLogic = new SecurityLogic();
            file = new FileLogic();
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
        public ActionResult NewClient(ClientData param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var client = new ClientObj();
                client.ClientAlias = param.ClientAlias;
                client.ClientBanner = file.UploadBanner(param.ClientBanner);
                client.ClientName = param.ClientName;
                client.CreatedBy = User.Identity.Name;
                client.CreatedOn = DateTime.Now;
                client.IsClientActive = param.IsClientActive;
                client.RespTime = param.RespTime;
                client.RestTime = param.RestTime;
                client.RespTimeUp = param.RespTimeUp;
                client.RestTimeUp = param.RestTimeUp;
                client.SystemIp = ipaddress;
                client.Computername = ComputerDetails;
                var success = _service.InsertClient(client);
                if (success.RespCode.Equals("00"))
                {
                    TempData["SuccessMsg"] = "Record successfully added";
                    return RedirectToAction("listclient");
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
            return View();
        }

        public ActionResult ListClient()
        {
            ViewBag.Message = "Clients";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            ViewBag.Clients = _service.GetAllClient();
            return View();
        }


        [HttpGet]
        [Route("Modifyclient/{ClientId}")]
        public ActionResult ModifyClient(int ClientId)
        {
            ViewBag.Message = "Client";
            ViewBag.Client = _service.GetClientDetails(ClientId);
            return View();
        }

        [Route("Modifyclient/{ClientId}")]
        public ActionResult ModifyClient(ClientData param, int ClientId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "Client";
            var banner = new FileLogic().UploadBanner(param.ClientBanner, param.ClientBanner1);
            var client = new ClientObj();
            client.ClientId = ClientId;
            client.ClientAlias = param.ClientAlias;
            client.ClientBanner = banner;
            client.ClientName = param.ClientName;
            client.CreatedBy = User.Identity.Name;
            client.CreatedOn = DateTime.Now;
            client.IsClientActive = param.IsClientActive;
            client.RespTime = param.RespTime;
            client.RestTime = param.RestTime;
            client.RespTimeUp = param.RespTimeUp;
            client.RestTimeUp = param.RestTimeUp;
            client.UserName = User.Identity.Name;
            client.SystemIp = ipaddress;
            client.Computername = ComputerDetails;
            var success = _service.ModifyClient(client);
            if (success!=null)
            {
                TempData["SuccessMsg"] = "Record successfully updated";
                return RedirectToAction("listclient");
            }
            else
            {
                ViewBag.ErrorMsg = "Unable to create record";
            }
            return View();
        }
    }
}