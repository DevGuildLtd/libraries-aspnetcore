using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Sms
{
    /// <summary>
    /// Represents sms provider that does nothing.
    /// </summary>
    /// <seealso cref="ISmsProvider" />
    public sealed class NoneSmsProvider : ISmsProvider
    {
        /// <inheritdoc />
        public Task SendAsync(String sender, String phoneNumber, String messageId, String messageText)
        {
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}
