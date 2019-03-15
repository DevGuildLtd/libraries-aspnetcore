using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Testing.Identity
{
    public static class MockRequestIdentityExtensions
    {
        public static Task SignOutAsync(this IMockRequest request)
        {
            var mockAuth = request.GetService<IMockAuthenticationStateService>();
            return mockAuth.SignOutAsync();
        }

        public static Task SignInAsync(this IMockRequest request, String userName)
        {
            var mockAuth = request.GetService<IMockAuthenticationStateService>();
            return mockAuth.SignInAsync(userName);
        }
    }
}
