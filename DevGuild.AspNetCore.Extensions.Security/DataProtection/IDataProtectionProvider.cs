using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public interface IDataProtectionProvider
    {
        IDataProtectionPersistenceProviderCollection Persistence { get; }

        IDataProtectionEncryptionProviderCollection Encryption { get; }
    }
}
