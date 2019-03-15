using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Identity;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Testing.Identity
{
    public class MockAuthenticationStateNullableKeyService<TUser, TKey> : MockAuthenticationStateService<TUser, TKey>, IAuthenticatedUserIdAccessorService<TKey?>
        where TUser : IdentityUser<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public MockAuthenticationStateNullableKeyService(UserManager<TUser> userManager, IUserClaimsPrincipalFactory<TUser> claimsPrincipalFactory) : base(userManager, claimsPrincipalFactory)
        {
        }

        Task<TKey?> IAuthenticatedUserIdAccessorService<TKey?>.GetUserIdAsync()
        {
            return Task.FromResult<TKey?>(this.User.UserId);
        }
    }
}
