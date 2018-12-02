using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Represents implementation of the string identity key decoder.
    /// </summary>
    /// <seealso cref="IIdentityKeyDecoder{T}" />
    public class IdentityStringKeyDecoder<TUser> : IIdentityKeyDecoder<String>
        where TUser: IdentityUser<String>
    {
        private readonly UserManager<TUser> userManager;

        public IdentityStringKeyDecoder(UserManager<TUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <inheritdoc />
        public String DecodeUserId(ClaimsPrincipal principal)
        {
            return this.userManager.GetUserId(principal);
        }
    }
}
