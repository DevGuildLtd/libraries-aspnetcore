using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public interface IDataProtectionPersistenceProviderCollection
    {
        IReadOnlyList<IDataProtectionPersistenceProvider> Providers { get; }

        IDataProtectionPersistenceProvider GetProvider(String name);

        Boolean TryGetProvider(String name, out IDataProtectionPersistenceProvider provider);

        Boolean HasProvider(String name);
    }
}
