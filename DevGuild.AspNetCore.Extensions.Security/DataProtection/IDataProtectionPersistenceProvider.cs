using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public interface IDataProtectionPersistenceProvider
    {
        String Name { get; }

        IDataProtectionBuilder ConfigurePersistence(IDataProtectionBuilder builder, IConfigurationSection configuration);
    }
}
