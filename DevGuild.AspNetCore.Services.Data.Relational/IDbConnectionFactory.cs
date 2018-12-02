using System;
using System.Data;

namespace DevGuild.AspNetCore.Services.Data.Relational
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
