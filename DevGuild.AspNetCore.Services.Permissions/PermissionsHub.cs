using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Identity;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Represents default implementation of the permissions hub.
    /// </summary>
    /// <seealso cref="IPermissionsHub" />
    public class PermissionsHub : IPermissionsHub
    {
        private readonly Dictionary<String, ICorePermissionsManager> managers = new Dictionary<String, ICorePermissionsManager>();
        private readonly PermissionsHubConfiguration configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly IAuthenticationStatusService authenticationStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsHub"/> class.
        /// </summary>
        /// <param name="configuration">The permissions hub configuration.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="authenticationStatus">The authentication status service.</param>
        public PermissionsHub(PermissionsHubConfiguration configuration, IServiceProvider serviceProvider, IAuthenticationStatusService authenticationStatus)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
            this.authenticationStatus = authenticationStatus;
        }

        /// <inheritdoc />
        public T GetManager<T>(String path)
            where T : ICorePermissionsManager
        {
            if (!this.managers.TryGetValue(path, out var manager))
            {
                manager = this.CreateManager<T>(path);
                this.managers.Add(path, manager);
            }

            return (T)manager;
        }

        /// <inheritdoc />
        public Task<Exception> CreateUnauthorizedExceptionAsync()
        {
            return Task.FromResult<Exception>(new InsufficientPermissionsException());
        }

        private T CreateManager<T>(String path)
            where T : ICorePermissionsManager
        {
            var entry = this.configuration.GetEntry(path);
            if (entry == null)
            {
                throw new InvalidOperationException($"No entry registered for path {path}");
            }

            var parentEntry = this.configuration.GetEntryParent(entry);
            var parent = parentEntry != null ? this.GetManager<ICorePermissionsManager>(parentEntry.Path) : null;

            var manager = entry.Constructor(this, parent, entry.Namespace, this.serviceProvider);
            return (T)manager;
        }
    }
}
