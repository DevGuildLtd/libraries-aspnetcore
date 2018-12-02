using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Sms.Annotations;
using DevGuild.AspNetCore.Services.Sms.Models;

namespace DevGuild.AspNetCore.Services.Sms
{
    internal static class SmsExtensions
    {
        public static String GetConfigurationName(this ISms sms)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (sms is IExplicitSmsConfiguration explicitSmsConfiguration)
            {
                return explicitSmsConfiguration.GetConfigurationName();
            }

            return SmsConfigurationAttribute.GetConfigurationFromSms(sms);
        }
    }
}
