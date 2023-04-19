using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Defines interface for identity key decoders.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IIdentityKeyDecoder<out TKey>
    {
        /// <summary>
        /// Decodes the user identifier from provided claims principal.
        /// </summary>
        /// <param name="principal">The claims principal.</param>
        /// <returns>Decoded key.</returns>
        TKey DecodeUserId(ClaimsPrincipal principal);
    }
}
