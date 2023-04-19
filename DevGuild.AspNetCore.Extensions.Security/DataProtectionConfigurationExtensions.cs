using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Extensions.Security.DataProtection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Extensions.Security
{
    public static class DataProtectionConfigurationExtensions
    {
        public static IServiceCollection ConfigureDataProtection(this IServiceCollection services, IConfiguration configuration, Action<IDataProtectionProviderBuilder> providers)
        {
            var providerBuilder = new DataProtectionProviderBuilder();
            providers(providerBuilder);

            var provider = providerBuilder.Build();

            var rootSection = DataProtectionConfigurationExtensions.GetRootSection(configuration);
            if (rootSection == null)
            {
                return services;
            }

            var dataProtection = services.AddDataProtection();

            var persistenceSection = rootSection.GetSection("Persistence");
            if (persistenceSection.Exists())
            {
                var providerName = persistenceSection.GetValue<String>("Provider");
                if (String.IsNullOrEmpty(providerName))
                {
                    throw new InvalidOperationException("Provider is not configured for DataProtection Persistence");
                }

                if (provider.Persistence.TryGetProvider(providerName, out var persistenceProvider))
                {
                    dataProtection = persistenceProvider.ConfigurePersistence(dataProtection, persistenceSection);
                }
                else
                {
                    throw new InvalidOperationException($"Provider {providerName} is not registered as DataProtection Persistence Provider");
                }
            }

            var encryptionSection = rootSection.GetSection("Encryption");
            if (encryptionSection.Exists())
            {
                var providerName = encryptionSection.GetValue<String>("Provider");
                if (String.IsNullOrEmpty(providerName))
                {
                    throw new InvalidOperationException("Provider is not configured for DataProtection Encryption");
                }

                if (provider.Encryption.TryGetProvider(providerName, out var encryptionProvider))
                {
                    dataProtection = encryptionProvider.ConfigureEncryption(dataProtection, encryptionSection);
                }
                else
                {
                    throw new InvalidOperationException($"Provider {providerName} is not registered as DataProtection Encryption Provider");
                }
            }

            return services;
        }

        private static IConfigurationSection GetRootSection(IConfiguration configuration)
        {
            if (configuration is IConfigurationSection section)
            {
                return section;
            }

            section = configuration.GetSection("Security:DataProtection");
            return section.Exists() ? section : null;
        }

        public static IDataProtectionProviderBuilder AddEphemeralPersistence(this IDataProtectionProviderBuilder builder)
        {
            return builder.AddCustomPersistence(new EphemeralDataProtectionPersistenceProvider());
        }

        public static IDataProtectionProviderBuilder AddFileSystemPersistence(this IDataProtectionProviderBuilder builder)
        {
            return builder.AddCustomPersistence(new FileSystemDataProtectionPersistenceProvider());
        }

        public static IDataProtectionProviderBuilder AddRegistryPersistence(this IDataProtectionProviderBuilder builder)
        {
            if (OperatingSystem.IsWindows())
            {
                return builder.AddCustomPersistence(new RegistryDataProtectionPersistenceProvider());
            }
            else
            {
                return builder;
            }
        }

        public static IDataProtectionProviderBuilder AddDpapiEncryption(this IDataProtectionProviderBuilder builder)
        {
            if (OperatingSystem.IsWindows())
            {
                return builder.AddCustomEncryption(new DpapiDataProtectionEncryptionProvider());
            }
            else
            {
                return builder;
            }
        }

        public static IDataProtectionProviderBuilder AddDpapiNGEncryption(this IDataProtectionProviderBuilder builder)
        {
            if (OperatingSystem.IsWindows())
            {
                return builder.AddCustomEncryption(new DpapiNGDataProtectionEncryptionProvider());
            }
            else
            {
                return builder;
            }
        }

        public static IDataProtectionProviderBuilder AddCertificateEncryption(this IDataProtectionProviderBuilder builder)
        {
            return builder.AddCustomEncryption(new CertificateDataProtectionEncryptionProvider());
        }
    }
}
