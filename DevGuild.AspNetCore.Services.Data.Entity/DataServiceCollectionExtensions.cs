using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    public static class DataServiceCollectionExtensions
    {
        public static void AddEntityDataServices<TDbContext>(this IServiceCollection services, Func<DbContextOptions<TDbContext>, TDbContext> contextFactory)
            where TDbContext : DbContext
        {
            services.AddSingleton<IDbContextFactory<TDbContext>>(provider => new DbContextFactory<TDbContext>(() => contextFactory(provider.GetService<DbContextOptions<TDbContext>>())));
            services.AddSingleton<IDbContextFactory>(provider => provider.GetService<IDbContextFactory<TDbContext>>());

            services.AddSingleton<IRepositoryFactory, EntityFrameworkRepositoryFactory>();

            services.AddScoped<DbContext>(provider => provider.GetService<TDbContext>());
            services.AddScoped<IRepository, EntityFrameworkRepository>();
        }
    }
}
