using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class DataProtectionProviderBuilder : IDataProtectionProviderBuilder
    {
        private readonly List<IDataProtectionPersistenceProvider> persistenceProviders = new List<IDataProtectionPersistenceProvider>();
        private readonly List<IDataProtectionEncryptionProvider> encryptionProviders = new List<IDataProtectionEncryptionProvider>();

        public IDataProtectionProviderBuilder AddCustomPersistence(IDataProtectionPersistenceProvider provider)
        {
            this.persistenceProviders.Add(provider);
            return this;
        }

        public IDataProtectionProviderBuilder AddCustomEncryption(IDataProtectionEncryptionProvider provider)
        {
            this.encryptionProviders.Add(provider);
            return this;
        }

        public IDataProtectionProvider Build()
        {
            return new DataProtectionProvider(this.persistenceProviders, this.encryptionProviders);
        }
    }
}
