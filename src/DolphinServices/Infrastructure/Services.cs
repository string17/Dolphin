using DolphinServices.Request;
using DolphinServices.Response;
using DolphinServices.Responses;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace DolphinServices.Infrastructure
{
    public class Services
    {
        private readonly string _baseUrl = WebConfigurationManager.AppSettings["BaseUrl"];

        public LoginResponse ValidateUser(LoginRequest param)
        {
            string url = string.Concat(_baseUrl, "login");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string LoginObj = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", LoginObj, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            if (loginResponse == null || loginResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return loginResponse;
            }
        }



        public LoginResponse lockScreen(LoginRequest param)
        {
            string url = string.Concat(_baseUrl, "lockaccess");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string LoginDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", LoginDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            if (loginResponse == null || loginResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return loginResponse;
            }
        }


        public LoginResponse UnlockAccount(LoginRequest param)
        {
            string url = string.Concat(_baseUrl, "unlock");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string LoginDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", LoginDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

            if (loginResponse == null || loginResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return loginResponse;
            }
        }

        public UserDetailsResponse GetUserDetails(int Id)
        {
            string url = string.Concat(_baseUrl, "userdetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string UserId = JsonConvert.SerializeObject(Id);
            request.AddParameter("application/json", UserId, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            UserDetailsResponse userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }
        }


        public UserDetailsResponse GetUserInfo(string UserName)
        {
            string url = string.Concat(_baseUrl, "userinfo");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string UserId = JsonConvert.SerializeObject(UserName);
            request.AddParameter("application/json", UserId, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            var userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }
        }


        //Get User Menu
        public UserMenuResponse GetMenu(string UserName)
        {
            string url = string.Concat(_baseUrl, "menu");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            string LoginObj = JsonConvert.SerializeObject(UserName);
            request.AddParameter("application/json", LoginObj, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            var userMenuResponse = JsonConvert.DeserializeObject<UserMenuResponse>(response.Content);
            if (userMenuResponse == null || userMenuResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return userMenuResponse;
            }

        }


        public RoleResponse GetAllRole()
        {
            string url = string.Concat(_baseUrl, "allrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            RoleResponse roleResponse = JsonConvert.DeserializeObject<RoleResponse>(response.Content);
            if (roleResponse == null || roleResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return roleResponse;
            }

        }


        public BrandResponse GetAllBrand()
        {
            string url = string.Concat(_baseUrl, "allbrands");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/json", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var brandResponse = JsonConvert.DeserializeObject<BrandResponse>(response.Content);
            if (brandResponse == null || brandResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return brandResponse;
            }

        }


        //public AppResp InsertBrand(BrandObj param)
        //{
        //    string url = string.Concat(_baseUrl, "insertbrand");
        //    var client = new RestClient(url);
        //    var request = new RestSharp.RestRequest(Method.POST);
        //    request.AddObject(param);
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //    request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);

        //    string UserMenu = JsonConvert.SerializeObject(param);
        //    request.AddParameter("application/json", UserMenu, ParameterType.RequestBody);
        //    request.RequestFormat = DataFormat.Json;
        //    IRestResponse response = client.Execute(request);

        //    AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
        //    if (resp.RespCode == null || resp == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return resp;
        //    }

        //}

        //public bool ModifyBrand(BrandObj param)
        //{
        //    string url = string.Concat(_baseUrl, "modifybrand");
        //    var client = new RestClient(url);
        //    var request = new RestSharp.RestRequest(Method.POST);
        //    request.AddHeader("postman-token", "06950d99-7ddc-04c1-689d-022955c29656");
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //    request.AddObject(param);
        //    request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    AppResp resp = JsonConvert.DeserializeObject<AppResp>(response.Content);
        //    if (resp.RespCode.Equals("00"))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}


        public ClientResponse GetAllClient()
        {
            string url = string.Concat(_baseUrl, "allclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var clientResponse = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
            if (clientResponse == null || clientResponse.ResponseCode==null)
            {
                return null;
            }
            else
            {
                return clientResponse;
            }

        }


        public ClientResponse GetOnlyClients()
        {
            string url = string.Concat(_baseUrl, "onlyclients");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var clientResponse = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
            if (clientResponse == null || clientResponse.ResponseCode == null)
            {
                return clientResponse;
            }
            else
            {
                return clientResponse;
            }
        }


        public StateResponse GetAllStates()
        {
            string url = string.Concat(_baseUrl, "allstates");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var stateResponse = JsonConvert.DeserializeObject<StateResponse>(response.Content);
            if (stateResponse == null || stateResponse.ResponseCode==null)
            {
                return null;
            }
            else
            {
                return stateResponse;
            }

        }



        public UserDetailsResponse GetAllUserByRole(string Role)
        {
            string url = string.Concat(_baseUrl, "allengineers");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string RoleName = JsonConvert.SerializeObject(Role);
            request.AddParameter("application/json", RoleName, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            var userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode==null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }
        }


        public ClientResponse InsertClient(ClientRequest param)
        {
            string url = string.Concat(_baseUrl, "insertclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string ClientDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", ClientDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            ClientResponse clientResponse = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
            if (clientResponse != null || clientResponse.ResponseCode != null)
            {
                return clientResponse;
            }
            else
            {
                return null;
            }

        }


        public ClientResponse ModifyClient(ClientRequest param)
        {
            string url = string.Concat(_baseUrl, "modifyclient");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string ClientDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", ClientDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);

            ClientResponse clientResponse = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
            if (clientResponse == null || clientResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return clientResponse;
            }
        }


        public ClientResponse GetClientDetails(int Id)
        {
            string url = string.Concat(_baseUrl, "clientdetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string ClientId = JsonConvert.SerializeObject(Id);
            request.AddParameter("application/json", ClientId, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            ClientResponse clientResponse = JsonConvert.DeserializeObject<ClientResponse>(response.Content);
            if (clientResponse == null || clientResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return clientResponse;
            }
        }

        public RoleResponse InsertRole(RoleRequest param)
        {
            string url = string.Concat(_baseUrl, "insertrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string RoleDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",RoleDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            RoleResponse roleResponse = JsonConvert.DeserializeObject<RoleResponse>(response.Content);
            if (roleResponse == null || roleResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return roleResponse;
            }

        }


        public RoleResponse ModifyRole(RoleRequest param)
        {
            string url = string.Concat(_baseUrl, "modifyrole");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string RoleDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",RoleDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            RoleResponse roleResponse = JsonConvert.DeserializeObject<RoleResponse>(response.Content);
            if (roleResponse == null || roleResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return roleResponse;
            }

        }


        public RoleResponse GetRoleDetails(int Id)
        {
            string url = string.Concat(_baseUrl, "roledetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string RoleId = JsonConvert.SerializeObject(Id);
            request.AddParameter("application/json", RoleId,ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            RoleResponse roleResponse = JsonConvert.DeserializeObject<RoleResponse>(response.Content);
            if (roleResponse == null || roleResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return roleResponse;
            }
        }


        //Insert new user account
        public UserDetailsResponse InsertUserRecord(UserDetailsRequest param)
        {
            string url = string.Concat(_baseUrl, "newuser");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",UserDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            UserDetailsResponse userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }

        }



        public UserDetailsResponse ModifyUserRecord(UserDetailsRequest param)
        {
            string url = string.Concat(_baseUrl, "modifyuser");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",UserDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            UserDetailsResponse userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }
        }


        public UserDetailsResponse GetAllUser()
        {
            string url = string.Concat(_baseUrl, "allusers");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            UserDetailsResponse userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode==null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }

        }



        //Insert new terminal account
        public TerminalResponse InsertTerminal(TerminalRequest param)
        {
            string url = string.Concat(_baseUrl, "newterminal");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string TerminalDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",TerminalDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(response.Content);
            if (terminalResponse == null || terminalResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return terminalResponse;
            }

        }


        //Insert new terminal account
        public UserDetailsResponse UploadUserRecord(List<UserDetailsBulkRequest> param)
        {
            string url = string.Concat(_baseUrl, "bulkrecord");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", UserDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            UserDetailsResponse userDetailsResponse = JsonConvert.DeserializeObject<UserDetailsResponse>(response.Content);
            if (userDetailsResponse == null || userDetailsResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return userDetailsResponse;
            }

        }


        //Insert bulk terminal account
        public TerminalResponse UploadTerminal(List<TerminalBulkRequest> param)
        {
            string url = string.Concat(_baseUrl, "uploadterminal");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string TerminalDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", TerminalDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(response.Content);
            if (terminalResponse == null || terminalResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return terminalResponse;
            }

        }



        //Insert new terminal account
        public TerminalResponse ModifyTerminal(TerminalRequest param)
        {
            string url = string.Concat(_baseUrl, "modifyterminal");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string TerminalDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", TerminalDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(response.Content);
            if (terminalResponse == null || terminalResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return terminalResponse;
            }

        }


        public TerminalResponse GetTerminalDetails(TerminalRequest param)
        {
            string url = string.Concat(_baseUrl, "terminaldetails");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string TerminalDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",TerminalDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            TerminalResponse terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(response.Content);
            if (terminalResponse == null || terminalResponse.ResponseCode==null)
            {
                return null;
            }
            else
            {
                return terminalResponse;
            }

        }

        public TerminalResponse GetAllTerminal()
        {
            string url = string.Concat(_baseUrl, "allterminals");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var terminalResponse = JsonConvert.DeserializeObject<TerminalResponse>(response.Content);
            if (terminalResponse == null || terminalResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return terminalResponse;
            }

        }


        public LoginResponse TerminateSession(LoginRequest param)
        {
            string url = string.Concat(_baseUrl, "logout");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json",UserDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            if (loginResponse == null || loginResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return loginResponse;
            }

        }


        public LoginResponse ResetPassword(LoginRequest param)
        {
            string url = string.Concat(_baseUrl, "changepassword");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", UserDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            if (loginResponse == null || loginResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return loginResponse;
            }
        }



        public LoginResponse ForgotPassword(LoginRequest param)
        {
            string url = string.Concat(_baseUrl, "forgotpassword");
            var client = new RestClient(url);
            var request = new RestSharp.RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            string UserDetails = JsonConvert.SerializeObject(param);
            request.AddParameter("application/json", UserDetails, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            if (loginResponse == null || loginResponse.ResponseCode == null)
            {
                return null;
            }
            else
            {
                return loginResponse;
            }
        }


        public string EncryptValue(string Value)
        {

            int HashValue = 0;
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
