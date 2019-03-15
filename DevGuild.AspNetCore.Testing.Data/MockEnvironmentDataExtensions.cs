using System;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Testing.Data
{
    public static class MockEnvironmentDataExtensions
    {
        public static async Task SeedAsync(this IMockEnvironment environment, Func<DbSeedContext<DbContext>, Task> seedMethod)
        {
            using (var request = environment.BeginRequest())
            {
                var serviceProvider = request.GetService<IServiceProvider>();
                var repository = request.GetService<IRepository>();
                var seeder = new InlineDbSeed(serviceProvider, seedMethod);
                await seeder.SeedAsync();
                await repository.SaveChangesAsync();
            }
        }

        private class InlineDbSeed : DbSeed<DbContext>
        {
            private readonly Func<DbSeedContext<DbContext>, Task> seedMethod;

            public InlineDbSeed(IServiceProvider serviceProvider, Func<DbSeedContext<DbContext>, Task> seedMethod)
                : base(serviceProvider)
            {
                this.seedMethod = seedMethod;
            }

            public override Task SeedAsync()
            {
                return this.seedMethod(this.Context);
            }
        }
    }
}
