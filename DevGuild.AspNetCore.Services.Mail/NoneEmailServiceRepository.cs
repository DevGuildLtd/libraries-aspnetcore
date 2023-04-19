using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Mail.Models;

namespace DevGuild.AspNetCore.Services.Mail
{
    /// <summary>An implementation of <see cref="IEmailServiceRepository"/> that does not store sent messages.</summary>
    /// <seealso cref="IEmailServiceRepository" />
    public class NoneEmailServiceRepository : IEmailServiceRepository
    {
        /// <inheritdoc />
        public Task StorePreparedMessageAsync(EmailMessage message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task StoreSendingResultAsync(EmailMessage message, EmailSendingResult result)
        {
            return Task.CompletedTask;
        }
    }
}
