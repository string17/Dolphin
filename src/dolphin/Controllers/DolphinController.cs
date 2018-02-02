using BLL.ApplicationLogic;
using DataAccessLayer.CustomObjects;
using DolphinContext.Data.Models;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace dolphin.Controllers
{
    public class DolphinController : BaseController
    {
        // GET: Dolphin
        [Authorize]
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Dashboard";
            return View();
        }

        public ActionResult Create_User()
        {
            ViewBag.Message = "New User";
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            Company company = new Company();
            ViewBag.Customer = company.getCompanyById();
            return View();
        }

        [HttpPost]
        public ActionResult Create_User(UserViewModel Account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Message = "New User";
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            Company company = new Company();
            ViewBag.Customer = company.getCompanyById();
            if (Account.UserPWD.Any("!@#$%^&*".Contains) && Account.UserPWD.Length >= 6)
            {

                try
                {
                    var Acc = new DolUser();
                    Acc.FirstName = Account.FirstName;
                    Acc.MiddleName = Account.MiddleName;
                    Acc.LastName = Account.LastName;
                    Acc.UserName = Account.UserName.ToUpper();
                    Acc.UserPWD = Account.UserPWD;
                    Acc.PhoneNos = Account.PhoneNos;
                    Acc.CustomerId = Account.CustomerId;
                    Acc.RoleId = Account.RoleId;
                    Acc.UserStatus = Account.UserStatus;
                    Acc.CreatedBy = User.Identity.Name;
                    Acc.CreatedOn = DateTime.Now;

                    UserManagement usermgt = new UserManagement();
                    Acc.UserPWD = usermgt.base64Encode(Account.UserPWD);
                    Acc.UserImg = usermgt.DoFileUpload(Account.UserImg);
                    var validUser = usermgt.getUserByUsername(Account.UserName);
                    if (validUser != null && validUser.UserName == Account.UserName)
                    {
                        ViewBag.ErrorMsg = "User already exists";
                        return View();
                    }
                    else
                    {
                        bool NewUser = false;
                        NewUser = usermgt.InsertUser(Acc);
                        if (NewUser == true)
                        {
                            ViewBag.SuccessMsg = "User successfully created";
                            InsertAudit(BLL.ApplicationLogic.Constants.AuditActionType.CreatedRole, "Created a user " + Acc.UserName, User.Identity.Name);
                            return View();
                            //return RedirectToAction("View_User", "Dolphin");

                        }
                        else
                        {
                            ViewBag.ErrorMsg = "Unable to create the user's account";
                            return View();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                    return View();

                }
            }
            else
            {
                ViewBag.ErrorMsg = "The password must contain special and minimum of six characters";
                return View();
            }
            
        }

        public ActionResult Create_Role(DolRole Account)
        {

            ViewBag.Message = "Role";
            try
            {
                //Account.RoleStatus = false;
                Account.RoleName = Account.RoleName.ToUpper();
                Account.RoleDesc = Account.RoleDesc;

                if (Account.RoleStatus != null)
                {
                    Account.RoleStatus = true;
                }
                else
                {
                    Account.RoleStatus = false;
                }
                Account.RoleStatus = Account.RoleStatus;

                RoleManagement roleManagement = new RoleManagement();
                var validRole = roleManagement.getRoleByRoleName(Account.RoleName);
                if (validRole != null && validRole.RoleName == Account.RoleName)
                {
                    ViewBag.ErrorMsg = "Role Already Exists";
                    return View();
                }
                else
                {
                    bool NewRole = false;
                    NewRole = roleManagement.InsertRole(Account);
                    if (NewRole == true)
                    {
                        ViewBag.SuccessMsg = "Role created successfully";
                        InsertAudit(Constants.AuditActionType.CreatedRole, "Created a role " + Account.RoleName, User.Identity.Name);
                        return RedirectToAction("View_role", "Dolphin");

                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Unable to connect to the database";
                        return View();
                    }
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult Create_Menu(DolMenu account)
        {
            ViewBag.Message = "Menu";
            //MenuManagement menuMgt = new MenuManagement();
            //ViewBag.Menus = menuMgt.getMenuByMenuId();
            try
            {
                account.MenuName = account.MenuName;
                account.MenuURL = account.MenuURL;
                account.MenuDesc = account.MenuDesc;
                account.ParentId = account.ParentId;
                account.Sequence = account.Sequence;
                account.ExternalURL = account.ExternalURL;

                if (account.MenuStatus != null)
                {
                    account.MenuStatus = true;
                }
                else
                {
                    account.MenuStatus = false;
                }
                account.LinkIcon = account.LinkIcon;
                MenuManagement MM = new MenuManagement();
                var ExtMenu = MM.getMenuByName(account.MenuName);
                if(ExtMenu!=null && ExtMenu.MenuName == account.MenuName)
                {

                }
                return View();


            }
            catch(Exception)
            {
                return View();

            }
            
        }   

        public ActionResult Edit_Profile()
        {
            ViewBag.Message = "Edit Profile";
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            UserManagement existingUser = new UserManagement();
            var user = existingUser.getUserByUsername(User.Identity.Name);
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public ActionResult Edit_Profile(UserViewModel account, FormCollection c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            UserManagement usermgt = new UserManagement();
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            string Username = User.Identity.Name;
            var editUser = new DolUser();
            if (account.UserPWD.Any("!@#$%^&*".Contains) && account.UserPWD.Length >= 6)
            {
                try
                {
                                     
                    editUser.FirstName = account.FirstName;
                    editUser.MiddleName = account.MiddleName;
                    editUser.LastName = account.LastName;
                    editUser.UserName = account.UserName.ToUpper();
                    editUser.UserPWD = account.UserPWD;
                    editUser.PhoneNos = account.PhoneNos;
                    editUser.ModifiedBy = User.Identity.Name;
                    editUser.ModifiedOn = DateTime.Now;
                    editUser.UserImg = usermgt.DoFileUpload(account.UserImg,c["UserImg1"]);
                    UserManagement userManagement = new UserManagement();
                    editUser.UserPWD = usermgt.base64Encode(account.UserPWD);
                    bool UpdateUser = userManagement.UpdateProfile(editUser.FirstName, editUser.MiddleName, editUser.LastName, editUser.UserName, editUser.UserPWD, editUser.UserImg,editUser.PhoneNos,User.Identity.Name, DateTime.Now, User.Identity.Name);
                    InsertAudit(Constants.AuditActionType.ProfileModified, "Modified a user " + editUser.UserName, User.Identity.Name);
                    TempData["ProfileMsg"] = "Profile updated successfully. Please Kindly login";
                    return RedirectToAction("Logout", "Dolphin");
                    //ViewBag.SuccessMsg = "Account successfully modified";
                 
                }
                catch (Exception)
                {
                    ViewBag.ErrorMsg = "Unable to connect to the server";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMsg = "The password must contain special and minimum of six characters";
                return View();
            }
        }


        public ActionResult Generate_Invoice()
        {
            ViewBag.Message = "Invoice";
            
            return View();
        }
        public ActionResult View_User()
        {
            ViewBag.Message = "Users";
            TempData["SuccessMsg"] = TempData["SuccessMsg"];
            UserManagement Usermgts = new UserManagement();
            ViewBag.Users = Usermgts.getUserByCompany();
            return View();
        }

        [HttpGet]
        [Route("view_user/{Id}")]
        public ActionResult View_User(UserView User)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            UserManagement Usermgts = new UserManagement();
            ViewBag.Users = Usermgts.getUserByCompany();
            return View();
        }

        public ActionResult View_Terminal()
        {
            ViewBag.Message = "Terminals";
            TerminalManagement Machine = new TerminalManagement();
            ViewBag.terminal = Machine.getAllTerminal();
            return View();
        }

        public ActionResult Existing_Terminal()
        {
            ViewBag.Message = "Terminals";
            TerminalManagement Machine = new TerminalManagement();
            ViewBag.terminal = Machine.getAllTerminal();
            return View();
        }

        [HttpGet]
        [Route("Edit_Terminal/{Id}")]
        public ActionResult Edit_Terminal(int? Id)
        {
            ViewBag.Message = "Terminal";
            int Tid = Id.GetValueOrDefault();
            Region Regt = new Region();
            ViewBag.region = Regt.getRegionById();
            Brand brand = new Brand();
            ViewBag.Brand = brand.getBrandById();
            UserManagement Engineer = new UserManagement();
            ViewBag.engineer = Engineer.getUserById();
            Company dolCompany = new Company();
            ViewBag.custom = dolCompany.getCompanyById();
            //TerminalManagement trmMgt = new TerminalManagement();
            //ViewBag.Terminals = trmMgt.getTerminalById();
            //var terminal = trmMgt.
            TerminalManagement terminal = new TerminalManagement();
            var ExistTerm = terminal.getTerminalId(Tid);
            
            ViewBag.TRM = ExistTerm;
            return View();
        }

        [Route("Edit_Terminal/{Id}")]
        public ActionResult Edit_Terminal(Terminal term,int Id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int Tid = Id;
            Region Regt = new Region();
            ViewBag.region = Regt.getRegionById();
            Brand brand = new Brand();
            ViewBag.Brand = brand.getBrandById();
            UserManagement Engineer = new UserManagement();
            ViewBag.engineer = Engineer.getUserById();
            Company dolCompany = new Company();
            ViewBag.custom = dolCompany.getCompanyById();
            TerminalManagement trmMgt = new TerminalManagement();
            //ViewBag.Terminals = trmMgt.getTerminalById();
            var terminal = trmMgt.getTerminalId(Tid);
            ViewBag.TRM = terminal;
            try
            {
                var editTerm = new DolTerminal();
                editTerm.Tid = Convert.ToInt32(Id);
                editTerm.CustomerName = term.CustomerName;
                editTerm.TerminalId = term.TerminalId;
                editTerm.TSNum = term.TSNum;
                editTerm.TerminalRef = term.TerminalRef;
                editTerm.BrandName = term.BrandName;
                editTerm.TLocation = term.TLocation;
                editTerm.TAlias = term.TAlias;
                editTerm.RegionName = term.RegionName;
                editTerm.TEng = User.Identity.Name;
                editTerm.TerminalStatus = term.TerminalStatus;
                editTerm.ModifiedBy = User.Identity.Name;
                editTerm.ModifiedOn = DateTime.Now;
                TerminalManagement terminalManagement = new TerminalManagement();
                //editUser.UserPWD = userManagement.base64Encode(extUser.UserPWD);
                bool UpdateMach = false;
                UpdateMach = terminalManagement.UpdateTerminal(editTerm.CustomerName, editTerm.TerminalId, editTerm.TSNum, editTerm.TerminalRef, editTerm.BrandName, editTerm.TLocation, editTerm.TAlias, editTerm.RegionName, editTerm.TEng,editTerm.TerminalStatus, User.Identity.Name, DateTime.Now, Id);
                InsertAudit(Constants.AuditActionType.TerminalModified, "Modified a Terminal " + editTerm.ModifiedBy, User.Identity.Name);
                ViewBag.SuccessMsg = "Terminal details successfully modified";
                return View();
            }
            catch (Exception)
            {
                ViewBag.ErrorMsg = "Unable to connect to the server";
                return View();
            }
        }
      
        public ActionResult Create_Institution()
        {
            ViewBag.Message = "Institution";
            return View();
        }

        [HttpPost]
        public ActionResult Create_Institution(CompanyView customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var customers = new DolCompany();
                customers.CustomerName = customer.CustomerName.ToUpper();
                customers.CustomerAlias = customer.CustomerAlias;
                customers.CustomerRespTime = customer.CustomerRespTime;
                customers.CustomerRestTime = customer.CustomerRestTime;
                customers.CustomerRest1Time = customer.CustomerRest1Time;

                if (customers.CustomerStatus != null)
                {
                    customers.CustomerStatus = true;
                }
                else
                {
                    customers.CustomerStatus = false;
                }
                customers.CustomerStatus = customer.CustomerStatus;
                customers.CreatedOn = DateTime.Now;
                Company client = new Company();
                customers.CustomerBanner = client.DoFileUpload(customer.CustomerBanner);
                var OldClient = client.getCompanyByName(customers.CustomerName);
                if (OldClient != null)
                {
                    ViewBag.ErrorMsg = "Customer already exists";
                    return View();
                }
                else
                {
                    bool newClient = false;
                    newClient = client.InsertCompany(customers);
                    if (newClient == true)
                    {
                        InsertAudit(Constants.AuditActionType.CustomerCreated, "Created an Institution " + customers.CustomerName, User.Identity.Name);
                        return RedirectToAction("view_institution", "Dolphin");
                        //ViewBag.SuccessMsg = "Account successfully created";
                        //return View();
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Unable to create the account";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;

            }

            return View();
        }

        public ActionResult View_Institution()
        {
            ViewBag.Message = "Institution";
            ViewBag.SuccessMsg = TempData["SuccessMsg"];
            Company institution = new Company();
            ViewBag.customer = institution.getCompanyById();
            return View();
        }

        [HttpGet]
        [Route("Edit_Institution/{Id}")]
        public ActionResult Edit_Institution(int? Id)
        {
            ViewBag.Message = "Institution";
            int CustomerId = Id.GetValueOrDefault();
            Company ext = new Company();
            var inst = ext.getCompanyId(CustomerId);
            ViewBag.Inst = inst;
            return View();
        }

        [Route("Edit_Institution/{Id}")]
        public ActionResult Edit_Institution(CompanyViewModel extCompany, int Id, FormCollection c)
        {
            int CustomerId = Id;
            Company ext = new Company();
            var inst = ext.getCompanyId(CustomerId);
            ViewBag.Inst = inst;
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var extClient = new DolCompany();
                extClient.CustomerId = Convert.ToInt32(Id);
                extClient.CustomerName = extCompany.CustomerName;
                extClient.CustomerAlias = extCompany.CustomerAlias;
                extClient.CustomerStatus = extCompany.CustomerStatus;
                extClient.CustomerRespTime = extCompany.CustomerRespTime;
                extClient.CustomerRestTime = extCompany.CustomerRestTime;
                extClient.CustomerRest1Time = extCompany.CustomerRest1Time;
                Company instManagement = new Company();
                extClient.CustomerBanner = instManagement.DoFileUpload(extCompany.CustomerBanner, c["CustomerBanner1"]);
                bool UpdateRole = false;
                UpdateRole = instManagement.UpdateCompany(extClient.CustomerName, extClient.CustomerAlias, extClient.CustomerBanner, extClient.CustomerStatus, extClient.CustomerRespTime, extClient.CustomerRestTime, extClient.CustomerRest1Time, extClient.CustomerId);
                InsertAudit(Constants.AuditActionType.CustomerProfileModified, "Modified a Company's profile" + extClient.CustomerName, User.Identity.Name);
                TempData["SuccessMsg"] = "Profile successfully modified";
                return RedirectToAction("View_institution");
            }
            catch (Exception)
            {
                ViewBag.ErrorMsg = "Unable to connect to the server";
                return View();
            }
        }

        public ActionResult Call()
        {
            ViewBag.Message = "Incident";
            return View();
        }

        public ActionResult Upload_Terminal()
        {
            ViewBag.Message = "Upload";
            TerminalManagement newTerminal = new TerminalManagement();
            var result = newTerminal.getTerminalById();
            //ViewBag.newUpload = newTerminal.getStudentByMatricNo();
            UserManagement userService = new UserManagement();
            var userDetails = userService.getUserByUsername(User.Identity.Name);

            Company company = new Company();
            ViewBag.Customer = company.getCompanyById();
            return View();
        }

        //[HttpPost]
        //public ActionResult Upload_Term(HttpPostedFileBase TerminalDoc, DolCompany customer)
        //{
        //    ViewBag.Message = "Upload";
        //    UserManagement userService = new UserManagement();
        //    var userDetails = userService.getUserByUsername(User.Identity.Name);
        //    if (ModelState.IsValid)
        //    {
        //        var context = DolServiceDb.GetInstance();
        //        TerminalManagement newTerminal = new TerminalManagement();
        //        var files = System.Web.HttpContext.Current.Request.Files["TerminalDoc"];
        //        var supportedTypes = new[] { "xlsx" };
        //        var fileExt = System.IO.Path.GetExtension(files.FileName).Substring(1);
        //        if (!supportedTypes.Contains(fileExt))
        //        {
        //            ViewBag.ErrorMsg = "File Extension Is InValid - Only Upload Excel format";
        //            return View();
        //        }

        //        else
        //        {
        //            string result1 = DateTime.Now.Millisecond + files.FileName;
        //            string Filepath = "~/TerminalFile/" + result1;
        //            int rowCount = 0;
        //            int CustomerId = userDetails.CustomerId.GetValueOrDefault();
        //            string CustomerId = customer.CustomerName;
        //            List<DolTerminal> newDet = new List<DolTerminal>();

        //            XSSFWorkbook workBook = new XSSFWorkbook(files.InputStream);
        //            var worksheet = workBook.GetSheetAt(0);
        //            rowCount = worksheet.PhysicalNumberOfRows;
        //            for (int i = 1; i < rowCount; i++)
        //            {
        //                var row = worksheet.GetRow(i);
        //                string TerminaId = row.Cells[1].ToString();
        //                if (!string.IsNullOrWhiteSpace(TerminaId))
        //                {
        //                    newDet.Add(new DolTerminal()
        //                    {
        //                        CustomerName = CustomerName,
        //                        TerminalId = (string.IsNullOrEmpty(row.Cells[0].ToString()) ? string.Empty : row.Cells[1].ToString().ToUpper()),
        //                        TSNum = (string.IsNullOrEmpty(row.Cells[1].ToString()) ? string.Empty : row.Cells[2].ToString().ToUpper()),
        //                        TerminalRef = (string.IsNullOrEmpty(row.Cells[3].ToString()) ? string.Empty : row.Cells[3].ToString().ToUpper()),
        //                        BrandName = (string.IsNullOrEmpty(row.Cells[4].ToString()) ? string.Empty : row.Cells[5].ToString().ToUpper()),
        //                        TLocation = (string.IsNullOrEmpty(row.Cells[5].ToString()) ? string.Empty : row.Cells[5].ToString().ToUpper()),
        //                        TAlias = (string.IsNullOrEmpty(row.Cells[6].ToString()) ? string.Empty : row.Cells[6].ToString().ToUpper()),
        //                        RegionName = (string.IsNullOrEmpty(row.Cells[7].ToString()) ? string.Empty : row.Cells[7].ToString().ToUpper()),
        //                        TEng = "support@altavizsupport.com".ToString().ToUpper(),
        //                        TerminalStatus = Convert.ToBoolean(string.IsNullOrEmpty(row.Cells[9].ToString()) ? string.Empty : row.Cells[9].ToString()),
        //                        CreatedBy = User.Identity.Name,
        //                        CreatedOn = DateTime.Now,
        //                        ModifiedBy = User.Identity.Name,
        //                        ModifiedOn = DateTime.Now
        //                    });
        //                }
        //            }

        //            context.InsertBulk(newDet);
        //            ViewBag.SuccessMsg = "Record upload successfully";
        //            ModelState.Clear();
        //            return View();
        //        }

        //    }
        //    return View();
        //}

        //public ActionResult Upload_Terminal()
        //{
        //    ViewBag.Message = "Upload Terminal";
        //    return View();
        //}

        [HttpPost]
        public ActionResult Upload_Terminal(HttpPostedFileBase TerminalDoc)
        {
            ViewBag.Message = "Upload Terminal";
            UserManagement usermgt = new UserManagement();
            var merchantDetails = usermgt.getUserByUsername(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var context = DolServiceDb.GetInstance();
                TerminalManagement newTerminal = new TerminalManagement();
                var files = System.Web.HttpContext.Current.Request.Files["TerminalDoc"];
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
                    string Filepath = "~/TerminalFile/" + result1;
                    int rowCount = 0;
                    int Mid = merchantDetails.CustomerId.GetValueOrDefault();
                    List<DolTerminal> newDet = new List<DolTerminal>();

                    XSSFWorkbook workBook = new XSSFWorkbook(files.InputStream);
                    var worksheet = workBook.GetSheetAt(0);
                    rowCount = worksheet.PhysicalNumberOfRows;
                    for (int i = 1; i < rowCount; i++)
                    {
                        var row = worksheet.GetRow(i);
                        List<ICell> cells = new List<ICell> { };
                        var cellCount = row.PhysicalNumberOfCells;
                        var cellCount1 = row.LastCellNum;

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
                        if(two==null & five==null & six==null& seven==null & eight==null & nine==null & eleven==null)
                        {
                            ViewBag.ErrorMsg = "Kindly fill the empty fields";
                            return View();
                        }
                        var existingTerminal = newTerminal.getTerminalByTID(two.ToString());
                        if (existingTerminal == null)
                        {
                            try
                            {
                                var terminal = new DolTerminal();
                                terminal.CustomerName = one.ToString();
                                terminal.TerminalId = two.ToString();
                                terminal.TSNum = three.ToString();
                                terminal.TerminalRef = (row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                                terminal.BrandName = five.ToString();
                                terminal.TLocation = six.ToString();
                                terminal.TState = seven.ToString();
                                terminal.TAlias = eight.ToString();
                                terminal.RegionName = nine.ToString();
                                terminal.TEng = (row.GetCell(9, MissingCellPolicy.RETURN_NULL_AND_BLANK) == null ? string.Empty : row.GetCell(9, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                                terminal.TerminalStatus=eleven.ToString().ToUpper() == "ACTIVE" ? true: false;
                                terminal.CreatedBy = User.Identity.Name;
                                terminal.CreatedOn = DateTime.Now;
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
                    }
                    context.InsertBulk(newDet);
                    InsertAudit(Constants.AuditActionType.UploadUser, "Upload " + newDet.Count +"Terminals", User.Identity.Name);
                    ModelState.Clear();
                    ViewBag.SuccessMsg =newDet.Count + " Records upload successfully" ;
                    return View();

                }
                //  return View();
            }
            return View();
        }


        public ActionResult Upload_User()
        {
            ViewBag.Message = "Upload";
            //TerminalManagement newTerminal = new TerminalManagement();
            //var result = newTerminal.getTerminalById();
            ////ViewBag.newUpload = newTerminal.getStudentByMatricNo();
            //UserManagement userService = new UserManagement();
            //var userDetails = userService.getUserByUsername(User.Identity.Name);

            //Company company = new Company();
            //ViewBag.Customer = company.getCompanyById();
            return View();
        }


        [HttpPost]
        public ActionResult Upload_User(HttpPostedFileBase UserDoc)
        {
            ViewBag.Message = "Upload User Document";
            UserManagement usermgt = new UserManagement();
            var merchantDetails = usermgt.getUserByUsername(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var context = DolServiceDb.GetInstance();
                UserManagement newUser = new UserManagement();
                var files = System.Web.HttpContext.Current.Request.Files["UserDoc"];
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
                    string Filepath = "~/UserFile/" + result1;
                    int rowCount = 0;
                    int Mid = merchantDetails.CustomerId.GetValueOrDefault();
                    List<DolUser> newDet = new List<DolUser>();

                    XSSFWorkbook workBook = new XSSFWorkbook(files.InputStream);
                    var worksheet = workBook.GetSheetAt(0);
                    rowCount = worksheet.PhysicalNumberOfRows;
                    for (int i = 1; i < rowCount; i++)
                    {
                        var row = worksheet.GetRow(i);
                        List<ICell> cells = new List<ICell> { };
                        var cellCount = row.PhysicalNumberOfCells;
                        var cellCount1 = row.LastCellNum;

                        var one = row.GetCell(0, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var two = row.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var three = row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var four = row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK).ToString().ToUpper();
                        var five = row.GetCell(4, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var six = row.GetCell(5, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var seven = row.GetCell(6, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var eight = row.GetCell(7, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var nine = row.GetCell(8, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var ten = row.GetCell(9, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        var eleven = row.GetCell(10, MissingCellPolicy.RETURN_NULL_AND_BLANK);
                        if (one==null & four == null & five == null & six == null & seven == null & eight == null & nine == null & eleven==null )
                        {
                            ViewBag.ErrorMsg = "Kindly fill the empty fields";
                            return View();
                        }
                        var existingUser = newUser.getUserByUsername(four.ToString());
                        if (existingUser == null)
                        {
                            try
                            {
                                var user = new DolUser();
                                user.FirstName = one.ToString();
                                user.MiddleName = two+" ";
                                user.LastName = three+" ";
                                user.UserName = four;
                                user.PhoneNos = six.ToString();
                                user.UserPWD = five.ToString();
                                user.UserImg = "";
                                user.CustomerId = 6;
                                user.RoleId = 8;
                                user.UserStatus = eleven.ToString().ToUpper() == "ACTIVE" ? true:false;
                                user.CreatedBy = User.Identity.Name;
                                user.CreatedOn = DateTime.Now;
                                user.ModifiedBy = "";
                                user.ModifiedOn = DateTime.Now;
                                newDet.Add(user);

                            }
                            catch (Exception ex)
                            {
                                ViewBag.ErrorMsg = ex.Message;
                                return View();
                            }
                        }
                    }
                    context.InsertBulk(newDet);
                    InsertAudit(Constants.AuditActionType.UploadTerminal, "Upload " + newDet.Count + "Users", User.Identity.Name);
                    ModelState.Clear();
                    ViewBag.SuccessMsg = newDet.Count + " Records upload successfully";

                    return View();

                }
                //  return View();
            }
            return View();
        }


        public ActionResult Create_Terminal()
        {
            ViewBag.Message = "Upload";
            string roleName = ("Engineer").ToUpper();
            Region Regt = new Region();
            ViewBag.region = Regt.getRegionById();
            TerminalManagement tState = new TerminalManagement();
            ViewBag.state = tState.getAllState();
            Brand brand = new Brand();
            ViewBag.Brand = brand.getBrandById();
            UserManagement Engineer = new UserManagement();
            ViewBag.engineer = Engineer.getEngineer();
            Company dolCompany = new Company();
            ViewBag.custom = dolCompany.getCompanyById();
            return View();
        }

        [HttpPost]
        public ActionResult Create_Terminal(Terminal extTerminal)
        {
            Region Regt = new Region();
            ViewBag.region = Regt.getRegionById();
            Brand brand = new Brand();
            ViewBag.Brand = brand.getBrandById();
            UserManagement Engineer = new UserManagement();
            ViewBag.engineer = Engineer.getUserById();
            Company dolCompany = new Company();
            ViewBag.custom = dolCompany.getCompanyById();
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var newTerminal = new DolTerminal();
                newTerminal.CustomerName = extTerminal.CustomerName;
                newTerminal.TerminalId = extTerminal.TerminalId;
                newTerminal.TSNum = extTerminal.TSNum.ToUpper();
                newTerminal.TerminalRef = extTerminal.TerminalRef;
                newTerminal.BrandName = extTerminal.BrandName;
                newTerminal.TLocation = extTerminal.TLocation;
                newTerminal.TAlias = extTerminal.TAlias;
                newTerminal.RegionName = extTerminal.RegionName;
                newTerminal.TEng = extTerminal.TEng;
                newTerminal.CreatedBy = Engineer.getUserByUsername(User.Identity.Name).ToString();
                newTerminal.CreatedOn = DateTime.Now;
                TerminalManagement terminalMgt = new TerminalManagement();
                var ExistingTerminal = terminalMgt.getTerminalByTID(extTerminal.TerminalId);
                if(ExistingTerminal!=null && ExistingTerminal.TerminalId == extTerminal.TerminalId)
                {
                    ViewBag.ErrorMsg = "This machine already exists";
                    return View();
                }
                else
                {
                    bool createTerminal = false;
                    createTerminal = terminalMgt.InsertTerminal(newTerminal);
                    if (createTerminal == true)
                    {
                        InsertAudit(Constants.AuditActionType.TerminalAdded, "Modified a Company's profile" + extTerminal.TerminalId, User.Identity.Name);
                        ViewBag.SuccessMsg = "Terminal successfully added";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Unable to add the terminal";
                        return View();
                    }
                }
              }
            catch (Exception)
            {
                ViewBag.ErrorMsg = "Unable to connect to the server";
                return View();
            }
           
        }

        public ActionResult View_Menu()
        {
            ViewBag.Message = "Menu";
            MenuManagement Menumgts = new MenuManagement();
            ViewBag.Menus = Menumgts.getMenuById();
            return View();
        }
     
        public ActionResult View_Role()
        {
            ViewBag.Message = "Role";
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            return View();
        }
       
        public ActionResult Edit_Menu()
        {
            ViewBag.Message = "Menu";
            return View();
        }
        public ActionResult Menu_Role()
        {
            ViewBag.Message = "Mapping";
            return View();
        }

        public ActionResult View_Call()
        {
            ViewBag.Message = "Logged Call";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetAllIncidents();
            ViewBag.Incidents = extIncident;
            return View();
        }

        public ActionResult View_Incident()
        {
            ViewBag.Message = "Logged Call";
            IncidentManagement incidentMgt = new IncidentManagement();
            ViewBag.Incidents = incidentMgt.GetAllIncidents();
            Company company = new Company();
            ViewBag.Customer = company.getCompanyByGroup();
            return View();
        }

        public ActionResult Incident_History()
        {
            ViewBag.Message = "Incident History";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetAllIncidents();
            ViewBag.Incidents = extIncident;
            return View();
        }

        public ActionResult Pending_Call()
        {
            ViewBag.Message = "Logged Call";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetCustomerCall(User.Identity.Name);
            ViewBag.Incidents = extIncident;
            return View();
        }

        public ActionResult Call_History()
        {
            ViewBag.Message = "Logged Call";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetCustomerCall(User.Identity.Name);
            ViewBag.Incidents = extIncident;
            return View();
        }

        public ActionResult Update_Incident()
        {
            ViewBag.Message = "Logged Call";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetAllIncidents();
            ViewBag.Incidents = extIncident;
            return View();
        }

        public ActionResult View_Report()
        {
            ViewBag.Message = "Logged Call";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetAllIncidents();
            ViewBag.Incidents = extIncident;
            Company company = new Company();
            ViewBag.Customer = company.getCompanyByGroup();
            return View();
        }

        public ActionResult Gen_Report()
        {
            ViewBag.Message = "Report";
            IncidentManagement incidentMgt = new IncidentManagement();
            var extIncident = incidentMgt.GetAllIncidents();
            ViewBag.Incidents = extIncident;
            return View();
        }
        public ActionResult New_Call()
        {
            ViewBag.Message = "Log Call";
            return View();
        }

        [HttpGet]
        [Route("Edit_Role/{Id}")]
        public ActionResult Edit_Role(int? Id)
        {
            ViewBag.Message = "Role";
            int RoleId = Id.GetValueOrDefault();
            RoleManagement existingRole = new RoleManagement();
            var userRole = existingRole.getRoleById(RoleId);
            ViewBag.UserRole = userRole;
            return View();
        }
        
        [Route("Edit_Role/{Id}")]
        public ActionResult Edit_Role(RoleViewModel extRole, int Id)
        {
            int RoleId = Id;
            RoleManagement existingRole = new RoleManagement();
            var userRole = existingRole.getRoleById(RoleId);
            ViewBag.UserRole = userRole;
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
                {
                    var editRole = new DolRole();
                    editRole.RoleId = Convert.ToInt32(Id);
                    editRole.RoleName = extRole.RoleName;
                    editRole.RoleDesc = extRole.RoleDesc;
                    editRole.RoleStatus = extRole.RoleStatus;
                    RoleManagement roleManagement = new RoleManagement();
                    bool UpdateRole = false;
                    UpdateRole = roleManagement.UpdateRole(extRole.RoleName, extRole.RoleDesc, extRole.RoleStatus, editRole.RoleId);
                    InsertAudit(Constants.AuditActionType.RoleModified, "Modified a role" + editRole.RoleName, User.Identity.Name);
                    ViewBag.SuccessMsg = "Role successfully modified";
                    return RedirectToAction("View_role", "Dolphin");
                }
                catch(Exception)
            {
                ViewBag.ErrorMsg = "Unable to connect to the server";
                return View();
            }
          
        }

        [HttpGet]
        [Route("Edit_User/{Id}")]
        public ActionResult Edit_User(int? Id)
        {
            ViewBag.Message = "Edit User";
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            int UserId = Id.GetValueOrDefault();
            UserManagement existingUser = new UserManagement();
            var user = existingUser.getUserByIds(UserId);
            ViewBag.User = user;
            return View();
        }

        [Route("Edit_User/{Id}")]
        public ActionResult Edit_User(UserViewModel Account, int? Id, FormCollection c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ViewBag.Message = "Modify User";
            int UserId = Id.GetValueOrDefault();
            RoleManagement rolemgt = new RoleManagement();
            ViewBag.Roles = rolemgt.getRoleByRoleId();
            Company company = new Company();
            ViewBag.Customer = company.getCompanyById();
            if (Account.UserPWD.Any("!@#$%^&*".Contains) && Account.UserPWD.Length >= 6)
            {

                try
                {
                    var Acc = new DolUser();
                    Acc.FirstName = Account.FirstName;
                    Acc.MiddleName = Account.MiddleName;
                    Acc.LastName = Account.LastName;
                    Acc.UserName = Account.UserName.ToUpper();
                    Acc.UserPWD = Account.UserPWD;
                    Acc.PhoneNos = Account.PhoneNos;
                    Acc.RoleId = Account.RoleId;
                    Acc.UserStatus = Account.UserStatus;
                    Acc.ModifiedBy = User.Identity.Name;
                    Acc.ModifiedOn = DateTime.Now;
                    UserManagement usermgt = new UserManagement();
                    Acc.UserPWD = usermgt.base64Encode(Account.UserPWD);
                    Acc.UserImg = usermgt.DoFileUpload(Account.UserImg, c["UserImg1"]);
                    var updateUser = usermgt.UpdateUser(Acc.FirstName, Acc.MiddleName,Acc.LastName, Acc.UserName, Acc.UserPWD, Acc.UserImg, Acc.PhoneNos,Acc.RoleId,Acc.UserStatus, User.Identity.Name, DateTime.Now, UserId);
                    if (updateUser == true)
                    {
                        InsertAudit(Constants.AuditActionType.UserModified, "Modified a user " + Acc.UserName, User.Identity.Name);
                        TempData["SuccessMsg"] = "Account successfully modified";
                        return RedirectToAction("view_user");
                        //return View();
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Account modification failed";
                        return View();
                    }
                    
                    
                    
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMsg = ex.Message;
                    return View();

                }
            }
            else
            {
                ViewBag.ErrorMsg = "The password must contain special and minimum of six characters";
                return View();
            }

        }

        public ActionResult View_Audit()
        {
            AuditService auditService = new AuditService();
            ViewBag.Audit = auditService.getAuditById();
            return View();
        }

        public ActionResult Logout()
        {
            TempData["ProfileMsg"] = TempData["ProfileMsg"];
            InsertAudit(Constants.AuditActionType.Logout, "User signed out successfully",User.Identity.Name);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}