using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Testing
{
    public interface IMockRequest : IDisposable
    {
        T GetService<T>();

        Object GetService(Type type);

        T CreateInstanceOf<T>();

        Object CreateInstanceOf(Type type);
    }
}
