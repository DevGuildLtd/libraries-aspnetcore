using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Represents implementation of the guid identity key decoder.
    /// </summary>
    /// <seealso cref="IIdentityKeyDecoder{T}" />
    public class IdentityGuidKeyDecoder<TUser> : IIdentityKeyDecoder<Guid>, IIdentityKeyDecoder<Guid?>
        where TUser : IdentityUser<Guid>
    {
        private readonly UserManager<TUser> userManager;

        public IdentityGuidKeyDecoder(UserManager<TUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <inheritdoc />
        public Guid DecodeUserId(ClaimsPrincipal principal)
        {
            var userId = this.userManager.GetUserId(principal);
            if (String.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("UserId is empty");
            }

            if (Guid.TryParse(userId, out var result))
            {
                return result;
            }

            throw new InvalidOperationException("UserId is in invalid format");
        }

        /// <inheritdoc />
        Guid? IIdentityKeyDecoder<Guid?>.DecodeUserId(ClaimsPrincipal principal)
        {
            var userId = this.userManager.GetUserId(principal);
            if (String.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("UserId is empty");
            }

            if (Guid.TryParse(userId, out var result))
            {
                return result;
            }

            throw new InvalidOperationException("UserId is in invalid format");
        }
    }
}
