using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Sms.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Sms
{
    public static class SmsServiceCollectionExtensions
    {
        public static SmsServiceBuilder AddSms(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationCollection = new SmsConfigurationCollection();
            services.AddSingleton<SmsConfigurationCollection>(configurationCollection);
            services.AddScoped<ISmsService, SmsService>();

            return new SmsServiceBuilder(services, configuration, configurationCollection)
                .AddNoneProvider();
        }

        internal static SmsServiceBuilder AddNoneProvider(this SmsServiceBuilder builder)
        {
            return builder.AddProvider("None", (name, configuration) => new SmsConfiguration(
                configurationName: name,
                senderName: configuration.GetValue<String>("SenderName"),
                providerConstructor: provider => new NoneSmsProvider()));
        }
    }
}
