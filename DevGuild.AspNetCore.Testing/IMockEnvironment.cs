using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Testing
{
    public interface IMockEnvironment : IDisposable
    {
        IMockRequest BeginRequest();

        void Configure(IMockConfiguration configuration);
    }
}
