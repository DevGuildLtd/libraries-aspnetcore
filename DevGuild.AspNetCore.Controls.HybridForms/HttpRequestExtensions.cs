using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DevGuild.AspNetCore.Controls.HybridForms
{
    internal static class HttpRequestExtensions
    {
        public static Boolean IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"HttpRequest is not available");
            }

            if (request.Headers != null && request.Headers.TryGetValue("X-Requested-With", out var requestedWith))
            {
                return requestedWith == "XMLHttpRequest";
            }

            return false;
        }
    }
}
