using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DevGuild.AspNetCore.Controls.HybridForms
{
    public static class HybridPageExtensions
    {
        public static void ConfigureHybridPage(this RazorPage page, String pageId, String normalLayout)
        {
            if (page.Context.Request.IsAjaxRequest())
            {
                page.ViewBag.HybridPageId = pageId;
                page.Layout = page.Context.RequestServices.GetService<IOptions<HybridFormsOptions>>()?.Value.DefaultIfNull().AjaxPageLayout;
            }
            else
            {
                page.Layout = normalLayout;
            }
        }

        public static void ConfigureHybridPage(this RazorPage page, String pageId)
        {
            if (page.Context.Request.IsAjaxRequest())
            {
                page.ViewBag.HybridPageId = pageId;
                page.Layout = page.Context.RequestServices.GetService<IOptions<HybridFormsOptions>>()?.Value.DefaultIfNull().AjaxPageLayout;
            }
        }

        public static IActionResult HybridFormResult(this Controller controller, String pageId, IActionResult normalResult)
        {
            if (controller.Request.IsAjaxRequest())
            {
                return controller.Json(new
                {
                    formId = pageId,
                    result = new Object()
                });
            }

            return normalResult;
        }

        public static IActionResult HybridFormResult(this Controller controller, String pageId, Object result, IActionResult normalResult)
        {
            if (controller.Request.IsAjaxRequest())
            {
                return controller.Json(new
                {
                    formId = pageId,
                    result = result
                });
            }

            return normalResult;
        }

        public static Task<IActionResult> HybridFormResultAsync(this Controller controller, String pageId, IActionResult normalResult)
        {
            return Task.FromResult(controller.HybridFormResult(pageId, normalResult));
        }

        public static Task<IActionResult> HybridFormResultAsync(this Controller controller, String pageId, Object result, IActionResult normalResult)
        {
            return Task.FromResult(controller.HybridFormResult(pageId, result, normalResult));
        }
    }
}
