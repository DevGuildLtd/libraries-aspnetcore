using System;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    [SupportedOSPlatform("windows")]
    public class DpapiNGDataProtectionEncryptionProvider : IDataProtectionEncryptionProvider
    {
        public String Name => "DpapiNG";

        public IDataProtectionBuilder ConfigureEncryption(IDataProtectionBuilder builder, IConfigurationSection configuration)
        {
            var descriptor = configuration.GetValue<String>("Descriptor", "");
            var flags = configuration.GetValue<DpapiNGProtectionDescriptorFlags>("Flags", DpapiNGProtectionDescriptorFlags.None);
            if (String.IsNullOrEmpty(descriptor))
            {
                return builder.ProtectKeysWithDpapiNG();
            }
            else
            {
                return builder.ProtectKeysWithDpapiNG(descriptor, flags);
            }
        }
    }
}
