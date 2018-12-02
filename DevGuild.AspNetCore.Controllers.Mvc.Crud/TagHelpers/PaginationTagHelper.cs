using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.TagHelpers
{
    [HtmlTargetElement("pagination")]
    public class PaginationTagHelper : ITagHelper, ITagHelperComponent
    {
        private const String PreviousIconHtml = "&laquo;";
        private const String NextIconHtml = "&raquo;";

        public Int32 Order { get; } = 0;

        public IPaginationInfo Info { get; set; }

        public Func<Int32, String> PageUrlGenerator { get; set; }

        public void Init(TagHelperContext context)
        {
        }

        public Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.Content.Clear();
            output.Content.AppendHtml(this.CreatePaginationList());
            return Task.CompletedTask;
        }

        private IHtmlContent CreatePaginationList()
        {
            var paginationList = new TagBuilder("ul");
            paginationList.AddCssClass("pagination");

            if (this.Info.TotalItems == 0)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("disabled", this.CreateIconLink(false, null, PaginationTagHelper.PreviousIconHtml)));
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("active", this.CreateTextLink(true, this.GeneratePageUrl(1), "1", true)));
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("disabled", this.CreateIconLink(false, null, PaginationTagHelper.NextIconHtml)));
                return paginationList;
            }

            // Render Previous button
            if (this.Info.CurrentPage == 1)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("disabled", this.CreateIconLink(false, null, PaginationTagHelper.PreviousIconHtml)));
            }
            else
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem(String.Empty, this.CreateIconLink(true, this.GeneratePageUrl(this.Info.CurrentPage - 1), PaginationTagHelper.PreviousIconHtml)));
            }

            // Render pages in following way: 1 2 3 ... 17 18 CURRENT 20 21 ... 32 33 34
            var firstPageBlockEnd = 1 + 2;
            var currentPageBlockStart = this.Info.CurrentPage - 2;
            var currentPageBlockEnd = this.Info.CurrentPage + 2;
            var lastPageBlockStart = this.Info.TotalPages - 2;

            Int32 i;
            for (i = 1; i <= Math.Min(firstPageBlockEnd, this.Info.TotalPages); i++)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem(
                    i == this.Info.CurrentPage ? "active" : String.Empty,
                    this.CreateTextLink(true, this.GeneratePageUrl(i), i.ToString(), i == this.Info.CurrentPage)));
            }

            if (i < currentPageBlockStart)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("disabled", this.CreateTextLink(false, null, "…", false)));
                i = currentPageBlockStart;
            }

            for (; i <= Math.Min(currentPageBlockEnd, this.Info.TotalPages); i++)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem(
                    i == this.Info.CurrentPage ? "active" : String.Empty,
                    this.CreateTextLink(true, this.GeneratePageUrl(i), i.ToString(), i == this.Info.CurrentPage)));
            }

            if (i < lastPageBlockStart)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("disabled", this.CreateTextLink(false, null, "…", false)));
                i = lastPageBlockStart;
            }

            for (; i <= this.Info.TotalPages; i++)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem(
                    i == this.Info.CurrentPage ? "active" : String.Empty,
                    this.CreateTextLink(true, this.GeneratePageUrl(i), i.ToString(), i == this.Info.CurrentPage)));
            }

            // Render Next button
            if (this.Info.CurrentPage >= this.Info.TotalPages)
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem("disabled", this.CreateIconLink(false, null, PaginationTagHelper.NextIconHtml)));
            }
            else
            {
                paginationList.InnerHtml.AppendHtml(this.CreatePageItem(String.Empty, this.CreateIconLink(true, this.GeneratePageUrl(this.Info.CurrentPage + 1), PaginationTagHelper.NextIconHtml)));
            }

            return paginationList;
        }

        private IHtmlContent CreatePageItem(String customClass, IHtmlContent body)
        {
            var tagBuilder = new TagBuilder("li");
            tagBuilder.AddCssClass("page-item");

            if (!String.IsNullOrEmpty(customClass))
            {
                tagBuilder.AddCssClass(customClass);
            }

            tagBuilder.InnerHtml.AppendHtml(body);

            return tagBuilder;
        }

        private IHtmlContent CreateIconLink(Boolean enabled, String href, String iconHtml)
        {
            var tagBuilder = new TagBuilder(enabled ? "a" : "span");
            tagBuilder.AddCssClass("page-link");
            if (enabled)
            {
                tagBuilder.Attributes.Add("href", href);
            }

            var icon = new TagBuilder("span");
            icon.Attributes.Add("aria-hidden", "true");
            icon.InnerHtml.AppendHtml(iconHtml);

            var screenReader = new TagBuilder("span");
            screenReader.AddCssClass("sr-only");
            screenReader.InnerHtml.Append("Previous");

            tagBuilder.InnerHtml.AppendHtml(icon);
            tagBuilder.InnerHtml.AppendHtml(screenReader);

            return tagBuilder;
        }

        private IHtmlContent CreateTextLink(Boolean enabled, String href, String text, Boolean isCurrent)
        {
            var tagBuilder = new TagBuilder(enabled ? "a" : "span");
            tagBuilder.AddCssClass("page-link");
            if (enabled)
            {
                tagBuilder.Attributes.Add("href", href);
            }

            tagBuilder.InnerHtml.AppendHtml(text);

            if (isCurrent)
            {
                var screenReader = new TagBuilder("span");
                screenReader.AddCssClass("sr-only");
                screenReader.InnerHtml.Append("(current)");

                tagBuilder.InnerHtml.AppendHtml(screenReader);
            }

            return tagBuilder;
        }

        private String GeneratePageUrl(Int32 page)
        {
            return this.PageUrlGenerator(page);
        }
    }
}
