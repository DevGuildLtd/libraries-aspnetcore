using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Bundling.Models;
using DevGuild.AspNetCore.Services.Bundling.Models.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DevGuild.AspNetCore.Services.Bundling
{
    public class BundlingConfigurationService : IBundlingConfigurationService
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly BundlingOptions options;

        private Boolean initialized = false;
        private Dictionary<String, ScriptsBundle> scriptsBundles = null;
        private Dictionary<String, StylesBundle> stylesBundles = null;

        public BundlingConfigurationService(IWebHostEnvironment hostingEnvironment, IOptions<BundlingOptions> options)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.options = options.Value;
        }

        public Boolean Enabled => this.options.Enabled;

        public IReadOnlyDictionary<String, ScriptsBundle> ScriptsBundles
        {
            get
            {
                if (!this.initialized)
                {
                    throw new InvalidOperationException("Configuration not initialized");
                }

                return this.scriptsBundles;
            }
        }

        public IReadOnlyDictionary<String, StylesBundle> StylesBundles
        {
            get
            {
                if (!this.initialized)
                {
                    throw new InvalidOperationException("Configuration not initialized");
                }

                return this.stylesBundles;
            }
        }

        public Task InitializeAsync()
        {
            async Task ExecuteInitialization()
            {
                var configuration = await this.ReadConfigurationAsync();

                var stylesBundlesMap = new Dictionary<String, StylesBundle>();
                var scriptsBundlesMap = new Dictionary<String,  ScriptsBundle>();

                if (configuration.Styles != null)
                {
                    foreach (var bundleConfig in configuration.Styles)
                    {
                        var bundle = new StylesBundle(
                            output: this.ConvertToBundlePath(bundleConfig.Output),
                            input: bundleConfig.Input.Select(this.ConvertToBundlePath));

                        stylesBundlesMap.Add(bundle.Output.Path, bundle);
                    }
                }

                if (configuration.Scripts != null)
                {
                    foreach (var bundleConfig in configuration.Scripts)
                    {
                        var bundle = new ScriptsBundle(
                            output: this.ConvertToBundlePath(bundleConfig.Output),
                            input: bundleConfig.Input.Select(this.ConvertToBundlePath));

                        scriptsBundlesMap.Add(bundle.Output.Path, bundle);
                    }
                }

                this.initialized = true;
                this.scriptsBundles = scriptsBundlesMap;
                this.stylesBundles = stylesBundlesMap;
            }

            if (this.initialized)
            {
                return Task.CompletedTask;
            }

            return ExecuteInitialization();
        }

        private BundlePath ConvertToBundlePath(String configPath)
        {
            var wwwroot = this.options.WebRootRelativePath ?? "wwwroot/";
            if (!configPath.StartsWith(wwwroot))
            {
                throw new InvalidOperationException("Both bundles output and input files must be located in web root directory");
            }

            var trimmedPath = configPath.Substring(wwwroot.Length);
            var logicalPath = "/" + trimmedPath;
            var file = this.hostingEnvironment.WebRootFileProvider.GetFileInfo(logicalPath);

            return new BundlePath(logicalPath, file);
        }

        private async Task<BundlesConfiguration> ReadConfigurationAsync()
        {
            var configPath = this.options.Path ?? "bundles.json";
            var configFile = this.hostingEnvironment.ContentRootFileProvider.GetFileInfo(configPath);
            if (configFile.IsDirectory)
            {
                throw new InvalidOperationException("Configuration file is a directory");
            }

            if (!configFile.Exists)
            {
                throw new InvalidOperationException("Configuration file not found");
            }

            String configContent;
            await using (var configStream = configFile.CreateReadStream())
            {
                using var reader = new StreamReader(configStream);
                configContent = await reader.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<BundlesConfiguration>(configContent);
        }
    }
}
