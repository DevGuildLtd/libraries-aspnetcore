using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace DevGuild.AspNetCore.Controls.HybridForms
{
    [HtmlTargetElement("form", Attributes = "hybrid-form-id")]
    public class HybridFormTagHelper : ITagHelper, ITagHelperComponent
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HybridFormsOptions options;

        public HybridFormTagHelper(IHttpContextAccessor httpContextAccessor, IOptions<HybridFormsOptions> options)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.options = options?.Value;
        }

        public Int32 Order => 1000;

        public String HybridFormId { get; set; }

        public void Init(TagHelperContext context)
        {
        }

        public Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("hybrid-form-id");

            if (this.httpContextAccessor.HttpContext.Request.IsAjaxRequest())
            {
                output.Attributes.Add("data-ajax", "true");
                output.Attributes.Add("data-ajax-begin", this.options.DefaultIfNull().BeginRequestHandler);
                output.Attributes.Add("data-ajax-complete", this.options.DefaultIfNull().CompleteRequestHandler);
                output.Attributes.Add("data-ajax-mode", "replace-with");
                output.Attributes.Add("data-ajax-update", $"#{this.HybridFormId}");
            }

            return Task.CompletedTask;
        }
    }
}
