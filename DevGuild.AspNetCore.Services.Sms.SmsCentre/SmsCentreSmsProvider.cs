using System;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Sms.SmsCentre.Client;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre
{
    /// <summary>
    /// Represents sms provider that is using SMS Centre for sending sms messages.
    /// </summary>
    /// <seealso cref="ISmsProvider" />
    public sealed class SmsCentreSmsProvider : ISmsProvider
    {
        private readonly SmsCentreSmsProviderConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsCentreSmsProvider"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public SmsCentreSmsProvider(SmsCentreSmsProviderConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public async Task SendAsync(String sender, String phoneNumber, String messageId, String messageText)
        {
            var attempt = 0;
            while (attempt++ < 3)
            {
                try
                {
                    using (var client = this.CreateClient(attempt))
                    {
                        await client.SendAsync(new SmsCentreSendRequest
                        {
                            Sender = sender,
                            Phones = new[] { phoneNumber },
                            Identifier = messageId,
                            Message = messageText
                        });

                        return;
                    }
                }
                catch (SmsCentreException)
                {
                    throw;
                }
                catch (Exception)
                {
                    if (attempt == 2)
                    {
                        throw;
                    }
                }

                await Task.Delay(2000 + 1000 * attempt);
            }

            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        private SmsCentreClient CreateClient(Int32 attempt)
        {
            var serviceUrl = attempt <= 2 || String.IsNullOrEmpty(this.configuration.SecondaryUrl)
                ? this.configuration.ServiceUrl
                : this.configuration.SecondaryUrl;

            return new SmsCentreClient(
                host: serviceUrl,
                username: this.configuration.Username,
                password: this.configuration.Password);
        }
    }
}
