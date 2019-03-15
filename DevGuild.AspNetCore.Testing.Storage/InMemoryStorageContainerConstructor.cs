using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Storage;

namespace DevGuild.AspNetCore.Testing.Storage
{
    public class InMemoryStorageContainerConstructor : StorageContainerConstructor
    {
        public InMemoryStorageContainerConstructor(String baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        public String BaseUrl { get; }

        public Dictionary<String, Byte[]> InMemoryFiles { get; } = new Dictionary<String, Byte[]>();

        public override IStorageContainer Create()
        {
            return new InMemoryStorageContainer(this.BaseUrl, this.InMemoryFiles);
        }
    }
}
