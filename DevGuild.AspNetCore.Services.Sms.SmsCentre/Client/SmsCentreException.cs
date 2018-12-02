using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre.Client
{
    /// <summary>
    /// Represents sms centre exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SmsCentreException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsCentreException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="error">The error text.</param>
        public SmsCentreException(Int32 errorCode, String error)
            : base($"Error {errorCode}: {error}")
        {
        }
    }
}
