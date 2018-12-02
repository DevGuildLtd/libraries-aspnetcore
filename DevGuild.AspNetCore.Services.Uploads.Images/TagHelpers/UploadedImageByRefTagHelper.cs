using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Uploads.Images.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevGuild.AspNetCore.Services.Uploads.Images.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "uploaded-ref,uploaded-variation")]
    public class UploadedImageByRefTagHelper : ITagHelper, ITagHelperComponent
    {
        private readonly IImageUploadService imageUploadService;

        public UploadedImageByRefTagHelper(IImageUploadService imageUploadService)
        {
            this.imageUploadService = imageUploadService;
        }

        public Int32 Order { get; } = 0;

        public UploadedImage UploadedRef { get; set; }

        public String UploadedVariation { get; set; }

        public void Init(TagHelperContext context)
        {
        }

        public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("uploaded-ref");
            output.Attributes.RemoveAll("uploaded-variation");

            if (output.Attributes.ContainsName("src"))
            {
                return;
            }

            var src = await this.imageUploadService.GetImageUrlAsync(this.UploadedRef, this.UploadedVariation);
            output.Attributes.Add("src", src);
        }
    }
}
