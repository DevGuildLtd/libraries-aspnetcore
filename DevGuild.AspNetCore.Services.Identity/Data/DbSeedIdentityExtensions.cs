using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data.Entity;
using DevGuild.AspNetCore.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Identity.Data
{
    /// <summary>
    /// Contains extensions for the database seed.
    /// </summary>
    public static class DbSeedIdentityExtensions
    {
        #region Roles

        /// <summary>
        /// Asynchronously seeds the roles to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="roles">The roles names.</param>
        /// <returns>A task that represents the operation.</returns>
        public static async Task SeedRolesAsync<TContext, TRole, TKey>(this DbSeedContext<TContext> context, params String[] roles)
            where TContext : DbContext
            where TRole : IdentityRole<TKey>, new()
            where TKey : IEquatable<TKey>
        {
            var roleManager = context.GetService<RoleManager<TRole>>();
            
            var existing = await context.Set<TRole>().ToListAsync();
            foreach (var roleName in roles.Where(x => existing.All(y => y.Name != x)))
            {
                var role = new TRole { Name = roleName };
                await roleManager.CreateAsync(role).ThrowOnErrorsAsync();
            }
        }

        /// <summary>
        /// Asynchronously seeds the role to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="roleName">The role name.</param>
        /// <returns>A task that represents the operation.</returns>
        public static Task<TRole> SeedRoleAsync<TContext, TRole, TKey>(this DbSeedContext<TContext> context, String roleName)
            where TContext : DbContext
            where TRole : IdentityRole<TKey>, new()
            where TKey : IEquatable<TKey>
        {
            return context.SeedRoleAsync<TContext, TRole, TKey>(roleName, role => { });
        }

        /// <summary>
        /// Asynchronously seeds the role to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="roleName">The role name.</param>
        /// <param name="initializer">The additional role initializer.</param>
        /// <returns>A task that represents the operation.</returns>
        public static async Task<TRole> SeedRoleAsync<TContext, TRole, TKey>(this DbSeedContext<TContext> context, String roleName, Action<TRole> initializer)
            where TContext : DbContext
            where TRole : IdentityRole<TKey>, new()
            where TKey : IEquatable<TKey>
        {
            var roleManager = context.GetService<RoleManager<TRole>>();
            var existing = await context.Set<TRole>().SingleOrDefaultAsync(x => x.Name == roleName);
            if (existing != null)
            {
                return existing;
            }

            var role = new TRole { Name = roleName };
            initializer(role);
            await roleManager.CreateAsync(role).ThrowOnErrorsAsync();
            return role;
        }

        #endregion

        #region Users

        /// <summary>
        /// Asynchronously seeds the user to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TUser">The type of the user.</typeparam>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The user email.</param>
        /// <param name="password">The user password.</param>
        /// <param name="roles">The user roles.</param>
        /// <returns>A task that represents the operation.</returns>
        public static Task<TUser> SeedUserAsync<TContext, TUser, TRole, TKey>(this DbSeedContext<TContext> context, String userName, String email, String password, params String[] roles)
            where TContext : DbContext
            where TUser : IdentityUser<TKey>, new()
            where TRole : IdentityRole<TKey>, new()
            where TKey : IEquatable<TKey>
        {
            return context.SeedUserAsync<TContext, TUser, TRole, TKey>(userName, email, password, user => { }, roles);
        }

        /// <summary>
        /// Asynchronously seeds the user to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TUser">The type of the user.</typeparam>
        /// <typeparam name="TRole">The type of the role.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="email">The user email.</param>
        /// <param name="password">The user password.</param>
        /// <param name="initializer">The additional user initializer.</param>
        /// <param name="roles">The user roles.</param>
        /// <returns>A task that represents the operation.</returns>
        public static async Task<TUser> SeedUserAsync<TContext, TUser, TRole, TKey>(this DbSeedContext<TContext> context, String userName, String email, String password, Action<TUser> initializer, params String[] roles)
            where TContext : DbContext
            where TUser : IdentityUser<TKey>, new()
            where TRole : IdentityRole<TKey>, new()
            where TKey : IEquatable<TKey>
        {
            var userManager = context.GetService<UserManager<TUser>>();
            var existing = await context.Set<TUser>().SingleOrDefaultAsync(x => x.UserName == userName);
            if (existing != null)
            {
                return existing;
            }

            var created = new TUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(created, password).ThrowOnErrorsAsync();
            foreach (var role in roles)
            {
                await userManager.AddToRoleAsync(created, role).ThrowOnErrorsAsync();
            }

            return created;
        }

        #endregion
    }
}
