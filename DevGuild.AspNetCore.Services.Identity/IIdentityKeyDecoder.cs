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
        /// Decodes the user identifier.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns>Decoded key.</returns>
        TKey DecodeUserId(ClaimsPrincipal principal);
    }
}
