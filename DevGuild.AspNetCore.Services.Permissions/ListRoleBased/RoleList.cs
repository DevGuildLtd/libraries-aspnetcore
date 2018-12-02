using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.ListRoleBased
{
    /// <summary>
    /// Represents roles permissions list.
    /// </summary>
    public class RoleList
    {
        private RoleList(Boolean allowAnonymous, IEnumerable<String> roles)
        {
            this.AllowAnonymous = allowAnonymous;
            this.Roles = roles.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a value indicating whether anonymous access is allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if anonymous access is allowed; otherwise, <c>false</c>.
        /// </value>
        public Boolean AllowAnonymous { get; }

        /// <summary>
        /// Gets the required roles.
        /// </summary>
        /// <value>
        /// The required roles.
        /// </value>
        public IList<String> Roles { get; }

        /// <summary>
        /// Create role list that allows anonymous access.
        /// </summary>
        /// <returns>Created role list.</returns>
        public static RoleList Anonymous()
        {
            return new RoleList(true, new String[0]);
        }

        /// <summary>
        /// Create role list that only required for user to be authenticated.
        /// </summary>
        /// <returns>Created role list.</returns>
        public static RoleList Authenticated()
        {
            return new RoleList(false, new String[0]);
        }

        /// <summary>
        /// Create role list that requires specified roles.
        /// </summary>
        /// <param name="roles">The required roles.</param>
        /// <returns>Created role list.</returns>
        public static RoleList WithRoles(params String[] roles)
        {
            return new RoleList(false, roles);
        }
    }
}
