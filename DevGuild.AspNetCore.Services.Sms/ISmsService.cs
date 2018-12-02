using System;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Sms.Models;

namespace DevGuild.AspNetCore.Services.Sms
{
    /// <summary>
    /// Defines interface of the sms service.
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Asynchronously sends the sms message.
        /// </summary>
        /// <param name="message">The sms message.</param>
        /// <returns>A task that represents the operation.</returns>
        Task SendAsync(ISms message);
    }
}
