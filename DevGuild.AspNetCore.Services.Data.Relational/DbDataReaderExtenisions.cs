using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data.Relational
{
    public static class DbDataReaderExtenisions
    {
        public static Task<Boolean> ReadAsync(this IDataReader reader)
        {
            if (reader is DbDataReader dbReader)
            {
                return dbReader.ReadAsync();
            }
            else
            {
                return Task.FromResult(reader.Read());
            }
        }
    }
}
