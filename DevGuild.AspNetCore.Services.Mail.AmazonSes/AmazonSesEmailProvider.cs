using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;

namespace DevGuild.AspNetCore.Services.Mail.AmazonSes
{
    /// <summary>
    /// Represents implementation of the email provider using AmazonSes.
    /// </summary>
    /// <seealso cref="DevGuild.AspNetCore.Services.Mail.IEmailProvider" />
    public class AmazonSesEmailProvider : IEmailProvider
    {
        private readonly AmazonSesEmailProviderConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AmazonSesEmailProvider"/> class.
        /// </summary>
        /// <param name="configuration">The provider configuration.</param>
        public AmazonSesEmailProvider(AmazonSesEmailProviderConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<EmailSendingResult> SendAsync(MimeMessage message)
        {
            try
            {
                using (var client = this.CreateClient())
                using (var stream = new MemoryStream())
                {
                    await message.WriteToAsync(stream);
                    stream.Position = 0;

                    var request = new SendRawEmailRequest
                    {
                        RawMessage = new RawMessage(stream),
                    };

                    var response = await client.SendRawEmailAsync(request);
                    return EmailSendingResult.Succeed(response.MessageId);
                }
            }
            catch (Exception exception)
            {
                return EmailSendingResult.Fail(exception.ToString());
            }
        }

        private IAmazonSimpleEmailService CreateClient()
        {
            var credentials = new BasicAWSCredentials(this.configuration.AccessKey, this.configuration.SecretKey);
            var region = RegionEndpoint.GetBySystemName(this.configuration.Region);
            return new AmazonSimpleEmailServiceClient(credentials, region);
        }
    }
}
