﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DevGuild.AspNetCore.Services.Storage
{
    /// <summary>
    /// Represents exception that is throw when there is attempt to store the file when there is already a file with that name.
    /// </summary>
    [Serializable]
    public class FileAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAlreadyExistsException"/> class.
        /// </summary>
        public FileAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FileAlreadyExistsException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public FileAlreadyExistsException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected FileAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
