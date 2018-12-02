using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data.Relational
{
    public static class DbConnectionExtensions
    {
        public static Task EnsureOpenAsync(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                return Task.CompletedTask;
            }

            return connection.OpenAsync();
        }

        public static Task OpenAsync(this IDbConnection connection)
        {
            if (connection is DbConnection dbConnection)
            {
                return dbConnection.OpenAsync();
            }
            else
            {
                connection.Open();
                return Task.CompletedTask;
            }
        }
    }
}
