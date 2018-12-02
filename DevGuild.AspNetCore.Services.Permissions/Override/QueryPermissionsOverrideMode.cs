using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.Override
{
    /// <summary>
    /// Represents query permissions override mode.
    /// </summary>
    public enum QueryPermissionsOverrideMode
    {
        /// <summary>
        /// Override is disabled.
        /// </summary>
        Disabled,

        /// <summary>
        /// Parent overrides are checked before child checks.
        /// </summary>
        BeforeChildCheck
    }
}
