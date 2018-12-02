using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre.Client
{
    /// <summary>
    /// Represents sms centre send response.
    /// </summary>
    public class SmsCentreSendResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmsCentreSendResponse"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="count">The count.</param>
        public SmsCentreSendResponse(String id, Int32 count)
        {
            this.Id = id;
            this.Count = count;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public String Id { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public Int32 Count { get; }
    }
}
