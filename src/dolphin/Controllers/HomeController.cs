using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dolphin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Dashboard()
        {
            ViewBag.Message = "Dashboard";
            return View();
        }


        public ActionResult Create_User()
        {
            ViewBag.Message = "New User";
            return View();
        }

        public ActionResult Edit_User()
        {
            ViewBag.Message = "Edit User";
            return View();
        }

        public ActionResult Edit_Profile()
        {
            ViewBag.Message = "Edit Profile";
            return View();
        }


        public ActionResult View_User()
        {
            ViewBag.Message = "Users";
            return View();
        }

        public ActionResult View_Terminal()
        {
            ViewBag.Message = "Terminals";
            return View();
        }

        public ActionResult Call()
        {
            ViewBag.Message = "Users";
            return View();
        }

        public ActionResult Upload_Terminal()
        {
            ViewBag.Message = "Upload";
            return View();
        }

        public ActionResult Create_Menu()
        {
            ViewBag.Message = "Menu";
            return View();
        }

        public ActionResult Create_Menuitem()
        {
            ViewBag.Message = "Item";
            return View();
        }

        public ActionResult Create_Role()
        {
            ViewBag.Message = "Role";
            return View();
        }

        public ActionResult View_Role()
        {
            ViewBag.Message = "Role";
            return View();
        }

        
    }
}