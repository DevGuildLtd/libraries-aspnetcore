using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data.Relational
{
    public static class DbCommandExtensions
    {
        public static Task<Int32> ExecuteNonQueryAsync(this IDbCommand command)
        {
            if (command is DbCommand dbCommand)
            {
                return dbCommand.ExecuteNonQueryAsync();
            }
            else
            {
                var result = command.ExecuteNonQuery();
                return Task.FromResult(result);
            }
        }

        public static Task<T> ExecuteScalarAsync<T>(this IDbCommand command)
        {
            if (command is DbCommand dbCommand)
            {
                return dbCommand.ExecuteScalarAsync().Then(x => (T)x);
            }
            else
            {
                var result = command.ExecuteScalar();
                return Task.FromResult<T>((T)result);
            }
        }

        public static Task<IDataReader> ExecuteReaderAsync(this IDbCommand command)
        {
            if (command is DbCommand dbCommand)
            {
                return dbCommand.ExecuteReaderAsync().Then(x => x as IDataReader);
            }
            else
            {
                var result = command.ExecuteReader();
                return Task.FromResult(result);
            }
        }

        public static void AddParameter(this IDbCommand command, String name, DbType type, Object value)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;
            command.Parameters.Add(param);
        }
    }
}
