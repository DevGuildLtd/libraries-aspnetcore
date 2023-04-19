using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public interface IDataProtectionEncryptionProvider
    {
        String Name { get; }

        IDataProtectionBuilder ConfigureEncryption(IDataProtectionBuilder builder, IConfigurationSection configuration);
    }
}
