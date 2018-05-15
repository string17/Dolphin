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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Username=" + UserName  + "&Password=" + Password + "&Computername=" + Computername + "&SystemIp="+ SystemIp, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "UserId=" + UserName+ "&Computername=" + Computername + "&SystemIp=" + SystemIp, ParameterType.RequestBody);
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
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Username=" + Username + "&Computername=" + Computername + "&SystemIp=" + SystemIp, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Username=" + Username + "&Computername=" + Computername + "&SystemIp=" + SystemIp, ParameterType.RequestBody);
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

        //Get all menu
        public List<MenuObj> GetAllMenu()
        {
            return new List<MenuObj>
            {

            };
            //string url = string.Concat(_baseUrl, "ListMenu");
            //var client = new RestClient(url);
            //var request = new RestSharp.RestRequest(Method.POST);
            //request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "application/x-www-form-urlencoded");
            //IRestResponse response = client.Execute(request);

            //List<MenuObj> resp = JsonConvert.DeserializeObject<List<MenuObj>>(response.Content);
            //if (resp == null)
            //{
            //    return resp;
            //}
            //else
            //{
            //    return resp;
            //}

        }


        //public List<RoleObj> GetAllRole()
        //{
        //    string url = string.Concat(_baseUrl, "allrole");
        //    var client = new RestClient(url);
        //    var request = new RestSharp.RestRequest(Method.POST);
        //    request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //    IRestResponse response = client.Execute(request);

        //    List<RoleObj> resp = JsonConvert.DeserializeObject<List<RoleObj>>(response.Content);
        //    if (resp == null)
        //    {
        //        return resp;
        //    }
        //    else
        //    {
        //        return resp;
        //    }
        //    //return Roles;
        //}


        public List<RoleObj> GetAllRole()
        {
            string url = string.Concat(_baseUrl, "allrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "ClientName=" + param.ClientName + "&RespTime=" + param.RespTime + "&RestTime=" + param.RestTime + "&ClientBanner=" + param.ClientBanner + "&ExtClientBanner=" + param.ExtClientBanner + "&ClientAlias=" +param.ClientAlias+ "&IsClientActive="+param.IsClientActive+ "&CreatedBy="+param.CreatedBy+ "&CreatedOn="+param.CreatedOn + "&SystemIp=" + param.SystemIp + "&Computername=" + param.Computername, ParameterType.RequestBody);
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

        public bool ModifyClient(ClientObj param)
        {
            string url = string.Concat(_baseUrl, "modifyclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "ClientName=" + param.ClientName + "&RespTime=" + param.RespTime + "&RestTime=" + param.RestTime + "&ClientBanner=" + param.ClientBanner + "&ExtClientBanner=" + param.ExtClientBanner + "&ClientAlias=" + param.ClientAlias + "&IsClientActive=" + param.IsClientActive + "&CreatedBy=" + param.CreatedBy + "&CreatedOn=" + param.CreatedOn +"&ClientId=" + param.ClientId + "&SystemIp=" + param.SystemIp + "&Computername=" + param.Computername, ParameterType.RequestBody);
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

        public ClientResp GetClientDetails(int ClientId)
        {
            string url = string.Concat(_baseUrl, "clientdetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "ClientId=" + ClientId, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Title=" + param.Title+ "&_Desc=" + param._Desc + "&IsRoleActive=" + param.IsRoleActive + "&RoleId="+param.RoleId, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Title=" + Title + "&_Desc=" + _Desc + "&IsRoleActive=" + IsRoleActive + "&RoleId=" + RoleId, ParameterType.RequestBody);
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
            request.AddParameter("application/x-www-form-urlencoded", "RoleId=" + RoleId, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Username=" + UserName + "&Computername=" + Computername + "&SystemIp=" + SystemIp, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded","Username=" + Username + "&Password=" + Password + "&EventDate = " + DateTime.Now + "&Computername=" + Computername + "&SystemIp=" + SystemIp, ParameterType.RequestBody);
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
            request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "Email=" + Email + "&EventDate=" + DateTime.Now + "&Computername=" + Computername + "&SystemIp=" + SystemIp, ParameterType.RequestBody);
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
    }
}