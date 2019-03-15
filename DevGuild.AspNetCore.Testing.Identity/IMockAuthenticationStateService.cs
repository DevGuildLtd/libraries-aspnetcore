using System;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Testing.Identity
{
    public interface IMockAuthenticationStateService
    {
        Task SignOutAsync();

        Task SignInAsync(String userName);
    }
}
