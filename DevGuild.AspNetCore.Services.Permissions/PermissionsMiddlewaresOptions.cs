using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions
{
    public class PermissionsMiddlewaresOptions
    {
        public InsufficientPermissionsHandlingOptions InsufficientPermissionsHandling { get; } = new InsufficientPermissionsHandlingOptions();
    }
}
