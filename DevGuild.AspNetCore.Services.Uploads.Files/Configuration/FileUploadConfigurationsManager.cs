using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Contracts;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Configuration
{
    public sealed class FileUploadConfigurationsManager
    {
        private readonly Dictionary<String, FileUploadConfiguration> configurations = new Dictionary<String, FileUploadConfiguration>();

        internal void AddConfiguration(String identifier, Int32 version, Func<FileUploadConfigurationBuilder, FileUploadConfigurationBuilder> configuration)
        {
            Ensure.Argument.NotNullOrEmpty(identifier, nameof(identifier));
            Ensure.Argument.GreaterThanOrEqualTo(version, 1, nameof(version));
            Ensure.Argument.DoesNotMeetCondition(this.configurations.ContainsKey(identifier), nameof(identifier), $"Configuration '{identifier}' already exists.");

            var builder = new FileUploadConfigurationBuilder(identifier, version);
            builder = configuration.Invoke(builder);

            this.configurations.Add(identifier, builder.BuildConfiguration());
        }

        public FileUploadConfiguration GetConfiguration(String identifier)
        {
            Ensure.Argument.NotNullOrEmpty(identifier, nameof(identifier));

            return this.configurations.TryGetValue(identifier, out var configuration) ? configuration : null;
        }
    }
}
