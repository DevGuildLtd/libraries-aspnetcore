using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Data.Relational.SqlServer
{
    public static class SqlServiceCollectionExtensions
    {
        public static void AddSqlConnectionFactory(this IServiceCollection services, Action<SqlConnectionFactoryOptions> configuration)
        {
            services.Configure<SqlConnectionFactoryOptions>(configuration);
            services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
        }
    }
}
