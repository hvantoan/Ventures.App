using Microsoft.AspNetCore.Authorization;

namespace CB.Domain.Common.Attributes;

public class CbAuthorizeAttribute : AuthorizeAttribute {

    public CbAuthorizeAttribute(params string[] claims) {
        if (claims != null && claims.Any()) {
            base.Roles = string.Join(',', claims);
        }
    }
}

public static class CbClaim {

    public static class Web {
        public const string Dashboard = "BO.Dashboard";
        public const string Setting_General_Order = "BO.General.Order";
        public const string Setting_General_GenerateCode = "BO.General.GenerateCode";
        public const string Setting_General_Api = "BO.General.Api";
        public const string Setting_General_Email = "BO.General.Email";
        public const string Setting_User = "BO.User";
        public const string Setting_User_Reset = "BO.User.Reset";
        public const string Setting_Role = "BO.Role";
    }
}
