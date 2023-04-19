using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Mail.Models;

namespace DevGuild.AspNetCore.Services.Mail
{
    /// <summary>An implementation of <see cref="IEmailServiceRepository"/> that stores sent messages in the database.</summary>
    /// <seealso cref="IEmailServiceRepository" />
    public class DefaultEmailServiceRepository : IEmailServiceRepository
    {
        private readonly IRepositoryFactory repositoryFactory;

        public DefaultEmailServiceRepository(IRepositoryFactory repositoryFactory)
        {
            this.repositoryFactory = repositoryFactory;
        }

        /// <inheritdoc />
        public async Task StorePreparedMessageAsync(EmailMessage message)
        {
            using var repository = this.repositoryFactory.CreateRepository();
            await repository.InsertAsync(message);
            await repository.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task StoreSendingResultAsync(EmailMessage message, EmailSendingResult result)
        {
            using var repository = this.repositoryFactory.CreateRepository();
            if (result.Success)
            {
                message.Status = EmailMessageStatus.Sent;
                message.MessageId = result.MessageId;
            }
            else
            {
                message.Status = EmailMessageStatus.Failed;
                message.SendingError = result.Error;
            }

            await repository.UpdateAsync(message);
            await repository.SaveChangesAsync();
        }
    }
}
