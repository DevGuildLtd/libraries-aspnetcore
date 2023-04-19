using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class DataProtectionProvider : IDataProtectionProvider
    {
        private readonly DataProtectionPersistenceProviderCollection persistence;
        private readonly DataProtectionEncryptionProviderCollection encryption;

        public DataProtectionProvider(
            IEnumerable<IDataProtectionPersistenceProvider> persistenceProviders,
            IEnumerable<IDataProtectionEncryptionProvider> encryptionProviders)
        {
            this.persistence = new DataProtectionPersistenceProviderCollection(persistenceProviders);
            this.encryption = new DataProtectionEncryptionProviderCollection(encryptionProviders);
        }

        public IDataProtectionPersistenceProviderCollection Persistence => this.persistence;

        public IDataProtectionEncryptionProviderCollection Encryption => this.encryption;
    }
}
