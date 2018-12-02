using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.Models
{
    /// <summary>
    /// Defines permissions that could be applied in a single security scope.
    /// </summary>
    public abstract class PermissionsNamespace
    {
        private String defaultName;
        private List<Permission> permissions;

        /// <summary>
        /// Gets the list of permissions defined in this namespace.
        /// </summary>
        /// <value>
        /// The list of permissions defined in this namespace.
        /// </value>
        public IList<Permission> Permissions => (this.permissions ?? (this.permissions = this.GetAllPermissions())).AsReadOnly();

        /// <summary>
        /// Gets the name of permissions namespace.
        /// </summary>
        /// <value>
        /// The name of permissions namespace.
        /// </value>
        public virtual String Name => this.defaultName ?? (this.defaultName = this.GetDefaultName());

        /// <summary>
        /// Determines whether the specified permission is defined in this namespace.
        /// </summary>
        /// <param name="permission">The tested permission.</param>
        /// <returns>
        ///   <c>true</c> if the specified permission is defined; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsDefined(Permission permission)
        {
            return this.Permissions.Contains(permission);
        }

        private List<Permission> GetAllPermissions()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CanRead && x.PropertyType == typeof(Permission))
                .ToList();

            return properties.Select(x => (Permission)x.GetValue(this)).ToList();
        }

        private String GetDefaultName()
        {
            const String suffix = "PermissionsNamespace";
            var type = this.GetType();
            if (type.Name != suffix && type.Name.EndsWith(suffix))
            {
                return type.Name.Substring(0, type.Name.Length - suffix.Length);
            }
            else
            {
                return type.Name;
            }
        }
    }
}
