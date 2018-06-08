using DolphinWeb.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace DolphinWeb.Services
{
    public class AppLogic
    {
        private readonly string _baseUrl= WebConfigurationManager.AppSettings["BaseUrl"];

        public AppResp ValidateUser(string UserName,string Password,string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "login");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("Username", UserName);
            request.AddParameter("Password", Password);
            request.AddParameter("Computername", Computername);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }  
        }


        public UserInfo GetUserDetails(int UserId)
        {
            string url = string.Concat(_baseUrl, "userdetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("UserId", UserId);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            UserInfo resp = JsonConvert.DeserializeObject<UserInfo>(response.Content);
            if (resp == null || resp.RespCode == null)
                return null;

            if (resp.RespCode.Equals("00"))
            {
                return resp;
            }
            return null;
        }


        public UserInfo GetUserInfo(string Username, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "userinfo");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddParameter("application/json", ParameterType.RequestBody);
            request.AddParameter( "Username", Username);
            request.AddParameter("Computername", Computername);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response == null)
                return null;

            UserInfo resp = JsonConvert.DeserializeObject<UserInfo>(response.Content);
            if (resp == null || resp.RespCode == null)
                return null;
           
            if (resp.RespCode.Equals("00"))
            {
                return resp;
            }
            return null;
        }


        //Get User Menu
        public List<UserMenu> GetMenu(string Username, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "Menu");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("Username", Username);
            request.AddParameter("Computername", Computername);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<UserMenu> resp = JsonConvert.DeserializeObject<List<UserMenu>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }

        }


        public List<RoleObj> GetAllRole()
        {
            string url = string.Concat(_baseUrl, "allrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            List<RoleObj> resp = JsonConvert.DeserializeObject<List<RoleObj>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }

        }


        public List<BrandObj> GetAllBrand()
        {
            string url = string.Concat(_baseUrl, "allbrands");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<BrandObj> resp = JsonConvert.DeserializeObject<List<BrandObj>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }

        }


        public AppResp InsertBrand(BrandObj param)
        {
            string url = string.Concat(_baseUrl, "insertbrand");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddObject(param);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp.RespCode==null || resp == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }

        public bool ModifyBrand(BrandObj param)
        {
            string url = string.Concat(_baseUrl, "modifybrand");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddObject(param);
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp.RespCode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<ClientResp> GetAllClient()
        {
            string url = string.Concat(_baseUrl, "allclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            List<ClientResp> resp = JsonConvert.DeserializeObject<List<ClientResp>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }

        }


        public List<ClientResp> GetOnlyClients()
        {
            string url = string.Concat(_baseUrl, "onlyclients");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<ClientResp> resp = JsonConvert.DeserializeObject<List<ClientResp>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }
        }


        public List<StateObj> GetAllStates()
        {
            string url = string.Concat(_baseUrl, "allstates");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<StateObj> resp = JsonConvert.DeserializeObject<List<StateObj>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }

        }



        public List<UserInfo> GetAllEngineers()
        {
            string url = string.Concat(_baseUrl, "allengineers");
            string RoleName = "Engineer";
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("RoleName", RoleName);
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<UserInfo> resp = JsonConvert.DeserializeObject<List<UserInfo>>(response.Content);
            if (resp == null)
            {
                return resp;
            }
            else
            {
                return resp;
            }
        }


        public AppResp InsertClient(ClientObj param)
        {
            string url = string.Concat(_baseUrl, "insertclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("ClientId", param.ClientId);
            //request.AddParameter("ClientName", param.ClientName);
            //request.AddParameter("ClientAlias", param.ClientAlias);
            //request.AddParameter("IsClientActive", param.IsClientActive);
            //request.AddParameter("CreatedBy", param.CreatedBy);
            //request.AddParameter("CreatedOn", param.CreatedOn);
            //request.AddParameter("SystemIp", param.SystemIp);
            //request.AddParameter("Computername", param.Computername);
            //request.AddParameter("RespTime", param.RespTime);
            //request.AddParameter("RestTime", param.RestTime);
            //request.AddParameter("RespTimeUp", param.RespTimeUp);
            //request.AddParameter("RestTimeUp", param.RestTimeUp);
            //request.AddParameter("ClientBanner", param.ClientBanner);
            request.AddObject(param);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp != null || resp.RespCode != null)
            {
                return resp;
            }
            else
            {
                return null;
            }

        }


        public AppResp ModifyClient(ClientObj param)
        {
            string url = string.Concat(_baseUrl, "modifyclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("ClientId", param.ClientId);
            //request.AddParameter("ClientName", param.ClientName);
            //request.AddParameter("ClientAlias", param.ClientAlias);
            //request.AddParameter("IsClientActive", param.IsClientActive);
            //request.AddParameter("CreatedBy", param.CreatedBy);
            //request.AddParameter("CreatedOn", param.CreatedOn);
            //request.AddParameter("SystemIp",param.SystemIp);
            //request.AddParameter("Computername", param.Computername);
            //request.AddParameter("UserName", param.UserName);
            //request.AddParameter("RespTime", param.RespTime);
            //request.AddParameter("RestTime", param.RestTime);
            //request.AddParameter("RespTimeUp", param.RespTimeUp);
            //request.AddParameter("RestTimeUp", param.RestTimeUp);
            //request.AddParameter("ClientBanner", param.ClientBanner);
            request.AddObject(param);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode==null)
            {
                return null;
            }
            else
            {
                return resp;
            }
        }

        public ClientResp GetClientDetails(int ClientId)
        {
            string url = string.Concat(_baseUrl, "clientdetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("ClientId", ClientId);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ClientResp resp = JsonConvert.DeserializeObject<ClientResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
        }

        public AppResp InsertRole(RoleObj param)
        {
            string url = string.Concat(_baseUrl, "insertrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("RoleId", param.RoleId);
            //request.AddParameter("Title", param.RoleName);
            //request.AddParameter("_Desc", param.RoleDesc);
            //request.AddParameter("IsRoleActive", param.IsRoleActive);
            request.AddObject(param);
            request.AddParameter("application/json",ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
            
        }


        public AppResp ModifyRole(string RoleName, string RoleDesc, bool IsRoleActive, int RoleId)
        {
            string url = string.Concat(_baseUrl, "modifyrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("RoleId", RoleId);
            request.AddParameter("RoleName", RoleName);
            request.AddParameter("RoleDesc", RoleDesc);
            request.AddParameter("IsRoleActive", IsRoleActive);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }


        public RoleResp GetRoleDetails(int RoleId)
        {
            string url = string.Concat(_baseUrl, "roledetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("RoleId", RoleId);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            RoleResp resp = JsonConvert.DeserializeObject<RoleResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
        }


        //Insert new user account
        public AppResp InsertUserRecord(UserInfo param)
        {
            string url = string.Concat(_baseUrl, "newuser");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("UserId", param.UserId);
            //request.AddParameter("FirstName", param.FirstName);
            //request.AddParameter("MiddleName", param.MiddleName);
            //request.AddParameter("LastName", param.LastName);
            //request.AddParameter("UserName", param.UserName);
            //request.AddParameter("Email", param.Email);
            //request.AddParameter("Password", param.Password);
            //request.AddParameter("PhoneNo", param.PhoneNo);
            //request.AddParameter("UserImg", param.UserImg);
            //request.AddParameter("Sex", param.Sex);
            //request.AddParameter("RoleId", param.RoleId);
            //request.AddParameter("ClientId", param.ClientId);
            //request.AddParameter("IsUserActive", param.IsUserActive);
            //request.AddParameter("CreatedBy", param.CreatedBy);
            //request.AddParameter("CreatedOn", param.CreatedOn);
            //request.AddParameter("ModifiedBy", param.ModifiedBy);
            //request.AddParameter("ModifiedOn", param.ModifiedOn);
            //request.AddParameter("SystemIp", param.SystemIp);
            //request.AddParameter("Computername", param.Computername);
            request.AddObject(param);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp==null || resp.RespCode==null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }



        public AppResp ModifyUserRecord(UserInfo param)
        {
            string url = string.Concat(_baseUrl, "modifyuser");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddObject(param);
            //request.AddParameter("UserId", param.UserId);
            //request.AddParameter("FirstName", param.FirstName);
            //request.AddParameter("MiddleName", param.MiddleName);
            //request.AddParameter("LastName", param.LastName);
            //request.AddParameter("UserName", param.UserName);
            //request.AddParameter("Email", param.Email);
            //request.AddParameter("Password", param.Password);
            //request.AddParameter("PhoneNo", param.PhoneNo);
            //request.AddParameter("UserImg", param.UserImg);
            //request.AddParameter("RoleId", param.RoleId);
            //request.AddParameter("Sex", param.Sex);
            //request.AddParameter("ClientId", param.ClientId);
            //request.AddParameter("IsUserActive", param.IsUserActive);
            //request.AddParameter("CreatedBy", param.CreatedBy);
            //request.AddParameter("CreatedOn", param.CreatedOn);
            //request.AddParameter("ModifiedBy", param.ModifiedBy);
            //request.AddParameter("ModifiedOn", param.ModifiedOn);
            //request.AddParameter("SystemIp", param.SystemIp);
            //request.AddParameter("Computername", param.Computername);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
         }


        public List<UserInfo> GetAllUser()
        {
            string url = string.Concat(_baseUrl, "allusers");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<UserInfo> resp = JsonConvert.DeserializeObject<List<UserInfo>>(response.Content);
            if (resp == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }



        //Insert new terminal account
        public AppResp InsertTerminal(TerminalObj param)
        {
            string url = string.Concat(_baseUrl, "newterminal");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("TerminalId", param.TerminalId);
            //request.AddParameter("TerminalRef", param.TerminalRef);
            //request.AddParameter("SerialNo", param.SerialNo);
            //request.AddParameter("RegionId", param.RegionId);
            //request.AddParameter("ClientId", param.ClientId);
            //request.AddParameter("BrandId", param.BrandId);
            //request.AddParameter("Engineer", param.Engineer);
            //request.AddParameter("IsUnderSupport", param.IsUnderSupport);
            //request.AddParameter("IsTerminalActive", param.IsTerminalActive);
            //request.AddParameter("Location", param.Location);
            //request.AddParameter("CreatedBy", param.CreatedBy);
            //request.AddParameter("CreatedOn", param.CreatedOn);
            //request.AddParameter("ModifiedBy", param.ModifiedBy);
            //request.AddParameter("ModifiedOn", param.ModifiedOn);
            //request.AddParameter("SystemIp", param.SystemIp);
            //request.AddParameter("Computername", param.Computername);
            request.AddObject(param);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }


        //Insert new terminal account
        public AppResp UploadUserRecord(List<UserDetails> param)
        {
            string url = string.Concat(_baseUrl, "bulkrecord");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDat = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", UserDat, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }


        //Insert new terminal account
        public AppResp UploadTerminal(List<TerminalData> param)
        {
            string url = string.Concat(_baseUrl, "uploadterminal");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //string requestData = JsonConvert.SerializeObject(oPaymentRequest);
            //request.AddObject(param,);
            string TerminalDat = JsonConvert.SerializeObject(param);
            //request.AddParameter(requestData);
            request.AddParameter("application/json", TerminalDat, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }



        //Insert new terminal account
        public AppResp ModifyTerminal(TerminalObj param)
        {
            string url = string.Concat(_baseUrl, "modifyterminal");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //request.AddParameter("TerminalId", param.TerminalId);
            //request.AddParameter("TerminalRef", param.TerminalRef);
            //request.AddParameter("SerialNo", param.SerialNo);
            //request.AddParameter("RegionId", param.RegionId);
            //request.AddParameter("ClientId", param.ClientId);
            //request.AddParameter("BrandId", param.BrandId);
            //request.AddParameter("Engineer", param.Engineer);
            //request.AddParameter("IsUnderSupport", param.IsUnderSupport);
            //request.AddParameter("IsTerminalActive", param.IsTerminalActive);
            //request.AddParameter("Location", param.Location);
            //request.AddParameter("CreatedBy", param.CreatedBy);
            //request.AddParameter("CreatedOn", param.CreatedOn);
            //request.AddParameter("ModifiedBy", param.ModifiedBy);
            //request.AddParameter("ModifiedOn", param.ModifiedOn);
            //request.AddParameter("SystemIp", param.SystemIp);
            //request.AddParameter("Computername", param.Computername);
            request.AddObject(param);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }

        public AppResp GetTerminalDetails(string TerminalNo, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "terminaldetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("TerminalNo", TerminalNo);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("Computername", Computername);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }

        public List<TerminalObj> GetAllTerminal()
        {
            string url = string.Concat(_baseUrl, "allterminals");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<TerminalObj> resp = JsonConvert.DeserializeObject<List<TerminalObj>>(response.Content);
            if (resp == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }


        public AppResp TerminateSession(string UserName, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "logout");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("Username", UserName);
            request.AddParameter("Computername", Computername);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }

        }


        public AppResp ResetPassword(string Username, string Password, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "changepassword");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("Username", Username);
            request.AddParameter("Username", Password);
            request.AddParameter("Computername", Computername);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
        }



        public AppResp ForgotPassword(string Email, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "forgotpassword");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("Email", Email);
            request.AddParameter("Computername", Computername);
            request.AddParameter("SystemIp", SystemIp);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
        }


        public string EncryptValue(string Value)
        {

            int HashValue=0;
            string Decrypt = "";
            foreach (var chars in Value)
            {
                HashValue = (char.ToUpper(chars) - 64);

                if (HashValue % 2 != 0)
                {
                    HashValue = (HashValue * 3) + 1;
                    Decrypt += "o" + HashValue;
                }
                else
                {
                    HashValue = HashValue / 2;
                    Decrypt += "e" + HashValue;
                }

            }


            return Decrypt;
        }



        public string[] DecryptValue(string Value)
        {
            string[] Result = null;
            foreach (var chars in Value)
            {
                if (char.IsLetter(chars))
                {
                    Result = Value.Split(chars);
                }
      
            }


            return Result;
        }
    }
}