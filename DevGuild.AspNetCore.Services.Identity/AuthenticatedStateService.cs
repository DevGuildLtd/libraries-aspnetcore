using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Represents implementation of authentication state services.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="IAuthenticationStatusService" />
    /// <seealso cref="IPrincipalUserAccessorService" />
    /// <seealso cref="IAuthenticatedUserIdAccessorService{TKey}" />
    /// <seealso cref="IAuthenticatedUserAccessorService{TUser}" />
    public class AuthenticatedStateService<TUser, TKey> : IAuthenticationStatusService, IPrincipalUserAccessorService, IAuthenticatedUserIdAccessorService<TKey>, IAuthenticatedUserAccessorService<TUser>
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<TUser> userManager;
        private readonly IIdentityKeyDecoder<TKey> identityKeyDecoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedStateService{TUser, TKey}" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="identityKeyDecoder">The identity key decoder.</param>
        public AuthenticatedStateService(IHttpContextAccessor httpContextAccessor, UserManager<TUser> userManager, IIdentityKeyDecoder<TKey> identityKeyDecoder)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.identityKeyDecoder = identityKeyDecoder;
        }

        /// <summary>
        /// Gets the HTTP context accessor.
        /// </summary>
        /// <value>
        /// The HTTP context accessor.
        /// </value>
        protected IHttpContextAccessor HttpContextAccessor => this.httpContextAccessor;

        /// <summary>
        /// Gets the user manger.
        /// </summary>
        /// <value>
        /// The user manger.
        /// </value>
        protected UserManager<TUser> UserManger => this.userManager;

        /// <summary>
        /// Gets the identity key decoder.
        /// </summary>
        /// <value>
        /// The identity key decoder.
        /// </value>
        protected IIdentityKeyDecoder<TKey> IdentityKeyDecoder => this.identityKeyDecoder;

        /// <inheritdoc />
        public Task<Boolean> GetAuthenticationStatusAsync()
        {
            return Task.FromResult(this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated);
        }

        /// <inheritdoc />
        public Task<ClaimsPrincipal> GetPrincipalUserAsync()
        {
            return this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? Task.FromResult<ClaimsPrincipal>(this.httpContextAccessor.HttpContext.User)
                : Task.FromResult<ClaimsPrincipal>(null);
        }

        /// <inheritdoc />
        public Task<TKey> GetUserIdAsync()
        {
            if (this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return Task.FromResult(this.IdentityKeyDecoder.DecodeUserId(this.httpContextAccessor.HttpContext.User));
            }

            throw new InvalidOperationException("User is not authenticated");
        }

        /// <inheritdoc />
        public Task<TUser> GetUserAsync()
        {
            return this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated
                ? this.UserManger.GetUserAsync(this.httpContextAccessor.HttpContext.User)
                : Task.FromResult<TUser>(null);
        }
    }
}
