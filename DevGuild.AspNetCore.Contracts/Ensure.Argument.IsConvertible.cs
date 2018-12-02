using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace DevGuild.AspNetCore.Contracts
{
    public static partial class Ensure
    {
        public static partial class Argument
        {
            /// <summary>
            /// Tests related to argument value convertability to different types.
            /// </summary>
            public static partial class IsConvertible
            {
                /// <summary>
                /// Check, whether the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is convertible to <see cref="Guid"/>; otherwise throws <see cref="ArgumentException"/>.
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="argumentName">Name of the argument.</param>
                /// <returns>The value converted to <see cref="Guid"/>.</returns>
                /// <exception cref="ArgumentException">The value is not convertible to <see cref="Guid"/>.</exception>
                [PublicAPI]
                [DebuggerStepThrough]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Guid ToGuid(String value, [InvokerParameterName]String argumentName)
                {
                    if (!Guid.TryParse(value, out var result))
                    {
                        throw new ArgumentException($"{argumentName} is not a valid Guid value", argumentName);
                    }

                    return result;
                }

                /// <summary>
                /// Check, whether the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is convertible to <see cref="Guid"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="argumentName">Name of the argument.</param>
                /// <param name="message">The exception message.</param>
                /// <returns>The value converted to <see cref="Guid"/>.</returns>
                /// <exception cref="ArgumentException">The value is not convertible to <see cref="Guid"/>.</exception>
                [PublicAPI]
                [DebuggerStepThrough]
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static Guid ToGuid(String value, [InvokerParameterName]String argumentName, String message)
                {
                    if (!Guid.TryParse(value, out var result))
                    {
                        throw new ArgumentException(message, argumentName);
                    }

                    return result;
                }
            }
        }
    }
}
