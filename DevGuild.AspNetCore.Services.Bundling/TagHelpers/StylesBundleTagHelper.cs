using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevGuild.AspNetCore.Services.Bundling.TagHelpers
{
    [HtmlTargetElement("styles-bundle")]
    public class StylesBundleTagHelper : ITagHelper, ITagHelperComponent
    {
        private readonly IBundlingService bundlingService;

        public StylesBundleTagHelper(IBundlingService bundlingService)
        {
            this.bundlingService = bundlingService;
        }

        public Int32 Order { get; } = 0;

        public String Path { get; set; }

        public void Init(TagHelperContext context)
        {
        }

        public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            output.Content.AppendHtml(await this.bundlingService.RenderStylesBundleAsync(this.Path));
        }
    }
}
