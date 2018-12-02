using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Sms
{
    /// <summary>
    /// Defines interface for the sms provider.
    /// </summary>
    public interface ISmsProvider : IDisposable
    {
        /// <summary>
        /// Asynchronously sends the sms message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="messageText">The message text.</param>
        /// <returns>A task that represents the operation.</returns>
        Task SendAsync(String sender, String phoneNumber, String messageId, String messageText);
    }
}
