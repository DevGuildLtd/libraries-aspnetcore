using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Testing.Identity
{
    public static class MockIdentityServiceCollectionExtensions
    {
        public static IdentityBuilder MockStateServicesGuidKey<TUser>(this IdentityBuilder builder)
            where TUser : IdentityUser<Guid>
        {
            builder.Services.AddScoped<IdentityGuidKeyDecoder<TUser>>();
            builder.Services.AddScoped<IIdentityKeyDecoder<Guid>>(provider => provider.GetService<IdentityGuidKeyDecoder<TUser>>());
            builder.Services.AddScoped<IIdentityKeyDecoder<Guid?>>(provider => provider.GetService<IdentityGuidKeyDecoder<TUser>>());

            builder.Services.AddScoped<MockAuthenticationStateNullableKeyService<TUser, Guid>>();
            builder.Services.AddScoped<IAuthenticationStatusService>(provider => provider.GetService<MockAuthenticationStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IPrincipalUserAccessorService>(provider => provider.GetService<MockAuthenticationStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IAuthenticatedUserIdAccessorService<Guid>>(provider => provider.GetService<MockAuthenticationStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IAuthenticatedUserIdAccessorService<Guid?>>(provider => provider.GetService<MockAuthenticationStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IAuthenticatedUserAccessorService<TUser>>(provider => provider.GetService<MockAuthenticationStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IMockAuthenticationStateService>(provider => provider.GetService<MockAuthenticationStateNullableKeyService<TUser, Guid>>());

            return builder;
        }

        public static IdentityBuilder MockStateServicesStringKey<TUser>(this IdentityBuilder builder)
            where TUser : IdentityUser<String>
        {
            builder.Services.AddScoped<IdentityStringKeyDecoder<TUser>>();
            builder.Services.AddScoped<IIdentityKeyDecoder<String>>(provider => provider.GetService<IdentityStringKeyDecoder<TUser>>());

            builder.Services.AddScoped<MockAuthenticationStateService<TUser, String>>();
            builder.Services.AddScoped<IAuthenticationStatusService>(provider => provider.GetService<MockAuthenticationStateService<TUser, String>>());
            builder.Services.AddScoped<IPrincipalUserAccessorService>(provider => provider.GetService<MockAuthenticationStateService<TUser, String>>());
            builder.Services.AddScoped<IAuthenticatedUserIdAccessorService<String>>(provider => provider.GetService<MockAuthenticationStateService<TUser, String>>());
            builder.Services.AddScoped<IAuthenticatedUserAccessorService<TUser>>(provider => provider.GetService<MockAuthenticationStateService<TUser, String>>());
            builder.Services.AddScoped<IMockAuthenticationStateService>(provider => provider.GetService<MockAuthenticationStateService<TUser, String>>());

            return builder;
        }
    }
}
