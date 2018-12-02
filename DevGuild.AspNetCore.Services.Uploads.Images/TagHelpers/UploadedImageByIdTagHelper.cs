using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevGuild.AspNetCore.Services.Uploads.Images.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "uploaded-id,uploaded-variation")]
    public class UploadedImageByIdTagHelper : ITagHelper, ITagHelperComponent
    {
        private readonly IImageUploadService imageUploadService;

        public UploadedImageByIdTagHelper(IImageUploadService imageUploadService)
        {
            this.imageUploadService = imageUploadService;
        }

        public Int32 Order { get; } = 0;

        public Guid? UploadedId { get; set; }

        public String UploadedVariation { get; set; }

        public void Init(TagHelperContext context)
        {
        }

        public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("uploaded-id");
            output.Attributes.RemoveAll("uploaded-variation");

            if (output.Attributes.ContainsName("src"))
            {
                return;
            }

            var src = await this.imageUploadService.GetImageUrlAsync(this.UploadedId, this.UploadedVariation);
            output.Attributes.Add("src", src);
        }
    }
}
