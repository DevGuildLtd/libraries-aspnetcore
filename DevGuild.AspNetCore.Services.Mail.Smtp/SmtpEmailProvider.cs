using System;
using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace DevGuild.AspNetCore.Services.Mail.Smtp
{
    public class SmtpEmailProvider : IEmailProvider
    {
        private readonly SmtpEmailProviderConfiguration configuration;

        public SmtpEmailProvider(SmtpEmailProviderConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<EmailSendingResult> SendAsync(MimeMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(
                        host: this.configuration.Host,
                        port: this.configuration.Port,
                        useSsl: this.configuration.EnableSsl);

                    if (!String.IsNullOrEmpty(this.configuration.Username))
                    {
                        await client.AuthenticateAsync(new NetworkCredential(this.configuration.Username, this.configuration.Password));
                    }

                    await client.SendAsync(message);
                    await client.DisconnectAsync(quit: true);

                    return EmailSendingResult.Succeed(String.Empty);
                }
            }
            catch (Exception exception)
            {
                return EmailSendingResult.Fail(exception.ToString());
            }
        }
    }
}
