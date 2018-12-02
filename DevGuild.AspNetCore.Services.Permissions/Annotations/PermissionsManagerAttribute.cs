using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.Annotations
{
    /// <summary>
    /// Defines permissions manager for the class to which this attribute is applied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class PermissionsManagerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsManagerAttribute"/> class.
        /// </summary>
        /// <param name="type">The type of the permissions manager.</param>
        /// <param name="path">The path to the permissions manager.</param>
        public PermissionsManagerAttribute(String type, String path)
        {
            this.Type = type;
            this.Path = path;
        }

        /// <summary>
        /// Gets the type of the permissions manager.
        /// </summary>
        /// <value>
        /// The type of the permissions manager.
        /// </value>
        public String Type { get; }

        /// <summary>
        /// Gets the path of the permissions manager.
        /// </summary>
        /// <value>
        /// The path of the permissions manager.
        /// </value>
        public String Path { get; }

        /// <summary>
        /// Gets the permissions manager from attribute annotations.
        /// </summary>
        /// <param name="classType">Type of the annotated class.</param>
        /// <param name="managerType">Type of the permissions manager.</param>
        /// <returns>Path to the permissions manager if it is defined; <c>null</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">Multiple ambiguous attributes are defined.</exception>
        public static String GetAnnotatedPermissionsManager(Type classType, String managerType)
        {
            var attributes = classType.GetCustomAttributes<PermissionsManagerAttribute>()
                .Where(x => x.Type == managerType)
                .ToList();

            if (attributes.Count == 0)
            {
                return null;
            }

            if (attributes.Count == 1)
            {
                return attributes[0].Path;
            }

            throw new InvalidOperationException($"Multiple ambiguous PermissionsManagerAttribute defined on class {classType}");
        }
    }
}
