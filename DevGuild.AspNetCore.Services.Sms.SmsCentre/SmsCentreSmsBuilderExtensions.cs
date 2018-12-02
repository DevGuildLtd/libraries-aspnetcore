using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Sms.Configuration;
using Microsoft.Extensions.Configuration;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre
{
    public static class SmsCentreSmsBuilderExtensions
    {
        public static SmsServiceBuilder AddSmsCentre(this SmsServiceBuilder builder)
        {
            return builder.AddProvider("SmsCentre", (name, configuration) => new SmsConfiguration(
                configurationName: name,
                senderName: configuration.GetValue<String>("SenderName"),
                providerConstructor: provider => new SmsCentreSmsProvider(new SmsCentreSmsProviderConfiguration
                {
                    ServiceUrl = configuration.GetValue<String>("Options:ServiceUrl"),
                    SecondaryUrl = configuration.GetValue<String>("Options:SecondaryUrl"),
                    Username = configuration.GetValue<String>("Options:Username"),
                    Password = configuration.GetValue<String>("Options:Password")
                })));
        }
    }
}
