using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Represents implementation of the authentication state related services that supports nullable keys.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="AuthenticatedStateService{TUser, TKey}" />
    public class AuthenticatedStateNullableKeyService<TUser, TKey> : AuthenticatedStateService<TUser, TKey>, IAuthenticatedUserIdAccessorService<TKey?>
        where TUser : IdentityUser<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        private readonly IIdentityKeyDecoder<TKey?> nullableIdentityKeyDecoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedStateNullableKeyService{TUser, TKey}" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="identityKeyDecoder">The identity key decoder.</param>
        /// <param name="nullableIdentityKeyDecoder">The nullable identity key decoder.</param>
        public AuthenticatedStateNullableKeyService(IHttpContextAccessor httpContextAccessor, UserManager<TUser> userManager, IIdentityKeyDecoder<TKey> identityKeyDecoder, IIdentityKeyDecoder<TKey?> nullableIdentityKeyDecoder)
            : base(httpContextAccessor, userManager, identityKeyDecoder)
        {
            this.nullableIdentityKeyDecoder = nullableIdentityKeyDecoder;
        }

        /// <summary>
        /// Gets the nullable identity key decoder.
        /// </summary>
        /// <value>
        /// The nullable identity key decoder.
        /// </value>
        protected IIdentityKeyDecoder<TKey?> NullableIdentityKeyDecoder => this.nullableIdentityKeyDecoder;

        /// <inheritdoc />
        Task<TKey?> IAuthenticatedUserIdAccessorService<TKey?>.GetUserIdAsync()
        {
            if (this.HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return Task.FromResult(this.nullableIdentityKeyDecoder.DecodeUserId(this.HttpContextAccessor.HttpContext.User));
            }

            throw new InvalidOperationException("User is not authenticated");
        }
    }
}
