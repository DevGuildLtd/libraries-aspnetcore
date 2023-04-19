using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public interface IDataProtectionEncryptionProviderCollection
    {
        IReadOnlyList<IDataProtectionEncryptionProvider> Providers { get; }

        IDataProtectionEncryptionProvider GetProvider(String name);

        Boolean TryGetProvider(String name, out IDataProtectionEncryptionProvider provider);

        Boolean HasProvider(String name);
    }
}
