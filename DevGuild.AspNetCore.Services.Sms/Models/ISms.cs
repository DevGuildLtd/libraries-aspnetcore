using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.Models
{
    /// <summary>
    /// Defines interface of a sms message.
    /// </summary>
    public interface ISms
    {
        /// <summary>
        /// Gets the recipient's phone number.
        /// </summary>
        /// <returns>Recipient's phone number.</returns>
        String GetPhoneNumber();

        /// <summary>
        /// Gets the message text.
        /// </summary>
        /// <returns>Message text.</returns>
        String GetMessageText();
    }
}
