using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DolphinWeb.Models
{
    public class Constants
    {
        public enum ActionType
        {
            Login,
            ResetPassword,
            ForgotPassword,
            SetupClient,
            CreateRole,
            CreateUser,
            ModifyClientDetails,
            ModifyUserDetails,
            ModifyPersonalProfile,
            LockAccount,
            Logout
        }
    }
}