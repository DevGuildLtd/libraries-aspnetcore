using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Uploads.Images.Configuration
{
    public class ImageUploadConfigurationManagerBuilder
    {
        private readonly ImageUploadConfigurationsManager configurationsManager;

        public ImageUploadConfigurationManagerBuilder(ImageUploadConfigurationsManager configurationsManager)
        {
            this.configurationsManager = configurationsManager;
        }

        public ImageUploadConfigurationManagerBuilder AddConfiguration(String identifier, Int32 version, Func<ImageUploadConfigurationBuilder, ImageUploadConfigurationBuilder> builder)
        {
            this.configurationsManager.AddConfiguration(identifier, version, builder);
            return this;
        }
    }
}
