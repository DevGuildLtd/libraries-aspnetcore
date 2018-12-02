using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Uploads.Files.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Uploads.Files
{
    public static class FileUploadServiceCollectionExtensions
    {
        public static FileUploadConfigurationsManagerBuilder AddFileUpload(this IServiceCollection services)
        {
            var configurationManager = new FileUploadConfigurationsManager();

            services.AddSingleton<FileUploadConfigurationsManager>(configurationManager);
            services.AddScoped<IFileUploadService, FileUploadService>();

            var builder = new FileUploadConfigurationsManagerBuilder(configurationManager);
            return builder;
        }
    }
}
