using System;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    [SupportedOSPlatform("windows")]
    public class DpapiDataProtectionEncryptionProvider : IDataProtectionEncryptionProvider
    {
        public String Name => "Dpapi";

        public IDataProtectionBuilder ConfigureEncryption(IDataProtectionBuilder builder, IConfigurationSection configuration)
        {
            var protectToLocalMachine = configuration.GetValue<Boolean>("LocalMachine", false);
            return builder.ProtectKeysWithDpapi(protectToLocalMachine);
        }
    }
}
