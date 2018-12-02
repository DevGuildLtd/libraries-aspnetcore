using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Controls.HybridForms
{
    internal static class HybridFormsOptionsExtensions
    {
        private static HybridFormsOptions DefaultOptions = new HybridFormsOptions
        {
            AjaxPageLayout = "_AjaxPageLayout",
            BeginRequestHandler = "DevGuild.AspNet.Forms.AjaxForms.AjaxFormsManager.handleBeginRequest",
            CompleteRequestHandler = "DevGuild.AspNet.Forms.AjaxForms.AjaxFormsManager.handleComplete"
        };

        internal static HybridFormsOptions DefaultIfNull(this HybridFormsOptions options)
        {
            if (options == null)
            {
                return new HybridFormsOptions
                {
                    AjaxPageLayout = HybridFormsOptionsExtensions.DefaultOptions.AjaxPageLayout,
                    BeginRequestHandler = HybridFormsOptionsExtensions.DefaultOptions.BeginRequestHandler,
                    CompleteRequestHandler = HybridFormsOptionsExtensions.DefaultOptions.CompleteRequestHandler
                };
            }

            if (!String.IsNullOrEmpty(options.AjaxPageLayout) &&
                !String.IsNullOrEmpty(options.BeginRequestHandler) &&
                !String.IsNullOrEmpty(options.CompleteRequestHandler))
            {
                return options;
            }

            return new HybridFormsOptions
            {
                AjaxPageLayout = !String.IsNullOrEmpty(options.AjaxPageLayout)
                    ? options.AjaxPageLayout
                    : HybridFormsOptionsExtensions.DefaultOptions.AjaxPageLayout,

                BeginRequestHandler = !String.IsNullOrEmpty(options.BeginRequestHandler)
                    ? options.BeginRequestHandler
                    : HybridFormsOptionsExtensions.DefaultOptions.BeginRequestHandler,

                CompleteRequestHandler = !String.IsNullOrEmpty(options.CompleteRequestHandler)
                    ? options.CompleteRequestHandler
                    : HybridFormsOptionsExtensions.DefaultOptions.CompleteRequestHandler,
            };
        }
    }
}
