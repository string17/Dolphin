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

        public UserResponse ValidateUser(string UserName,string Password,string Computername, string SystemIp)
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
            UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }  
        }


        public UserInfo GetUserDetails(string UserName, string Computername, string SystemIp)
        {
            string url = string.Concat(_baseUrl, "userdetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("UserId", UserName);
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
            string url = string.Concat(_baseUrl, "allbrand");
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


        public bool InsertBrand(BrandObj param)
        {
            string url = string.Concat(_baseUrl, "insertbrand");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "BrandId=" + param.BrandId + "&BrandTitle=" + param.BrandTitle + "&BrandDesc=" + param.BrandDesc + "&IsBrandActive=" + param.IsBrandActive  + "&SystemIp=" + param.SystemIp + "&Computername=" + param.SystemName, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            BrandResp resp = JsonConvert.DeserializeObject<BrandResp>(response.Content);
            if (resp.RespCode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
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
            request.AddParameter("application/x-www-form-urlencoded", "BrandId=" + param.BrandId + "&BrandTitle=" + param.BrandTitle + "&BrandDesc=" + param.BrandDesc + "&IsBrandActive=" + param.IsBrandActive + "&SystemIp=" + param.SystemIp + "&Computername=" + param.SystemName, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            BrandResp resp = JsonConvert.DeserializeObject<BrandResp>(response.Content);
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


        public bool InsertClient(ClientObj param)
        {
            string url = string.Concat(_baseUrl, "insertclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("ClientId", param.ClientId);
            request.AddParameter("ClientName", param.ClientName);
            request.AddParameter("ClientAlias", param.ClientAlias);
            request.AddParameter("IsClientActive", param.IsClientActive);
            request.AddParameter("CreatedBy", param.CreatedBy);
            request.AddParameter("CreatedOn", param.CreatedOn);
            request.AddParameter("SystemIp", param.SystemIp);
            request.AddParameter("Computername", param.Computername);
            request.AddParameter("RespTime", param.RespTime);
            request.AddParameter("RestTime", param.RestTime);
            request.AddParameter("ClientBanner", param.ClientBanner);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            ClientResp resp = JsonConvert.DeserializeObject<ClientResp>(response.Content);
            if (resp.RespCode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public ClientResp ModifyClient(ClientObj param)
        {
            string url = string.Concat(_baseUrl, "modifyclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("ClientId", param.ClientId);
            request.AddParameter("ClientName", param.ClientName);
            request.AddParameter("ClientAlias", param.ClientAlias);
            request.AddParameter("IsClientActive", param.IsClientActive);
            request.AddParameter("CreatedBy", param.CreatedBy);
            request.AddParameter("CreatedOn", param.CreatedOn);
            request.AddParameter("SystemIp",param.SystemIp);
            request.AddParameter("Computername", param.Computername);
            request.AddParameter("RespTime", param.RespTime);
            request.AddParameter("RestTime", param.RestTime);
            request.AddParameter("ClientBanner", param.ClientBanner);
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

        public bool InsertRole(RoleObj param)
        {
            string url = string.Concat(_baseUrl, "insertrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("RoleId", param.RoleId);
            request.AddParameter("Title", param.Title);
            request.AddParameter("_Desc", param._Desc);
            request.AddParameter("IsRoleActive", param.IsRoleActive);
            request.AddParameter("application/json",ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            RoleResp resp = JsonConvert.DeserializeObject<RoleResp>(response.Content);
            if (resp.RespCode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        public bool ModifyRole(string Title, string _Desc, bool IsRoleActive, int RoleId)
        {
            string url = string.Concat(_baseUrl, "modifyrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("RoleId", RoleId);
            request.AddParameter("Title", Title);
            request.AddParameter("_Desc", _Desc);
            request.AddParameter("IsRoleActive", IsRoleActive);
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            RoleResp resp = JsonConvert.DeserializeObject<RoleResp>(response.Content);
            if (resp.RespCode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
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


        public bool TerminateSession(string UserName, string Computername, string SystemIp)
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
            UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            if (resp.RespCode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public UserResponse ResetPassword(string Username, string Password, string Computername, string SystemIp)
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
            UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            if (resp == null || resp.RespCode == null)
            {
                return null;
            }
            else
            {
                return resp;
            }
        }



        public UserResponse ForgotPassword(string Email, string Computername, string SystemIp)
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
            UserResponse resp = JsonConvert.DeserializeObject<UserResponse>(response.Content);
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