using System;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    [SupportedOSPlatform("windows")]
    public class RegistryDataProtectionPersistenceProvider : IDataProtectionPersistenceProvider
    {
        public String Name => "Registry";

        public IDataProtectionBuilder ConfigurePersistence(IDataProtectionBuilder builder, IConfigurationSection configuration)
        {
            var registryKey = this.OpenKey(configuration.GetValue<String>("RegistryKey"));
            return builder.PersistKeysToRegistry(registryKey);
        }

        private RegistryKey OpenKey(String name)
        {
            var registryKey = this.GetBaseKey(name, out var subKey);
            if (!String.IsNullOrEmpty(subKey))
            {
                registryKey = registryKey.OpenSubKey(subKey, true);
            }

            return registryKey;
        }

        private RegistryKey GetBaseKey(String name, out String subKey)
        {
            var firstSeparator = name.IndexOf('\\');
            var baseKeyName = firstSeparator >= 0 ? name.Substring(0, firstSeparator) : name;

            RegistryKey baseKey;
            switch (baseKeyName.ToUpperInvariant())
            {
                case "HKEY_LOCAL_MACHINE":
                    baseKey = Registry.LocalMachine;
                    break;
                case "HKEY_CURRENT_USER":
                    baseKey = Registry.CurrentUser;
                    break;
                case "HKEY_USERS":
                    baseKey = Registry.Users;
                    break;
                case "HKEY_CLASSES_ROOT":
                    baseKey = Registry.ClassesRoot;
                    break;
                case "HKEY_CURRENT_CONFIG":
                    baseKey = Registry.CurrentConfig;
                    break;
                case "HKEY_PERFORMANCE_DATA":
                    baseKey = Registry.PerformanceData;
                    break;
                default:
                    throw new InvalidOperationException("Invalid registry key name");
            }

            subKey = firstSeparator >= 0 && name.Length > firstSeparator ? name.Substring(firstSeparator + 1, name.Length - firstSeparator - 1) : "";
            return baseKey;
        }
    }
}
