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
        /// <summary>
        /// Argument-related condition tests
        /// </summary>
        public static partial class Argument
        {
            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the param <paramref name="argumentName"/> of the calling method is not null; otherwise throws <see cref="ArgumentNullException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="argumentValue">The argument value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentNullException"><paramref name="argumentValue" /> is null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNull<T>(T argumentValue, [InvokerParameterName]String argumentName)
                where T : class
            {
                if (argumentValue == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the param <paramref name="argumentName"/> of the calling method is not null; otherwise throws <see cref="ArgumentNullException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="argumentValue">The argument value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentNullException"><paramref name="argumentValue" /> is null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNull<T>(T argumentValue, [InvokerParameterName]String argumentName, String message)
                where T : class
            {
                if (argumentValue == null)
                {
                    throw new ArgumentNullException(argumentName, message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the param <paramref name="argumentName"/> of the calling method is null; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="argumentValue">The argument value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException"><paramref name="argumentValue"/> is not null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Null<T>(T argumentValue, [InvokerParameterName]String argumentName)
                where T : class
            {
                if (argumentValue != null)
                {
                    throw new ArgumentException($"{argumentName} is not null", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the param <paramref name="argumentName"/> of the calling method is null; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="argumentValue">The argument value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException"><paramref name="argumentValue"/> is not null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Null<T>(T argumentValue, [InvokerParameterName]String argumentName, String message)
                where T : class
            {
                if (argumentValue != null)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the nullable param <paramref name="argumentName"/> of the calling method has a value; otherwise throws <see cref="ArgumentNullException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value</typeparam>
            /// <param name="argumentValue">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentNullException">The nullable value does not have a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasValue<T>(T? argumentValue, [InvokerParameterName]String argumentName)
                where T : struct
            {
                if (!argumentValue.HasValue)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the nullable param <paramref name="argumentName"/> of the calling method has a value; otherwise throws <see cref="ArgumentNullException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value</typeparam>
            /// <param name="argumentValue">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentNullException">The nullable value does not have a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasValue<T>(T? argumentValue, [InvokerParameterName]String argumentName, String message)
                where T : struct
            {
                if (!argumentValue.HasValue)
                {
                    throw new ArgumentNullException(argumentName, message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the nullable param <paramref name="argumentName"/> of the calling method does not have a value; otherwise throws <see cref="ArgumentNullException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value</typeparam>
            /// <param name="argumentValue">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentNullException">The nullable value has a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotHaveValue<T>(T? argumentValue, [InvokerParameterName]String argumentName)
                where T : struct
            {
                if (argumentValue.HasValue)
                {
                    throw new ArgumentException($"{argumentName} has a value", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="argumentValue" /> parameter of the nullable param <paramref name="argumentName"/> of the calling method does not have a value; otherwise throws <see cref="ArgumentNullException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value</typeparam>
            /// <param name="argumentValue">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentNullException">The nullable value has a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("argumentValue:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotHaveValue<T>(T? argumentValue, [InvokerParameterName]String argumentName, String message)
                where T : struct
            {
                if (argumentValue.HasValue)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> of the param <paramref name="argumentName"/> of the calling method is met; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <param name="condition">if set to <c>false</c> throws an exception.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">The condition is not met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:false => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void MeetCondition(Boolean condition, [InvokerParameterName] String argumentName)
            {
                if (!condition)
                {
                    throw new ArgumentException($"{argumentName} does not meet required condition", argumentName);
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> of the param <paramref name="argumentName"/> of the calling method is met; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <param name="condition">if set to <c>false</c> throws an exception.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">The condition is not met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:false => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void MeetCondition(Boolean condition, [InvokerParameterName] String argumentName, String message)
            {
                if (!condition)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> of the param <paramref name="argumentName"/> of the calling method is not met; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <param name="condition">if set to <c>true</c> throws an exception.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">The condition is met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:true => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotMeetCondition(Boolean condition, [InvokerParameterName] String argumentName)
            {
                if (condition)
                {
                    throw new ArgumentException($"{argumentName} does not meet required condition", argumentName);
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> of the param <paramref name="argumentName"/> of the calling method is not met; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <param name="condition">if set to <c>true</c> throws an exception.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">The condition is met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:true => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotMeetCondition(Boolean condition, [InvokerParameterName] String argumentName, String message)
            {
                if (condition)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value" /> parameter of the param <paramref name="argumentName"/> of the calling method is equal to <paramref name="expectedValue"/>; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="expectedValue">The expected value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is not equal to expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void EqualTo<T>(T value, T expectedValue, [InvokerParameterName] String argumentName)
                where T : IEquatable<T>
            {
                if (!value.Equals(expectedValue))
                {
                    throw new ArgumentException($"{argumentName} must be equal to {expectedValue}", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value" /> parameter of the param <paramref name="argumentName"/> of the calling method is equal to <paramref name="expectedValue"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="expectedValue">The expected value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">Value is not equal to expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void EqualTo<T>(T value, T expectedValue, [InvokerParameterName] String argumentName, String message)
                where T : IEquatable<T>
            {
                if (!value.Equals(expectedValue))
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value" /> parameter of the param <paramref name="argumentName"/> of the calling method is not equal to <paramref name="notExpectedValue"/>; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="notExpectedValue">The value that is not expected.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is equal to not expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEqualTo<T>(T value, T notExpectedValue, [InvokerParameterName] String argumentName)
                where T : IEquatable<T>
            {
                if (value.Equals(notExpectedValue))
                {
                    throw new ArgumentException($"{argumentName} must not be equal to {notExpectedValue}", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value" /> parameter of the param <paramref name="argumentName"/> of the calling method is not equal to <paramref name="notExpectedValue"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="notExpectedValue">The value that is not expected.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">Value is equal to not expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEqualTo<T>(T value, T notExpectedValue, [InvokerParameterName] String argumentName, String message)
                where T : IEquatable<T>
            {
                if (value.Equals(notExpectedValue))
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is greater than <paramref name="minimumValue"/>; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is less than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThan<T>(T value, T minimumValue, [InvokerParameterName] String argumentName)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) <= 0)
                {
                    throw new ArgumentException($"{argumentName} must be greater than {minimumValue}", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is greater than <paramref name="minimumValue"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">Value is less than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThan<T>(T value, T minimumValue, [InvokerParameterName] String argumentName, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) <= 0)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is greater than or equal to <paramref name="minimumValue"/>; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is less than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThanOrEqualTo<T>(T value, T minimumValue, [InvokerParameterName] String argumentName)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) < 0)
                {
                    throw new ArgumentException($"{argumentName} must be greater than or equal to {minimumValue}", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is greater than or equal to <paramref name="minimumValue"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">Value is less than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThanOrEqualTo<T>(T value, T minimumValue, [InvokerParameterName] String argumentName, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) < 0)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is less than <paramref name="maximumValue"/>; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is greater than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThan<T>(T value, T maximumValue, [InvokerParameterName] String argumentName)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) >= 0)
                {
                    throw new ArgumentException($"{argumentName} must be less than {maximumValue}", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is less than <paramref name="maximumValue"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">Value is greater than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThan<T>(T value, T maximumValue, [InvokerParameterName] String argumentName, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) >= 0)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is less than or equal to <paramref name="maximumValue"/>; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is greater than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThanOrEqualTo<T>(T value, T maximumValue, [InvokerParameterName] String argumentName)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) > 0)
                {
                    throw new ArgumentException($"{argumentName} must be less than or equal to {maximumValue}", argumentName);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> of the calling method is less than or equal to <paramref name="maximumValue"/>; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">Type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">Value is greater than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThanOrEqualTo<T>(T value, T maximumValue, [InvokerParameterName] String argumentName, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) > 0)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is not an empty string; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEmpty(String value, [InvokerParameterName] String argumentName)
            {
                if (value == String.Empty)
                {
                    throw new ArgumentException($"{argumentName} must not be empty", argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is not an empty string; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The message.</param>
            /// <exception cref="ArgumentException">Value is an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEmpty(String value, [InvokerParameterName] String argumentName, String message)
            {
                if (value == String.Empty)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is an empty string; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is not an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Empty(String value, [InvokerParameterName] String argumentName)
            {
                if (value != String.Empty)
                {
                    throw new ArgumentException($"{argumentName} must not be empty", argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is an empty string; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The message.</param>
            /// <exception cref="ArgumentException">Value is not an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Empty(String value, [InvokerParameterName] String argumentName, String message)
            {
                if (value != String.Empty)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is not a null or empty string; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is a null or empty string.</exception>
            [PublicAPI]
            [ContractAnnotation("value:null => halt")]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNullOrEmpty(String value, [InvokerParameterName] String argumentName)
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{argumentName} must not be neither null nor empty", argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is not a null or empty string; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The message.</param>
            /// <exception cref="ArgumentException">Value is a null or empty string.</exception>
            [PublicAPI]
            [ContractAnnotation("value:null => halt")]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNullOrEmpty(String value, [InvokerParameterName] String argumentName, String message)
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is a null or empty string; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">Value is not a null or empty string.</exception>
            [PublicAPI]
            [ContractAnnotation("value:null => halt")]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NullOrEmpty(String value, [InvokerParameterName] String argumentName)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{argumentName} must not be neither null nor empty", argumentName);
                }
            }

            /// <summary>
            /// Check that the value provided in <paramref name="value"/> parameter of the param <paramref name="argumentName"/> is a null or empty string; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The message.</param>
            /// <exception cref="ArgumentException">Value is not a null or empty string.</exception>
            [PublicAPI]
            [ContractAnnotation("value:null => halt")]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NullOrEmpty(String value, [InvokerParameterName] String argumentName, String message)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has no elements; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">The collection is not empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasNoElements<T>(ICollection<T> collection, [InvokerParameterName] String argumentName)
            {
                if (collection.Count != 0)
                {
                    throw new ArgumentException($"The collection {argumentName} is not empty", argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has no elements; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">The collection is not empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasNoElements<T>(ICollection<T> collection, [InvokerParameterName] String argumentName, String message)
            {
                if (collection.Count != 0)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has elements; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">The collection is empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasElements<T>(ICollection<T> collection, [InvokerParameterName] String argumentName)
            {
                if (collection.Count == 0)
                {
                    throw new ArgumentException($"The collection {argumentName} is empty", argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has elements; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">The collection is empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasElements<T>(ICollection<T> collection, [InvokerParameterName] String argumentName, String message)
            {
                if (collection.Count == 0)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has exactly one element; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">The collection has no or more than one elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasSingleElement<T>(ICollection<T> collection, [InvokerParameterName] String argumentName)
            {
                if (collection.Count != 1)
                {
                    throw new ArgumentException($"The collection {argumentName} has no or more than one elements", argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has exactly one element; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">The collection has no or more than one elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasSingleElement<T>(ICollection<T> collection, [InvokerParameterName] String argumentName, String message)
            {
                if (collection.Count != 1)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }

            /// <summary>
            /// Checks that the specified collection has exactly one element; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="noElementsMessage">The exception message if the collection has no elements.</param>
            /// <param name="multipleElementsMessage">The exception message if the collection have more than one element.</param>
            /// <exception cref="ArgumentException">The collection has no or more than one elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasSingleElement<T>(ICollection<T> collection, [InvokerParameterName] String argumentName, String noElementsMessage, String multipleElementsMessage)
            {
                if (collection.Count == 0)
                {
                    throw new ArgumentException(noElementsMessage, argumentName);
                }

                if (collection.Count > 1)
                {
                    throw new ArgumentException(multipleElementsMessage, argumentName);
                }
            }

            /// <summary>
            /// Check that the specified collection has exact number of elements; otherwise throws <see cref="ArgumentException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="count">The required number of elements.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="ArgumentException">The collection does not have exact number of elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasExactNumberOfElements<T>(ICollection<T> collection, Int32 count, [InvokerParameterName] String argumentName)
            {
                if (collection.Count != count)
                {
                    throw new ArgumentException($"The collection {collection} does not have exactly {count} elements", argumentName);
                }
            }

            /// <summary>
            /// Check that the specified collection has exact number of elements; otherwise throws <see cref="ArgumentException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="count">The required number of elements.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="ArgumentException">The collection does not have exact number of elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasExactNumberOfElements<T>(ICollection<T> collection, Int32 count, [InvokerParameterName] String argumentName, String message)
            {
                if (collection.Count != count)
                {
                    throw new ArgumentException(message, argumentName);
                }
            }
        }
    }
}
