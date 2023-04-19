using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class EphemeralDataProtectionPersistenceProvider  : IDataProtectionPersistenceProvider
    {
        public String Name => "Ephemeral";

        public IDataProtectionBuilder ConfigurePersistence(IDataProtectionBuilder builder, IConfigurationSection configuration)
        {
            return builder.UseEphemeralDataProtectionProvider();
        }
    }
}
