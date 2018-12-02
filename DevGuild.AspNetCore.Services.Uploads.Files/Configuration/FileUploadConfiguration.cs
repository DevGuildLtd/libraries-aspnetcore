using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Configuration
{
    public class FileUploadConfiguration
    {
        public FileUploadConfiguration(String id, Int32 version, String container, String containerPrefix, Int64 maximumSize, IEnumerable<String> allowedFormats)
        {
            this.Id = id;
            this.Version = version;
            this.Container = container;
            this.ContainerPrefix = containerPrefix;
            this.MaximumSize = maximumSize;
            this.AllowedFormats = allowedFormats.ToList().AsReadOnly();
        }

        public String Id { get; }

        public Int32 Version { get; }

        public String Container { get; }

        public String ContainerPrefix { get; }

        public Int64 MaximumSize { get; }

        public IReadOnlyCollection<String> AllowedFormats { get; }
    }
}
