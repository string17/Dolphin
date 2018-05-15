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
        private static string ipaddress = new AuditTrail().DetermineIPAddress();
        private readonly string ComputerDetails = new AuditTrail().DetermineCompName(ipaddress);

        public ClientController()
        {
            _service = new AppLogic();
            auditTrail = new AuditTrail();
            securityLogic = new SecurityLogic();
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
        public ActionResult NewClient(ClientObj param)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = new ClientObj();
            client.ClientAlias = param.ClientAlias;
            client.ClientBanner = param.ClientBanner;
            client.ClientName = param.ClientName;
            client.CreatedBy = User.Identity.Name;
            client.CreatedOn = DateTime.Now;
            client.IsClientActive = param.IsClientActive;
            client.RespTime = param.RespTime;
            client.RestTime = param.RestTime;
            client.SystemIp = ipaddress;
            client.Computername = ComputerDetails;
            bool success = _service.InsertClient(client);
            if (success)
            {
                ViewBag.SuccessMsg = "Record successfully created";
            }
            else
            {
                ViewBag.ErrorMsg = "Unable to create record";
            }
            return View();
        }

        public ActionResult ListClient()
        {
            ViewBag.Message = "Clients";
            ViewBag.Clients = _service.GetAllClient();
            return View();
        }


        [HttpGet]
        [Route("Modifyclient/{ClientId}")]
        public ActionResult ModifyClient(int ClientId)
        {
            ViewBag.Client = _service.GetClientDetails(ClientId);
            return View();
        }

        [Route("Modifyclient/{ClientId}")]
        public ActionResult ModifyClient(ClientObj param, int ClientId, FormCollection c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
       
            var client = new ClientObj();
            client.ClientId = ClientId;
            client.ClientAlias = param.ClientAlias;
            client.ClientBanner = param.ClientBanner;
            client.ExtClientBanner = c["ClientBanner1"].ToString();
            client.ClientName = param.ClientName;
            client.CreatedBy = User.Identity.Name;
            client.CreatedOn = DateTime.Now;
            client.IsClientActive = param.IsClientActive;
            client.RespTime = param.RespTime;
            client.RestTime = param.RestTime;
            client.SystemIp = ipaddress;
            client.Computername = ComputerDetails;
            bool success = _service.ModifyClient(client);
            if (success)
            {
                ViewBag.SuccessMsg = "Record successfully created";
            }
            else
            {
                ViewBag.ErrorMsg = "Unable to create record";
            }
            return View();
        }
    }
}