using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity.Models
{
    /// <summary>
    /// Represents a Role entity.
    /// </summary>
    /// <seealso cref="IdentityRole{Guid}" />
    public class Role : IdentityRole<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.Id = Guid.NewGuid();
        }
    }
}
