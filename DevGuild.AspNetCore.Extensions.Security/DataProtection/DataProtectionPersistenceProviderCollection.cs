using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class DataProtectionPersistenceProviderCollection : IDataProtectionPersistenceProviderCollection
    {
        private readonly List<IDataProtectionPersistenceProvider> providers;
        private readonly Dictionary<String, IDataProtectionPersistenceProvider> providersMap;

        public DataProtectionPersistenceProviderCollection(IEnumerable<IDataProtectionPersistenceProvider> providers)
        {
            this.providers = new List<IDataProtectionPersistenceProvider>();
            this.providersMap = new Dictionary<String, IDataProtectionPersistenceProvider>();

            foreach (var provider in providers)
            {
                this.providers.Add(provider);
                this.providersMap.Add(provider.Name, provider);
            }
        }

        public IReadOnlyList<IDataProtectionPersistenceProvider> Providers => this.providers;

        public IDataProtectionPersistenceProvider GetProvider(String name) => this.providersMap[name];

        public Boolean TryGetProvider(String name, out IDataProtectionPersistenceProvider provider) => this.providersMap.TryGetValue(name, out provider);

        public Boolean HasProvider(String name) => this.providersMap.ContainsKey(name);
    }
}
