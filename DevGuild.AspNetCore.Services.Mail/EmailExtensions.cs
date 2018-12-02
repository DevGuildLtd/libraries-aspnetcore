using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Mail.Annotations;
using DevGuild.AspNetCore.Services.Mail.Models;

namespace DevGuild.AspNetCore.Services.Mail
{
    public static class EmailExtensions
    {
        internal static String GetConfigurationName(this IEmail message)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (message is IExplicitEmailConfiguration explicitEmail)
            {
                return explicitEmail.GetConfigurationName();
            }

            return EmailConfigurationAttribute.GetConfigurationFromEmail(message);
        }
    }
}
