using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Identity;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Testing.Identity
{
    public class MockAuthenticationStateService<TUser, TKey> : IAuthenticationStatusService, IPrincipalUserAccessorService, IAuthenticatedUserAccessorService<TUser>, IAuthenticatedUserIdAccessorService<TKey>, IMockAuthenticationStateService
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly UserManager<TUser> userManager;
        private readonly IUserClaimsPrincipalFactory<TUser> claimsPrincipalFactory;
        private MockUser<TUser, TKey> user;

        public MockAuthenticationStateService(UserManager<TUser> userManager, IUserClaimsPrincipalFactory<TUser> claimsPrincipalFactory)
        {
            this.userManager = userManager;
            this.claimsPrincipalFactory = claimsPrincipalFactory;
        }

        protected MockUser<TUser, TKey> User => this.user;

        public Task SignOutAsync()
        {
            this.user = null;
            return Task.CompletedTask;
        }

        public async Task SignInAsync(String userName)
        {
            var appUser = await this.userManager.FindByNameAsync(userName);
            var claimsIdentity = await this.claimsPrincipalFactory.CreateAsync(appUser);
            this.user = new MockUser<TUser, TKey>(appUser.Id, appUser, new ClaimsPrincipal(claimsIdentity));
        }

        public Task<Boolean> GetAuthenticationStatusAsync()
        {
            return Task.FromResult(this.user != null);
        }

        public Task<ClaimsPrincipal> GetPrincipalUserAsync()
        {
            return Task.FromResult(this.user?.Principal ?? new ClaimsPrincipal());
        }

        public Task<TUser> GetUserAsync()
        {
            return Task.FromResult(this.user?.User);
        }

        public Task<TKey> GetUserIdAsync()
        {
            return Task.FromResult(this.user.UserId);
        }
    }
}
