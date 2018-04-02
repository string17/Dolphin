using DataAccessLayer.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class UserManagement:BaseService
    {
        private readonly DolServiceDb context = DolServiceDb.GetInstance();
        public DolUser getUserByUsername(string Username)
        {
            string SQL = "Select * from DolUser where UserName =@0";
            var actual = context.FirstOrDefault<DolUser>(SQL, Username.ToUpper());
            return actual;
        }

        public DolUser getUserByUsernames(string Username, int? CustomerId)
        {
            string SQL = "Select * from DolUser where UserName =@0 AND CustomerId=@1";
            var actual = context.FirstOrDefault<DolUser>(SQL, Username.ToUpper(),CustomerId);
            return actual;
        }

        public List<UserView> getUserByCompany()
        {
            string sql = "SELECT A.FirstName,A.MiddleName,A.LastName,A.UserName,A.PhoneNos,A.UserStatus,A.UserImg,A.RoleId,A.UserId,B.CustomerAlias FROM DolUser A INNER JOIN DolCompany B ON A.CustomerId = B.CustomerId ";
            var actual = context.Fetch<UserView>(sql).ToList();
            return actual;
        }

        public bool UpdatePassword(string Password, int? Id)
        {
            try
            {
                var users = context.SingleOrDefault<DolUser>("where UserId =@0", Id);
                users.UserPWD = Password;
                context.Update(users);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DolUser modifyPassword(int Id)
        {
            string sql = "Select * from DolUser where UserId =@0";
            var actual = context.FirstOrDefault<DolUser>(sql, Id);
            return actual;

        }

        public UserProfileView getUserProfileByUsername(string Username)
        {
            var actual = context.SingleOrDefault<DolUser>("where UserName=@0", Username);
            var userCompany = context.SingleOrDefault<DolCompany>("where CustomerId=@0", actual.CustomerId);
            var userrRole = context.SingleOrDefault<DolRole>("where RoleId=@0", actual.RoleId);
            Company company = new Company();
            UserProfileView userView = new UserProfileView()
            {
                CustomerId = actual.CustomerId,
                FirstName = actual.FirstName,
                MiddleName = actual.MiddleName,
                LastName = actual.LastName,
                UserName = actual.UserName,
                UserPWD = actual.UserPWD,
                RoleId = actual.RoleId,
                PhoneNos = actual.PhoneNos,
                CustomerAlias = userCompany.CustomerAlias,
                UserImg = actual.UserImg,
                RoleName=userrRole.RoleName,
                UserStatus=actual.UserStatus,
                CreatedBy=actual.CreatedBy,
                CreatedOn=actual.CreatedOn

            };

            return userView;
        }

        public UserView getUserByIds(int UserId)
        {
            var actual = context.SingleOrDefault<DolUser>("where UserId=@0", UserId);
            var userCompany = context.SingleOrDefault<DolCompany>("where CustomerId=@0", actual.CustomerId);
            Company company = new Company();
            UserView userView = new UserView()
            {
                CustomerId = actual.CustomerId,
                FirstName = actual.FirstName,
                MiddleName = actual.MiddleName,
                LastName=actual.LastName,
                UserName = actual.UserName,
                UserPWD = actual.UserPWD,
                RoleId = actual.RoleId,
                PhoneNos=actual.PhoneNos,
                CustomerAlias = userCompany.CustomerAlias,
                UserImg = actual.UserImg

            };
            
            return userView;
        }

        public EngineerViewModel getEngineerTerminal(string roleName)
        {
            var term = context.SingleOrDefault<DolRole>("where RoleName=@0",roleName);
            var actual= context.SingleOrDefault<DolUser>("Where RoleId=@0",term.RoleId);
            var termNum = context.ExecuteScalar<int>("select Count(*) from DolTerminal where UserId =@0", actual.UserId);
            EngineerViewModel engineerView = new EngineerViewModel()
            {
                FirstName = actual.FirstName,
                MiddleName = actual.MiddleName,
                LastName = actual.LastName,
                UserName = actual.UserName,
                UserPWD = actual.UserPWD,
                RoleId = actual.RoleId,
                PhoneNos = actual.PhoneNos,
                RoleName = term.RoleName,
                //UserImg = actual.UserImg

            };
            return engineerView;
        }

        public List<DolUser> getEngineer()
        {
            string sql= "select A.*,B.RoleName,B.RoleStatus from DolUser A INNER JOIN DolRole B ON A.RoleId=B.RoleId where A.UserStatus='true' AND B.RoleName='ENGINEER' ";
            var actual = context.Fetch<DolUser>(sql).ToList();
            return actual;
        }
        public List<DolUser> getAllUsers()
        {
            var actual = context.Fetch<DolUser>();
            return actual;
        }

        public List<DolUser> getUserById()
        {
            var actual = context.Fetch<DolUser>().ToList();
            return actual;
        }

        public List<DolMenu> getMenuByUsername(string Username)
        {
            try
            {
                string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
                var actual = context.Fetch<DolMenu>(SQL, Username).ToList();
                return actual;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DoesUsernameExists(string Username)
        {
            var rslt = context.Fetch<DolUser>().Where(a => a.UserName == Username).FirstOrDefault();
            if (rslt == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string base64Encode(string UserPWD) //Encode
        {
            try
            {
                byte[] encData_byte = new byte[UserPWD.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(UserPWD);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string base64Decode(string UserPWD) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(UserPWD);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
    
        public bool InsertUser(DolUser Username)
        {
            try
            {
                context.Insert(Username);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool DoesPasswordExists(string Username, string Password)
        {
            var rslt = context.Fetch<DolUser>().Where(a => a.UserName == Username).FirstOrDefault();
            if (rslt.UserPWD == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DolphinLoginResponse verifyUser(DolphinLoginRequest userLogin)
        {
            var rslt = context.Fetch<DolUser>().Where(a => a.UserName == userLogin.Username).FirstOrDefault();
            if (rslt.UserPWD == userLogin.Password)
            {
                return new DolphinLoginResponse { RespCode = "00", RespMessage = "Successful" };
            }
            else
            {
                return new DolphinLoginResponse { RespCode = "02", RespMessage = "Failed" };
            }
        }

        public int getFreshUser(string Username)
        {
            string sql = "Select COUNT(*) from AuditTrail where UserName = @0";
            var actual = context.ExecuteScalar<int>(sql, Username);
            return Convert.ToInt32(actual);
        }

        public DolUser getUserById(decimal UserId)
        {
            var actual = context.SingleById<DolUser>(UserId);
            return actual;
        }  

        

        public bool UpdateUser(string FirstName,string MiddleName, string LastName,string UserName,string UserPWD, string UserImg, string PhoneNos,int? RoleId,bool? UserStatus,string ModifiedBy,DateTime ModifiedOn,int? UserId)
        {
            try
            {
                var users = context.SingleOrDefault<DolUser>("WHERE UserId=@0", UserId);
                users.FirstName = FirstName;
                users.MiddleName = MiddleName;
                users.LastName = LastName;
                
                users.UserName = UserName;
                users.UserPWD = UserPWD;
                users.UserImg = UserImg;
                users.PhoneNos = PhoneNos;
                users.RoleId = RoleId;
                users.UserStatus = UserStatus;
                users.ModifiedBy = ModifiedBy;
                users.ModifiedOn = ModifiedOn;
                context.Update(users);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool UpdateProfile(string FirstName, string MiddleName,string LastName, string UserName, string UserPWD, string UserImg,string PhoneNos, string ModifiedBy, DateTime ModifiedOn,string Username)
        {
            try
            {
                var users = context.SingleOrDefault<DolUser>("WHERE UserName=@0", Username);
                users.FirstName = FirstName;
                users.MiddleName = MiddleName;
                users.LastName = LastName;
                users.UserImg = UserImg;
                users.UserName = UserName;
                users.UserPWD = UserPWD;
                users.PhoneNos = PhoneNos;
                //users.RoleId = RoleId;
                //users.UserStatus = UserStatus;
                users.ModifiedBy = ModifiedBy;
                users.ModifiedOn = ModifiedOn;
                context.Update(users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string DoFileUpload(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "UserPics.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/Images/") + result);
            return result;
        }


    }
}