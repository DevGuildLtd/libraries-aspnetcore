using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Configuration;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Services.Mail.Smtp
{
    public static class SmtpMailServiceBuilderExtensions
    {
        public static MailServiceBuilder AddSmtp(this MailServiceBuilder builder)
        {
            return builder.AddProvider("Smtp", (name, configuration) =>
            {
                var senderConfiguration = new MailSenderConfiguration(
                    sender: configuration.GetValue<String>("Sender"),
                    blindCopy: configuration.GetValue<String>("BlindCopy"),
                    debugMode: configuration.GetValue<Boolean>("DebugMode"));

                var smtpConfiguration = new SmtpEmailProviderConfiguration
                {
                    Host = configuration.GetValue<String>("Options:Host"),
                    Port = configuration.GetValue<Int32>("Options:Port"),
                    EnableSsl = configuration.GetValue<Boolean>("Options:UseSsl"),
                    Username = configuration.GetValue<String>("Options:UserName"),
                    Password = SmtpMailServiceBuilderExtensions.CreateSecureString(configuration.GetValue<String>("Options:Password"))
                };

                return new MailConfiguration(
                    configurationName: name,
                    senderConfiguration: senderConfiguration,
                    providerConstructor: () => new SmtpEmailProvider(smtpConfiguration));
            });
        }

        private static SecureString CreateSecureString(String value)
        {
            var secureString = new SecureString();
            foreach (var c in value)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();
            return secureString;
        }
    }
}
