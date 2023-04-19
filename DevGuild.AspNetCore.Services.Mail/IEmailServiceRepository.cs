using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Mail.Models;

namespace DevGuild.AspNetCore.Services.Mail
{
    /// <summary>Repository that is used to store sent messages.</summary>
    public interface IEmailServiceRepository
    {
        /// <summary>Stores the email message that is prepared to being sent.</summary>
        /// <param name="message">The message data.</param>
        /// <returns>A task that represents the operation.</returns>
        Task StorePreparedMessageAsync(EmailMessage message);

        /// <summary>Stores the result of message sending.</summary>
        /// <param name="message">The message data.</param>
        /// <param name="result">The sending result.</param>
        /// <returns>A task that represents the operation.</returns>
        Task StoreSendingResultAsync(EmailMessage message, EmailSendingResult result);
    }
}
