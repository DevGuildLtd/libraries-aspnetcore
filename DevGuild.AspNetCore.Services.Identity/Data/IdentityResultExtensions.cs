using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DevGuild.AspNetCore.Services.Identity.Data
{
    /// <summary>
    /// Contains IdentityResult extensions.
    /// </summary>
    /// <seealso cref="IdentityResult" />
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Checks the specified IdentityResult for errors and throws <see cref="InvalidOperationException"/> if there are any.
        /// </summary>
        /// <param name="result">The IdentityResult.</param>
        /// <exception cref="System.InvalidOperationException">Specified IdentityResult has any errors.</exception>
        public static void ThrowOnErrors(this IdentityResult result)
        {
            if (result.Succeeded)
            {
                return;
            }

            throw new InvalidOperationException(String.Join("\n", result.Errors.Select(x => x.ToErrorMessage())));
        }

        /// <summary>
        /// Asynchronously checks the specified IdentityResult for errors and throws <see cref="InvalidOperationException"/> if there are any.
        /// </summary>
        /// <param name="result">The IdentityResult.</param>
        /// <returns>A task that represents the operation.</returns>
        /// <exception cref="System.InvalidOperationException">Specified IdentityResult has any errors.</exception>
        public static async Task ThrowOnErrorsAsync(this Task<IdentityResult> result)
        {
            var awaited = await result;
            awaited.ThrowOnErrors();
        }

        private static String ToErrorMessage(this IdentityError error)
        {
            if (!String.IsNullOrEmpty(error.Code) && !String.IsNullOrEmpty(error.Description))
            {
                return $"{error.Code}: {error.Description}";
            }
            else if (!String.IsNullOrEmpty(error.Code))
            {
                return error.Code;
            }
            else
            {
                return error.Description;
            }
        }
    }
}
