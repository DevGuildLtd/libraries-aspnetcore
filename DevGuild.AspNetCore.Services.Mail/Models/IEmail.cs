using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;

namespace DevGuild.AspNetCore.Services.Mail.Models
{
    public interface IEmail
    {
        MimeMessage CreateMessage();
    }
}
