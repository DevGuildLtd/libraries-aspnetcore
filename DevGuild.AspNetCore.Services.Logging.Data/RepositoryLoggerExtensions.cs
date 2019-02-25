using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace DevGuild.AspNetCore.Services.Logging.Data
{
    public static class RepositoryLoggerExtensions
    {
        public static ILoggingBuilder AddRepositoryLogging(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();
            builder.Services.AddSingleton<ILoggerProvider>(services => new RepositoryLoggerProvider(
                null,
                services.GetService<IRepositoryFactory>(),
                services.GetService<IRequestInformationProvider>()));
            return builder;
        }
    }
}
