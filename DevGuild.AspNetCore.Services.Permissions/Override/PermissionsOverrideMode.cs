using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Represents permissions override mode.
    /// </summary>
    public enum PermissionsOverrideMode
    {
        /// <summary>
        /// Override is disabled.
        /// </summary>
        Disabled,

        /// <summary>
        /// Parent overrides are checked before child checks.
        /// </summary>
        BeforeChildCheck,

        /// <summary>
        /// Parent overrides are checked after child checks.
        /// </summary>
        AfterChildCheck
    }
}
