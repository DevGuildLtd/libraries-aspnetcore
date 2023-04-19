using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public interface IDataProtectionProviderBuilder
    {
        IDataProtectionProviderBuilder AddCustomPersistence(IDataProtectionPersistenceProvider provider);

        IDataProtectionProviderBuilder AddCustomEncryption(IDataProtectionEncryptionProvider provider);
    }
}
