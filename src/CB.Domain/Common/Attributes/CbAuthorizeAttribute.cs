﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CB.Domain.Common.Attributes;

public class CbAuthorizeAttribute : AuthorizeAttribute {

    public CbAuthorizeAttribute(params string[] claims) : base() {
        base.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        if (claims != null && claims.Length != 0) {
            base.Roles = string.Join(',', claims);
        }
    }
}

public static class CbClaim {

    public static class Web {
        public const string Dashboard = "BO.Dashboard";
        public const string Contact = "BO.Contact";
        public const string Setting_General_Api = "BO.General.Api";
        public const string Setting_User = "BO.User";
        public const string Setting_User_Reset = "BO.User.Reset";
        public const string Setting_Role = "BO.Role";
        public const string Service_Transaction = "BO.Transaction";
        public const string Service_Server = "BO.Server";
        public const string Category_Bot = "BO.Bot";
    }

    public static class NoClaim {
        public const string BO = "BO";
        public const string Setting = "BO.Setting";
        public const string General = "BO.General";
        public const string Category = "BO.Category";
        public const string Service = "BO.Service";
    }
}
