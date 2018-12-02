using System;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Mail.Configuration;
using DevGuild.AspNetCore.Services.Mail.Models;
using MimeKit;

namespace DevGuild.AspNetCore.Services.Mail
{
    public class EmailService : IEmailService
    {
        private readonly MailConfigurationCollection configurationCollection;
        private readonly IRepositoryFactory repositoryFactory;

        public EmailService(MailConfigurationCollection configurationCollection, IRepositoryFactory repositoryFactory)
        {
            this.configurationCollection = configurationCollection;
            this.repositoryFactory = repositoryFactory;
        }

        public async Task<EmailMessage> SendAsync(IEmail email)
        {
            var configurationName = email.GetConfigurationName() ?? this.configurationCollection.DefaultConfiguration;
            var configuration = this.configurationCollection.GetConfiguration(configurationName);
            if (configuration == null)
            {
                throw new InvalidOperationException("Configuration not found");
            }

            using (var repository = this.repositoryFactory.CreateRepository())
            {
                var message = email.CreateMessage();

                this.ApplySenderFrom(message, configuration.SenderConfiguration);
                
                var store = repository.GetEntityStore<EmailMessage>();
                var storeEntry = new EmailMessage
                {
                    Created = DateTime.UtcNow,
                    MessageType = email.GetType().Name,
                    Configuration = configuration.ConfigurationName,
                    From = message.From.ToString(encode: false),
                    To = message.To.ToString(encode: false),
                    Cc = message.Cc.ToString(encode: false),
                    Bcc = message.Bcc.ToString(encode: false),
                    Subject = message.Subject,
                    Content = message.ToString(),
                    Status = EmailMessageStatus.Created
                };

                await store.InsertAsync(storeEntry);
                await repository.SaveChangesAsync();

                this.ApplySenderDebugOptions(message, configuration.SenderConfiguration);

                var provider = configuration.ProviderConstructor();
                var result = await provider.SendAsync(message);
                if (result.Success)
                {
                    storeEntry.Status = EmailMessageStatus.Sent;
                    storeEntry.MessageId = result.MessageId;
                    await store.UpdateAsync(storeEntry);
                    await repository.SaveChangesAsync();
                }
                else
                {
                    storeEntry.Status = EmailMessageStatus.Failed;
                    storeEntry.SendingError = result.Error;
                    await store.UpdateAsync(storeEntry);
                    await repository.SaveChangesAsync();
                }

                return storeEntry;
            }
        }

        private void ApplySenderFrom(MimeMessage message, MailSenderConfiguration senderConfiguration)
        {
            message.From.Clear();
            message.From.Add(MailboxAddress.Parse(ParserOptions.Default, senderConfiguration.Sender));
        }

        private void ApplySenderDebugOptions(MimeMessage message, MailSenderConfiguration senderConfiguration)
        {
            if (senderConfiguration.DebugMode)
            {
                if (String.IsNullOrEmpty(senderConfiguration.BlindCopy))
                {
                    throw new InvalidOperationException("Debug email mode requires blind copy recipient to be configured");
                }

                var recipientsTo = new InternetAddressList(message.To);
                var recipientsCc = new InternetAddressList(message.Cc);
                var recipientsBcc = new InternetAddressList(message.Bcc);

                message.To.Clear();
                message.Cc.Clear();
                message.Bcc.Clear();

                var newTo = InternetAddressList.Parse(ParserOptions.Default, senderConfiguration.BlindCopy);
                foreach (var address in newTo)
                {
                    message.To.Add(address);
                }

                if (recipientsTo.Count > 0)
                {
                    message.Headers.Add("X-Debug-Intended-To", recipientsTo.ToString(encode: true));
                }

                if (recipientsCc.Count > 0)
                {
                    message.Headers.Add("X-Debug-Intended-Cc", recipientsCc.ToString(encode: true));
                }

                if (recipientsBcc.Count > 0)
                {
                    message.Headers.Add("X-Debug-Intended-Bcc", recipientsBcc.ToString(encode: true));
                }
            }
            else
            {
                var additionalBcc = InternetAddressList.Parse(ParserOptions.Default, senderConfiguration.BlindCopy);
                foreach (var address in additionalBcc)
                {
                    message.Bcc.Add(address);
                }
            }
        }
    }
}
