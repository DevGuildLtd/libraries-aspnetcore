using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Storage;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Testing.Storage
{
    public static class InMemoryStorageBuilderExtensions
    {
        public static StorageServiceBuilder AddInMemoryProvider(this StorageServiceBuilder builder)
        {
            return builder.AddContainerProvider("InMemory", configuration => new InMemoryStorageContainerConstructor(configuration.GetValue<String>("BaseUrl")));
        }
    }
}
