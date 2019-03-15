using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Testing
{
    public static class MockEnvironment
    {
        public static IMockEnvironment Create<TEnvironment>(IMockConfiguration configuration)
            where TEnvironment : IMockEnvironment
        {
            var env = Activator.CreateInstance<TEnvironment>();
            env.Configure(configuration);
            return env;
        }
    }
}
