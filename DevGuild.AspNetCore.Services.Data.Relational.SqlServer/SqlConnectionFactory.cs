using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DevGuild.AspNetCore.Services.Data.Relational.SqlServer
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly SqlConnectionFactoryOptions options;

        public SqlConnectionFactory(IOptions<SqlConnectionFactoryOptions> options)
        {
            this.options = options.Value;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(this.options.ConnectionString);
        }
    }
}
