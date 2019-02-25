using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Data;
using Microsoft.Extensions.Logging;

namespace DevGuild.AspNetCore.Services.Logging.Data
{
    [ProviderAlias("Repository")]
    public class RepositoryLoggerProvider : ILoggerProvider
    {
        private readonly Func<String, LogLevel, Boolean> filter;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IRequestInformationProvider requestInformationProvider;

        public RepositoryLoggerProvider(Func<String, LogLevel, Boolean> filter, IRepositoryFactory repositoryFactory, IRequestInformationProvider requestInformationProvider)
        {
            this.filter = filter;
            this.repositoryFactory = repositoryFactory;
            this.requestInformationProvider = requestInformationProvider;
        }

        public ILogger CreateLogger(String categoryName)
        {
            return new RepositoryLogger(categoryName, this.filter, this.repositoryFactory, this.requestInformationProvider);
        }

        public void Dispose()
        {
        }
    }
}
