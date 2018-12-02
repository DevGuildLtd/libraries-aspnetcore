using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace DevGuild.AspNetCore.Services.Bundling
{
    public class BundlingService : IBundlingService
    {
        private readonly IBundlingConfigurationService configurationService;

        public BundlingService(IBundlingConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public async Task<IHtmlContent> RenderStylesBundleAsync(String bundlePath)
        {
            await this.configurationService.InitializeAsync();
            if (!this.configurationService.StylesBundles.TryGetValue(bundlePath, out var bundle))
            {
                throw new InvalidOperationException($"Bundle '{bundlePath}' was not found");
            }

            var sb = new StringBuilder();
            if (this.configurationService.Enabled)
            {
                sb.Append(this.RenderStyleTag(bundle.Output.GetHashedPath()));
            }
            else
            {
                foreach (var input in bundle.Input)
                {
                    sb.Append(this.RenderStyleTag(input.GetHashedPath()));
                }
            }

            return new HtmlString(sb.ToString());
        }

        public async Task<IHtmlContent> RenderScriptsBundleAsync(String bundlePath)
        {
            await this.configurationService.InitializeAsync();
            if (!this.configurationService.ScriptsBundles.TryGetValue(bundlePath, out var bundle))
            {
                throw new InvalidOperationException($"Bundle '{bundlePath}' was not found");
            }

            var sb = new StringBuilder();
            if (this.configurationService.Enabled)
            {
                sb.Append(this.RenderScriptTag(bundle.Output.GetHashedPath()));
            }
            else
            {
                foreach (var input in bundle.Input)
                {
                    sb.Append(this.RenderScriptTag(input.GetHashedPath()));
                }
            }

            return new HtmlString(sb.ToString());
        }

        private String RenderScriptTag(String path)
        {
            return $"<script type=\"text/javascript\" src=\"{HtmlEncoder.Default.Encode(path)}\"></script>";
        }

        private String RenderStyleTag(String path)
        {
            return $"<link rel=\"stylesheet\" href=\"{HtmlEncoder.Default.Encode(path)}\" />";
        }
    }
}
