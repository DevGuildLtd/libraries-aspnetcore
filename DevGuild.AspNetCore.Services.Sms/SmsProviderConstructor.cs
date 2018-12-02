using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms
{
    /// <summary>
    /// Represents sms provider constructor.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    /// <returns>Created sms provider.</returns>
    public delegate ISmsProvider SmsProviderConstructor(IServiceProvider provider);
}
