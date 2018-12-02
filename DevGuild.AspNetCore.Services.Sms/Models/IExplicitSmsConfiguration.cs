using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.Models
{
    public interface IExplicitSmsConfiguration
    {
        String GetConfigurationName();
    }
}
