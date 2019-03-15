using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Testing
{
    public class MockEnvironmentBase : IMockEnvironment
    {
        private readonly ServiceCollection serviceCollection;
        private readonly List<IServiceScope> serviceScopes;

        private ServiceProvider serviceProvider;
        private IServiceScopeFactory serviceScopeFactory;

        public MockEnvironmentBase()
        {
            this.serviceCollection = new ServiceCollection();
            this.serviceScopes = new List<IServiceScope>();
        }

        public virtual IMockRequest BeginRequest()
        {
            return new MockRequestBase(this.CreateServiceScope());
        }

        public virtual void Configure(IMockConfiguration configuration)
        {
            configuration.ConfigureServices(this.serviceCollection);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                this.serviceProvider?.Dispose();
            }
        }

        protected IServiceScope CreateServiceScope()
        {
            if (this.serviceProvider == null)
            {
                this.serviceProvider = this.serviceCollection.BuildServiceProvider();
            }

            if (this.serviceScopeFactory == null)
            {
                this.serviceScopeFactory = this.serviceProvider.GetService<IServiceScopeFactory>();
            }

            var scope = this.serviceScopeFactory.CreateScope();
            this.serviceScopes.Add(scope);
            return scope;
        }
    }
}
