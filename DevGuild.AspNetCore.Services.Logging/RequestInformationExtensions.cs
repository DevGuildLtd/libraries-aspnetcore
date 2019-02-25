using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Logging
{
    public static class RequestInformationExtensions
    {
        public static void OverrideRequestInformation(this IServiceScope scope, RequestInformation information)
        {
            var provider = scope.ServiceProvider.GetService<IRequestInformationProvider>();
            provider.OverrideRequestInformation(information);
        }
    }
}
