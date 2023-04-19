using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Extensions.Security.DataProtection
{
    public class FileSystemDataProtectionPersistenceProvider : IDataProtectionPersistenceProvider
    {
        public String Name => "FileSystem";

        public IDataProtectionBuilder ConfigurePersistence(IDataProtectionBuilder builder, IConfigurationSection configuration)
        {
            var directoryPath = configuration.GetValue<String>("Directory");
            var directory = Directory.Exists(directoryPath) ? new DirectoryInfo(directoryPath) : Directory.CreateDirectory(directoryPath);

            return builder.PersistKeysToFileSystem(directory);
        }
    }
}
