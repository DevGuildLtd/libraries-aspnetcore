using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Testing
{
    public interface IMockConfiguration
    {
        void ConfigureServices(IServiceCollection services);
    }
}
