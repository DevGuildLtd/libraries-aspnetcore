using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Mail
{
    /// <summary>
    /// Represents email provider constructor.
    /// </summary>
    /// <returns>Created email provider.</returns>
    public delegate IEmailProvider EmailProviderConstructor();
}
