using System;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Testing
{
    public class MockRequestBase : IMockRequest
    {
        private readonly IServiceScope serviceScope;
        private readonly IServiceProvider serviceProvider;

        public MockRequestBase(IServiceScope serviceScope)
        {
            this.serviceScope = serviceScope;
            this.serviceProvider = this.serviceScope.ServiceProvider;
        }

        public T GetService<T>()
        {
            return this.serviceProvider.GetService<T>();
        }

        public Object GetService(Type type)
        {
            return this.serviceProvider.GetService(type);
        }

        public T CreateInstanceOf<T>()
        {
            return ActivatorUtilities.CreateInstance<T>(this.serviceProvider);
        }

        public Object CreateInstanceOf(Type type)
        {
            return ActivatorUtilities.CreateInstance(this.serviceProvider, type);
        }

        public void Dispose()
        {
            this.serviceScope?.Dispose();
        }
    }
}
