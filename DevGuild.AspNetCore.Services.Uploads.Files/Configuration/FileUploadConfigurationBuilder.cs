using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevGuild.AspNetCore.Contracts;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Configuration
{
    public class FileUploadConfigurationBuilder
    {
        private readonly String identifier;
        private readonly Int32 version;
        private readonly List<String> allowedFormats = new List<String>();
        private String containerName;
        private String containerPrefix;
        private Int64 maximumSize = Int64.MaxValue;

        public FileUploadConfigurationBuilder(String identifier, Int32 version)
        {
            Ensure.Argument.NotNullOrEmpty(identifier, nameof(identifier));
            Ensure.Argument.GreaterThanOrEqualTo(version, 1, nameof(version));

            this.identifier = identifier;
            this.version = version;
        }

        public FileUploadConfigurationBuilder Container(String containerName, String containerPrefix)
        {
            Ensure.Argument.NotNullOrEmpty(containerName, nameof(containerName));
            Ensure.Argument.NotNull(containerPrefix, nameof(containerPrefix));

            this.containerName = containerName;
            this.containerPrefix = containerPrefix;
            return this;
        }

        public FileUploadConfigurationBuilder AllowedFormats(params String[] formats)
        {
            Ensure.Argument.NotNull(formats, nameof(formats));
            Ensure.Argument.MeetCondition(formats.All(x => !String.IsNullOrEmpty(x)), nameof(formats));

            this.allowedFormats.AddRange(formats);
            return this;
        }

        public FileUploadConfigurationBuilder MaximumSize(Int64 value)
        {
            Ensure.Argument.GreaterThan(value, 0, nameof(value));

            this.maximumSize = value;
            return this;
        }

        public FileUploadConfiguration BuildConfiguration()
        {
            Ensure.State.NotNullOrEmpty(this.containerName, "Container is not configured");
            Ensure.State.NotNull(this.containerPrefix, "Container is not configured");
            Ensure.State.HasElements(this.allowedFormats, "No file format was allowed");

            return new FileUploadConfiguration(this.identifier, this.version, this.containerName, this.containerPrefix, this.maximumSize, this.allowedFormats);
        }
    }
}
