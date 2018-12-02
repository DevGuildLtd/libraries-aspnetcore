using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Files.Configuration
{
    public class FileUploadConfigurationsManagerBuilder
    {
        private readonly FileUploadConfigurationsManager configurationsManager;

        public FileUploadConfigurationsManagerBuilder(FileUploadConfigurationsManager configurationsManager)
        {
            this.configurationsManager = configurationsManager;
        }

        public FileUploadConfigurationsManagerBuilder AddConfiguration(String identifier, Int32 version, Func<FileUploadConfigurationBuilder, FileUploadConfigurationBuilder> configuration)
        {
            this.configurationsManager.AddConfiguration(identifier, version, configuration);
            return this;
        }
    }
}
