using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DevGuild.AspNetCore.Services.Logging.Data
{
    public class RepositoryLogger : ILogger
    {
        private readonly String categoryName;
        private readonly Func<String, LogLevel, Boolean> filter;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IRequestInformationProvider requestInformationProvider;
        private readonly IExternalScopeProvider scopeProvider;

        public RepositoryLogger(String categoryName, Func<String, LogLevel, Boolean> filter, IRepositoryFactory repositoryFactory, IRequestInformationProvider requestInformationProvider)
        {
            this.categoryName = categoryName;
            this.filter = filter;
            this.repositoryFactory = repositoryFactory;
            this.requestInformationProvider = requestInformationProvider;

            this.scopeProvider = new LoggerExternalScopeProvider();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, String> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);
            this.PostMessage(logLevel, eventId, message, exception);
        }

        public Boolean IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.None)
            {
                return false;
            }

            if (this.filter == null)
            {
                return true;
            }

            return this.filter(this.categoryName, logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this.scopeProvider.Push(state);
        }

        private void PostMessage(LogLevel logLevel, EventId eventId, String message, Exception exception)
        {
            var requestInformation = this.requestInformationProvider.GetRequestInformation();
            var loggedEvent = new LoggedEvent
            {
                Timestamp = DateTime.UtcNow,

                RequestHost = RepositoryLogger.LimitString(requestInformation.RequestHost, 300),
                RequestMethod = RepositoryLogger.LimitString(requestInformation.RequestMethod, 10, false),
                RequestAddress = RepositoryLogger.LimitString(requestInformation.RequestAddress, 1024),
                UserAddress = RepositoryLogger.LimitString(requestInformation.UserAddress, 40, false),
                UserAgent = RepositoryLogger.LimitString(requestInformation.UserAgent, 512),
                UserName = RepositoryLogger.LimitString(requestInformation.UserName, 512),
                RequestId = RepositoryLogger.LimitString(requestInformation.RequestId, 1024),

                LogLevel = RepositoryLogger.LogLevelToString(logLevel),
                Category = RepositoryLogger.LimitString(this.categoryName, 512),
                EventId = eventId.Id,
                EventName = RepositoryLogger.LimitString(eventId.Name, 256),
                EventScope = this.GetScope(),
                EventMessage = message,

                ExceptionClass = exception?.GetType().FullName,
                ExceptionMessage = exception?.Message,
                ExceptionDetails = exception?.ToString(),
            };

            Task.Run(() => this.PostMessageAsync(loggedEvent));
        }

        private async Task PostMessageAsync(LoggedEvent loggedEvent)
        {
            using (var repository = this.repositoryFactory.CreateRepository())
            {
                await repository.InsertAsync(loggedEvent);
                await repository.SaveChangesAsync();
            }
        }

        private String GetScope()
        {
            var sb = new StringBuilder();
            this.scopeProvider.ForEachScope<StringBuilder>(
                (state, builder) =>
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(" => ");
                    }

                    builder.Append(state);
                },
                sb);

            return sb.ToString();
        }

        private static String LimitString(String str, Int32 limit, Boolean addEllipsis = true)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }

            if (str.Length <= limit)
            {
                return str;
            }

            if (addEllipsis)
            {
                return str.Substring(0, limit - 3) + "...";
            }

            return str.Substring(0, limit);
        }

        private static String LogLevelToString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return "TRACE";
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Information:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARN";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Critical:
                    return "CRIT";
                case LogLevel.None:
                    return "NONE";
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}
