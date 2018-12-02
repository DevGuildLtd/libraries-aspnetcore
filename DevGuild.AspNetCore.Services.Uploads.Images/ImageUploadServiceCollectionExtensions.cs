using System;
using DevGuild.AspNetCore.Services.Uploads.Images.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Uploads.Images
{
    public static class ImageUploadServiceCollectionExtensions
    {
        public static ImageUploadConfigurationManagerBuilder AddImageUpload(this IServiceCollection services, String noImageUrl)
        {
            var configurationManager = new ImageUploadConfigurationsManager { NoImageUrl = noImageUrl };

            services.AddSingleton<ImageUploadConfigurationsManager>(configurationManager);
            services.AddScoped<IImageUploadService, ImageUploadService>();

            var builder = new ImageUploadConfigurationManagerBuilder(configurationManager);
            return builder;
        }
    }
}
