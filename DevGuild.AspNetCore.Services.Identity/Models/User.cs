using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity.Models
{
    /// <summary>
    /// Represents a User entity.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser{System.Guid}" />
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.Id = Guid.NewGuid();
        }
    }
}
