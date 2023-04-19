using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class DataProtectionEncryptionProviderCollection : IDataProtectionEncryptionProviderCollection
    {
        private readonly List<IDataProtectionEncryptionProvider> providers;
        private readonly Dictionary<String, IDataProtectionEncryptionProvider> providersMap;

        public DataProtectionEncryptionProviderCollection(IEnumerable<IDataProtectionEncryptionProvider> providers)
        {
            this.providers = new List<IDataProtectionEncryptionProvider>();
            this.providersMap = new Dictionary<String, IDataProtectionEncryptionProvider>();

            foreach (var provider in providers)
            {
                this.providers.Add(provider);
                this.providersMap.Add(provider.Name, provider);
            }
        }

        public IReadOnlyList<IDataProtectionEncryptionProvider> Providers => this.providers;

        public IDataProtectionEncryptionProvider GetProvider(String name) => this.providersMap[name];

        public Boolean TryGetProvider(String name, out IDataProtectionEncryptionProvider provider) => this.providersMap.TryGetValue(name, out provider);

        public Boolean HasProvider(String name) => this.providersMap.ContainsKey(name);
    }
}
