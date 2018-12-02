using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Mail
{
    public static class MailServiceCollectionExtensions
    {
        public static MailServiceBuilder AddMail(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationCollection = new MailConfigurationCollection();
            services.AddSingleton<MailConfigurationCollection>(configurationCollection);
            services.AddScoped<IEmailService, EmailService>();

            return new MailServiceBuilder(services, configuration, configurationCollection)
                .AddNoneProvider();
        }

        internal static MailServiceBuilder AddNoneProvider(this MailServiceBuilder builder)
        {
            return builder.AddProvider("None", (name, configuration) => new MailConfiguration(
                configurationName: name,
                senderConfiguration: new MailSenderConfiguration(
                    sender: configuration.GetValue<String>("Sender"),
                    blindCopy: configuration.GetValue<String>("BlindCopy"),
                    debugMode: configuration.GetValue<Boolean>("DebugMode")),
                providerConstructor: () => new NoneEmailProvider()));
        }
    }
}
