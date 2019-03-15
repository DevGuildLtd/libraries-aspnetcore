using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DevGuild.AspNetCore.Testing.Identity
{
    public class MockUser<TUser, TKey>
    {
        public MockUser(TKey userId, TUser user, ClaimsPrincipal principal)
        {
            this.UserId = userId;
            this.User = user;
            this.Principal = principal;
        }

        public TKey UserId { get; }

        public TUser User { get; }

        public ClaimsPrincipal Principal { get; }
    }
}
