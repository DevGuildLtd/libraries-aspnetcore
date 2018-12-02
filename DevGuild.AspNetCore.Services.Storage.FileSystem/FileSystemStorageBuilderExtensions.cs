using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Services.Storage.FileSystem
{
    public static class FileSystemStorageBuilderExtensions
    {
        public static StorageServiceBuilder AddFileSystemProvider(this StorageServiceBuilder builder)
        {
            return builder.AddContainerProvider("FileSystem", configuration => new FileSystemStorageContainerConstructor
            {
                BaseDirectory = configuration.GetValue<String>("BaseDirectory"),
                BaseUrl = configuration.GetValue<String>("BaseUrl")
            });
        }
    }
}
