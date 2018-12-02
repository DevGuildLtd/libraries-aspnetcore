using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Configuration;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Services.Mail.AmazonSes
{
    public static class AmazonSesMailServiceBuilderExtensions
    {
        public static MailServiceBuilder AddAmazonSes(this MailServiceBuilder builder)
        {
            return builder.AddProvider("AmazonSes", (name, configuration) =>
            {
                var senderConfiguration = new MailSenderConfiguration(
                    sender: configuration.GetValue<String>("Sender"),
                    blindCopy: configuration.GetValue<String>("BlindCopy"),
                    debugMode: configuration.GetValue<Boolean>("DebugMode"));

                var amazonSesConfiguration = new AmazonSesEmailProviderConfiguration
                {
                    Region = configuration.GetValue<String>("Options:Region"),
                    AccessKey = configuration.GetValue<String>("Options:AccessKey"),
                    SecretKey = configuration.GetValue<String>("Options:SecretKey")
                };

                return new MailConfiguration(
                    configurationName: name,
                    senderConfiguration: senderConfiguration,
                    providerConstructor: () => new AmazonSesEmailProvider(amazonSesConfiguration));
            });
        }
    }
}
