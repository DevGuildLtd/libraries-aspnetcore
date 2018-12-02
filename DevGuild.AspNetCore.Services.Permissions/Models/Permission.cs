using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.Models
{
    /// <summary>
    /// Represents a single permission that could be required to perform some action on some object.
    /// </summary>
    public sealed class Permission : IEquatable<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="id">The permission unique identifier.</param>
        /// <param name="name">The permission name.</param>
        /// <param name="bits">The permission bit mask.</param>
        public Permission(Guid id, String name, Int32 bits)
        {
            if (name == null)
            {
                throw new ArgumentNullException($"{nameof(name)} is null", nameof(name));
            }

            if (bits <= 0)
            {
                throw new ArgumentException($"{nameof(bits)} must be greater than 0", nameof(bits));
            }

            this.Id = id;
            this.Name = name;
            this.Bits = bits;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="id">The permission unique identifier.</param>
        /// <param name="name">The permission name.</param>
        /// <param name="bits">The permission bit mask.</param>
        public Permission(String id, String name, Int32 bits)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"{nameof(id)} is null", nameof(id));
            }

            if (!Guid.TryParse(id, out var idAsGuid))
            {
                throw new ArgumentException($"{nameof(id)} must be a valid Guid", nameof(id));
            }

            if (name == null)
            {
                throw new ArgumentNullException($"{nameof(name)} is null", nameof(name));
            }

            if (bits <= 0)
            {
                throw new ArgumentException($"{nameof(bits)} must be greater than 0", nameof(bits));
            }

            this.Id = idAsGuid;
            this.Name = name;
            this.Bits = bits;
        }

        /// <summary>
        /// Gets the unique identifier of the permission.
        /// </summary>
        /// <value>
        /// The unique identifier of the permission.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the name of the permission.
        /// </summary>
        /// <value>
        /// The name of the permission.
        /// </value>
        public String Name { get; }

        /// <summary>
        /// Gets the bit mask of the permission.
        /// </summary>
        /// <value>
        /// The bit mask of the permission.
        /// </value>
        public Int32 Bits { get; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(Permission other)
        {
            if (Object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Id.Equals(other.Id)
                   && String.Equals(this.Name, other.Name)
                   && this.Bits == other.Bits;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            if (Object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Permission permission
                   && this.Equals(permission);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Bits;
                return hashCode;
            }
        }
    }
}
