using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DevGuild.AspNetCore.Services.Mail
{
    public static class MailServiceCollectionExtensions
    {
        public static MailServiceBuilder AddMail(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationCollection = new MailConfigurationCollection();
            services.AddSingleton<MailConfigurationCollection>(configurationCollection);
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailServiceRepository, DefaultEmailServiceRepository>();

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

        public static MailServiceBuilder AddNoneRepository(this MailServiceBuilder builder)
        {
            builder.Services.RemoveAll<IEmailServiceRepository>();
            builder.Services.AddScoped<IEmailServiceRepository, NoneEmailServiceRepository>();
            return builder;
        }

        public static MailServiceBuilder AddDefaultRepository(this MailServiceBuilder builder)
        {
            builder.Services.RemoveAll<IEmailServiceRepository>();
            builder.Services.AddScoped<IEmailServiceRepository, DefaultEmailServiceRepository>();
            return builder;
        }

        public static MailServiceBuilder AddCustomRepository<TRepository>(this MailServiceBuilder builder)
            where TRepository : class, IEmailServiceRepository
        {
            builder.Services.RemoveAll<IEmailServiceRepository>();
            builder.Services.AddScoped<IEmailServiceRepository, TRepository>();
            return builder;
        }
    }
}
