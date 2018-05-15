using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class UserManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        public UserInfo GetUserInfo(UserObj userLogin)
        {
            //var _password = EncryptPassword(userLogin.Password);
            string sql = "select A.*,B.*,C.Alias from Dol_User A inner join User_Role B on A.RoleId=B.RoleId inner join Dol_Company C on C.CompanyId=A.CompanyId where A.UserName=@0 and A.IsDelete='false' ";
            var _actual = _db.FirstOrDefault<UserInfo>(sql, userLogin.Username);
            return _actual;

        }

        private string EncryptPassword(string UserPWD)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(UserPWD);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    UserPWD = Convert.ToBase64String(ms.ToArray());
                }
            }
            return UserPWD;
        }

        private string DecryptPassword(string UserPWD)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(UserPWD);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    UserPWD = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return UserPWD;
        }


        //public DolUser getUserByUsername(string Username)
        //{
        //    string sql = "Select * from DolUser where UserName =@0";
        //    var actual = _db.FirstOrDefault<DolUser>(sql, Username.ToUpper());
        //    return actual;
        //}

        public DolUser GetUserByUsername(string Username, int? CompanyId)
        {
            string SQL = "Select * from DolUser where UserName =@0 AND CompanyId=@1";
            var actual = _db.FirstOrDefault<DolUser>(SQL, Username.ToUpper(), CompanyId);
            return actual;
        }

        public List<UserObj> GetUserByCompany()
        {
            string sql = "SELECT A.FirstName,A.MiddleName,A.LastName,A.UserName,A.PhoneNos,A.UserStatus,A.UserImg,A.RoleId,A.UserId,B.CustomerAlias FROM DolUser A INNER JOIN DolCompany B ON A.CompanyId = B.CompanyId ";
            var actual = _db.Fetch<UserObj>(sql);
            return actual;
        }

        public bool UpdatePassword(string Password, int? Id)
        {
            try
            {
                var _user = _db.SingleOrDefault<DolUser>("where UserId =@0", Id);
                _user.Password = Password;
                _db.Update(_user);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public DolUser ModifyPassword(int Id)
        {
            string sql = "Select * from DolUser where UserId =@0";
            var actual = _db.FirstOrDefault<DolUser>(sql, Id);
            return actual;

        }

        //public UserObj GetUserProfileByUsername(string Username)
        //{
        //    var actual = _db.SingleOrDefault<DolUser>("where UserName=@0", Username);
        //    var userCompany = _db.SingleOrDefault<DolCompany>("where CustomerId=@0", actual.Customerid);
        //    var userrRole = _db.SingleOrDefault<DolRole>("where RoleId=@0", actual.Roleid);
        //    Company company = new Company();
        //    UserObj userView = new UserObj()
        //    {
        //        CustomerId = actual.CustomerId,
        //        FirstName = actual.FirstName,
        //        MiddleName = actual.MiddleName,
        //        LastName = actual.LastName,
        //        UserName = actual.UserName,
        //        UserPWD = actual.UserPWD,
        //        RoleId = actual.RoleId,
        //        PhoneNos = actual.PhoneNos,
        //        CustomerAlias = userCompany.CustomerAlias,
        //        UserImg = actual.UserImg,
        //        RoleName = userrRole.RoleName,
        //        UserStatus = actual.UserStatus,
        //        CreatedBy = actual.CreatedBy,
        //        CreatedOn = actual.CreatedOn

        //    };

        //    return userView;
        //}

        public UserInfo GetUserById(int UserId)
        {
            var _actual = _db.SingleOrDefault<DolUser>("where UserId=@0", UserId);
            var _company = _db.SingleOrDefault<DolClient>("where ClientId=@0", _actual.Clientid);
            var _role = _db.SingleOrDefault<UserRole>("Where RoleId=@0", _actual.Roleid);
            UserInfo _userObj = new UserInfo()
            {
                ClientId= _actual.Clientid,
                FirstName = _actual.Firstname,
                MiddleName = _actual.Middlename,
                LastName = _actual.Lastname,
                UserName = _actual.Username,
                Password = _actual.Password,
                RoleId = _actual.Roleid,
                PhoneNo = _actual.Phoneno,
                Alias = _company.Clientalias,
                RoleName=_role.Title,
                UserImg = _actual.Userimg

            };

            return _userObj;
        }

        //public EngineerViewModel getEngineerTerminal(string roleName)
        //{
        //    var term = _db.SingleOrDefault<DolRole>("where RoleName=@0", roleName);
        //    var actual = _db.SingleOrDefault<DolUser>("Where RoleId=@0", term.RoleId);
        //    var termNum = _db.ExecuteScalar<int>("select Count(*) from DolTerminal where UserId =@0", actual.UserId);
        //    EngineerViewModel engineerView = new EngineerViewModel()
        //    {
        //        FirstName = actual.FirstName,
        //        MiddleName = actual.MiddleName,
        //        LastName = actual.LastName,
        //        UserName = actual.UserName,
        //        UserPWD = actual.UserPWD,
        //        RoleId = actual.RoleId,
        //        PhoneNos = actual.PhoneNos,
        //        RoleName = term.RoleName,
        //        //UserImg = actual.UserImg

        //    };
        //    return engineerView;
        //}

        public List<DolUser> GetEngineer()
        {
            string sql = "select A.*,B.RoleName,B.RoleStatus from DolUser A INNER JOIN DolRole B ON A.RoleId=B.RoleId where A.UserStatus='true' AND B.RoleName='ENGINEER' ";
            var actual = _db.Fetch<DolUser>(sql);
            return actual;
        }
        public List<DolUser> getAllUsers()
        {
            var actual = _db.Fetch<DolUser>();
            return actual;
        }

        public List<DolUser> GetUserById()
        {
            var actual = _db.Fetch<DolUser>();
            return actual;
        }

        public List<DolMenuItem> GetMenuByUsername(string Username)
        {
            try
            {
                string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
                var actual = _db.Fetch<DolMenuItem>(SQL, Username);
                return actual;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DoesUsernameExists(string Username)
        {
            var rslt = _db.SingleOrDefault<DolUser>("where UserName=@0", Username); //.Where(a => a.Username == Username);
            if (rslt == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool DoesPasswordExists(string Username, string Password)
        {
            string _password = EncryptPassword(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1", Username, _password);
            if (rslt.Password == _password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserActive(string Username, string Password)
        {
            string _password = EncryptPassword(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1" , Username, _password);
            if (rslt.Isuseractive==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCompanyActive(string username)
        {
            string sql = "select A.*,B.* from Dol_User A inner join Dol_Client B on B.ClientId=A.ClientId inner join User_Role C on C.RoleId=A.RoleId where A.UserName=@0";
            var _actual = _db.SingleOrDefault<UserInfo>(sql, username);
            if (_actual.IsClientActive)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public bool InsertUser(UserDetails userDetails)
        {
            try
            {
                var user = new DolUser();
                user.Firstname = userDetails.FirstName;
                user.Middlename = userDetails.MiddleName;
                user.Username = userDetails.UserName;
                user.Email = userDetails.Email;
                user.Password = userDetails.Password;
                user.Phoneno = userDetails.PhoneNo;
                user.Roleid = userDetails.RoleId;
                user.Clientid = userDetails.ClientId;
                user.Userimg = DoFileUpload(userDetails.UserImg);
                user.Isuseractive = userDetails.IsUserActive;
                user.Isdelete = userDetails.IsDelete;
                user.Createdby = userDetails.CreatedBy;
                user.Createdon = userDetails.CreatedOn;
                _db.Insert(userDetails);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

 

        public int GetFreshUser(string Username)
        {
            string sql = "Select COUNT(*) from AuditTrail where UserName = @0";
            var _actual = _db.ExecuteScalar<int>(sql, Username);
            return Convert.ToInt32(_actual);
        }

        public DolUser GetUserById(decimal UserId)
        {
            var actual = _db.SingleById<DolUser>(UserId);
            return actual;
        }



        public bool ModifyUserDetails(UserDetails userDetails)
        {
            try
            {
                var _users = _db.SingleOrDefault<DolUser>("WHERE UserId=@0", userDetails.UserId);
                _users.Firstname = userDetails.FirstName;
                _users.Middlename = userDetails.MiddleName;
                _users.Lastname = userDetails.LastName;
                _users.Username = userDetails.UserName;
                _users.Password = EncryptPassword(userDetails.Password);
                _users.Userimg = DoFileUpload(userDetails.UserImg);
                _users.Phoneno = userDetails.PhoneNo;
                _users.Roleid = userDetails.RoleId;
                _users.Isuseractive = userDetails.Status;
                _users.Modifiedby = userDetails.ModifiedBy;
                _users.Modifiedon = userDetails.ModifiedOn;
                _db.Update(_users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateProfile(UserDetails userDetails)
        {
            try
            {
                var _user = _db.SingleOrDefault<DolUser>("WHERE UserName=@0", userDetails.UserName);
                _user.Firstname = userDetails.FirstName;
                _user.Middlename = userDetails.MiddleName;
                _user.Lastname = userDetails.LastName;
                _user.Userimg = DoFileUpload(userDetails.UserImg);
                _user.Username = userDetails.UserName;
                _user.Password = EncryptPassword(userDetails.Password);
                _user.Phoneno = userDetails.PhoneNo;
                _user.Modifiedby = userDetails.ModifiedBy;
                _user.Modifiedon = userDetails.ModifiedOn;
                _db.Update(_user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string DoFileUpload(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "UserPics.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/Content/UserImg/") + result);
            return result;
        }
    }
}
