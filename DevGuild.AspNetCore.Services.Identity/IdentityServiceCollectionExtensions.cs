using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Identity
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IdentityBuilder AddStateServicesGuidKey<TUser>(this IdentityBuilder builder)
            where TUser : IdentityUser<Guid>
        {
            builder.Services.AddScoped<IdentityGuidKeyDecoder<TUser>>();
            builder.Services.AddScoped<IIdentityKeyDecoder<Guid>>(provider => provider.GetService<IdentityGuidKeyDecoder<TUser>>());
            builder.Services.AddScoped<IIdentityKeyDecoder<Guid?>>(provider => provider.GetService<IdentityGuidKeyDecoder<TUser>>());

            builder.Services.AddScoped<AuthenticatedStateNullableKeyService<TUser, Guid>>();
            builder.Services.AddScoped<IAuthenticationStatusService>(provider => provider.GetService<AuthenticatedStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IPrincipalUserAccessorService>(provider => provider.GetService<AuthenticatedStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IAuthenticatedUserIdAccessorService<Guid>>(provider => provider.GetService<AuthenticatedStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IAuthenticatedUserIdAccessorService<Guid?>>(provider => provider.GetService<AuthenticatedStateNullableKeyService<TUser, Guid>>());
            builder.Services.AddScoped<IAuthenticatedUserAccessorService<TUser>>(provider => provider.GetService<AuthenticatedStateNullableKeyService<TUser, Guid>>());

            return builder;
        }

        public static IdentityBuilder AddStateServicesStringKey<TUser>(this IdentityBuilder builder)
            where TUser : IdentityUser<String>
        {
            builder.Services.AddScoped<IdentityStringKeyDecoder<TUser>>();
            builder.Services.AddScoped<IIdentityKeyDecoder<String>>(provider => provider.GetService<IdentityStringKeyDecoder<TUser>>());

            builder.Services.AddScoped<AuthenticatedStateService<TUser, String>>();
            builder.Services.AddScoped<IAuthenticationStatusService>(provider => provider.GetService<AuthenticatedStateService<TUser, String>>());
            builder.Services.AddScoped<IPrincipalUserAccessorService>(provider => provider.GetService<AuthenticatedStateService<TUser, String>>());
            builder.Services.AddScoped<IAuthenticatedUserIdAccessorService<String>>(provider => provider.GetService<AuthenticatedStateService<TUser, String>>());
            builder.Services.AddScoped<IAuthenticatedUserAccessorService<TUser>>(provider => provider.GetService<AuthenticatedStateService<TUser, String>>());

            return builder;
        }
    }
}
