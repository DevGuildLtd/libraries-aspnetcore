using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace DevGuild.AspNetCore.Testing.Hosting
{
    public class MockHostingEnvironment : IHostingEnvironment
    {
        public MockHostingEnvironment(String applicationName, String environmentName, String contentRootPath, String webRootPath)
        {
            this.ApplicationName = applicationName;
            this.EnvironmentName = environmentName;
            this.ContentRootPath = contentRootPath;
            this.ContentRootFileProvider = new PhysicalFileProvider(this.ContentRootPath);
            this.WebRootPath = webRootPath;
            this.WebRootFileProvider = new PhysicalFileProvider(this.WebRootPath);
        }

        public MockHostingEnvironment(String applicationName, String environmentName, String contentRootPath)
            : this(applicationName, environmentName, contentRootPath, Path.Combine(contentRootPath, "wwwroot"))
        {
        }

        public String EnvironmentName { get; set; }

        public String ApplicationName { get; set; }

        public String WebRootPath { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public String ContentRootPath { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
