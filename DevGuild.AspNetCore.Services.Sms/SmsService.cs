using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Sms.Annotations;
using DevGuild.AspNetCore.Services.Sms.Configuration;
using DevGuild.AspNetCore.Services.Sms.Models;

namespace DevGuild.AspNetCore.Services.Sms
{
    public class SmsService : ISmsService
    {
        private readonly SmsConfigurationCollection configuration;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsService"/> class.
        /// </summary>
        /// <param name="configuration">The sms configuration.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public SmsService(SmsConfigurationCollection configuration, IRepositoryFactory repositoryFactory, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.repositoryFactory = repositoryFactory;
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public async Task SendAsync(ISms message)
        {
            if (message == null)
            {
                throw new ArgumentNullException($"{nameof(message)} is null", nameof(message));
            }
            
            var configurationName = message.GetConfigurationName() ?? this.configuration.DefaultConfiguration;
            var actualConfiguration = this.configuration.GetConfiguration(configurationName);

            if (actualConfiguration == null)
            {
                throw new InvalidOperationException("Configuration not found");
            }

            using (var repository = this.repositoryFactory.CreateRepository())
            using (var provider = actualConfiguration.ProviderConstructor(this.serviceProvider))
            {
                var store = repository.GetEntityStore<SmsMessage>();

                var now = DateTime.UtcNow;
                var phone = message.GetPhoneNumber();
                var messageId = Guid.NewGuid().ToString("D");
                var messageText = message.GetMessageText();
                var storeEntry = new SmsMessage
                {
                    Created = now,
                    MessageType = message.GetType().Name,
                    Configuration = actualConfiguration.ConfigurationName,
                    From = actualConfiguration.SenderName,
                    To = phone,
                    MessageText = messageText,
                    MessageId = messageId,
                    LastAttempt = now,
                    TotalAttempts = 1
                };
                await store.InsertAsync(storeEntry);
                await repository.SaveChangesAsync();

                try
                {
                    await provider.SendAsync(
                        sender: actualConfiguration.SenderName,
                        phoneNumber: phone,
                        messageId: messageId,
                        messageText: messageText);

                    storeEntry.Status = SmsMessageStatus.Sent;
                    await store.UpdateAsync(storeEntry);
                    await repository.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    storeEntry.LastError = e.ToString();
                    await store.UpdateAsync(storeEntry);
                    await repository.SaveChangesAsync();
                }
            }
        }
    }
}
