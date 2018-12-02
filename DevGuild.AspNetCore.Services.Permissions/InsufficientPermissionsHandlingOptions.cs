using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions
{
    public class InsufficientPermissionsHandlingOptions
    {
        public String LoginUrl { get; set; } = "/Account/Login";

        public Boolean ReturnNotFoundForAuthenticatedUsers { get; set; } = false;
    }
}
