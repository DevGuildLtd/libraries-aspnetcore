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
        /// State-related condition tests.
        /// </summary>
        public static class State
        {
            /// <summary>
            /// Checks that the provided value is not null; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">The provided value is null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNull<T>(T value)
                where T : class
            {
                if (value == null)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided value is not null; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The provided value is null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNull<T>(T value, String message)
                where T : class
            {
                if (value == null)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided value is null; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">The provided value is not null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Null<T>(T value)
                where T : class
            {
                if (value != null)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided value is null; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The provided value is not null.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Null<T>(T value, String message)
                where T : class
            {
                if (value != null)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided nullable value has a value; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">The nullable value does not have a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasValue<T>(T? value)
                where T : struct
            {
                if (!value.HasValue)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided nullable value has a value; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The nullable value does not have a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:null => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasValue<T>(T? value, String message)
                where T : struct
            {
                if (!value.HasValue)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided nullable value does not have a value; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">The nullable value has a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotHaveValue<T>(T? value)
                where T : struct
            {
                if (value.HasValue)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided nullable value does not have a value; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The nullable value has a value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("value:notnull => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotHaveValue<T>(T? value, String message)
                where T : struct
            {
                if (value.HasValue)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> is met; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <param name="condition">if set to <c>false</c> throws an exception.</param>
            /// <exception cref="InvalidOperationException">The condition is not met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:false => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void MeetCondition(Boolean condition)
            {
                if (!condition)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> is met; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <param name="condition">if set to <c>false</c> throws an exception.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The condition is not met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:false => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void MeetCondition(Boolean condition, String message)
            {
                if (!condition)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> is not met; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <param name="condition">if set to <c>true</c> throws an exception.</param>
            /// <exception cref="InvalidOperationException">The condition is met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:true => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotMeetCondition(Boolean condition)
            {
                if (condition)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the condition calculated as a parameter <paramref name="condition"/> is not met; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <param name="condition">if set to <c>true</c> throws an exception.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The condition is met.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [ContractAnnotation("condition:true => halt")]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void DoesNotMeetCondition(Boolean condition, String message)
            {
                if (condition)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is equal to <paramref name="expectedValue"/>; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="expectedValue">The expected value.</param>
            /// <exception cref="InvalidOperationException">The value is not equal to the expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void EqualTo<T>(T value, T expectedValue)
                where T : IEquatable<T>
            {
                if (!value.Equals(expectedValue))
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is equal to <paramref name="expectedValue"/>; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="expectedValue">The expected value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The value is not equal to the expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void EqualTo<T>(T value, T expectedValue, String message)
                where T : IEquatable<T>
            {
                if (!value.Equals(expectedValue))
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is not equal to <paramref name="notExpectedValue"/>; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="notExpectedValue">The not expected value.</param>
            /// <exception cref="InvalidOperationException">The value is equal to the not expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEqualTo<T>(T value, T notExpectedValue)
                where T : IEquatable<T>
            {
                if (value.Equals(notExpectedValue))
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is not equal to <paramref name="notExpectedValue"/>; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="notExpectedValue">The not expected value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The value is equal to the not expected value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEqualTo<T>(T value, T notExpectedValue, String message)
                where T : IEquatable<T>
            {
                if (value.Equals(notExpectedValue))
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is greater than <paramref name="minimumValue"/>; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <exception cref="InvalidOperationException">The value is less than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThan<T>(T value, T minimumValue)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) <= 0)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is greater than <paramref name="minimumValue"/>; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The value is less than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThan<T>(T value, T minimumValue, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) <= 0)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is greater than or equal to <paramref name="minimumValue"/>; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <exception cref="InvalidOperationException">The value is less than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThanOrEqualTo<T>(T value, T minimumValue)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) < 0)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is greater than or equal to <paramref name="minimumValue"/>; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="minimumValue">The minimum value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The value is less than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void GreaterThanOrEqualTo<T>(T value, T minimumValue, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(minimumValue) < 0)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is less than <paramref name="maximumValue"/>; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <exception cref="InvalidOperationException">The value is greater than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThan<T>(T value, T maximumValue)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) >= 0)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is less than <paramref name="maximumValue"/>; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The value is greater than or equal to the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThan<T>(T value, T maximumValue, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) >= 0)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is less than or equal to <paramref name="maximumValue"/>; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <exception cref="InvalidOperationException">The value is greater than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThanOrEqualTo<T>(T value, T maximumValue)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) > 0)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the value provided in <paramref name="value"/> parameter is less than or equal to <paramref name="maximumValue"/>; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the value.</typeparam>
            /// <param name="value">The value.</param>
            /// <param name="maximumValue">The maximum value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The value is greater than the minimum value.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void LessThanOrEqualTo<T>(T value, T maximumValue, String message)
                where T : IComparable<T>
            {
                if (value.CompareTo(maximumValue) > 0)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided value is not an empty string; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">Value is an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEmpty(String value)
            {
                if (value == String.Empty)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided value is not an empty string; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">Value is an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotEmpty(String value, String message)
            {
                if (value == String.Empty)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided value is an empty string; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">Value is not an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Empty(String value)
            {
                if (value != String.Empty)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided value is an empty string; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">Value is not an empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Empty(String value, String message)
            {
                if (value != String.Empty)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided value is not a null or empty string; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">Value is a null or empty string.</exception>
            [PublicAPI]
            [ContractAnnotation("value:null => halt")]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNullOrEmpty(String value)
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided value is not a null or empty string; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">Value is a null or empty string.</exception>
            [PublicAPI]
            [ContractAnnotation("value:null => halt")]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NotNullOrEmpty(String value, String message)
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the provided value is either a null or empty string; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <exception cref="InvalidOperationException">Value is not a null or empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NullOrEmpty(String value)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the provided value is either a null or empty string; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">Value is not a null or empty string.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void NullOrEmpty(String value, String message)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the specified collection has no elements; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <exception cref="InvalidOperationException">The collection is not empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasNoElements<T>(ICollection<T> collection)
            {
                if (collection.Count != 0)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the specified collection has no elements; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The collection is not empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasNoElements<T>(ICollection<T> collection, String message)
            {
                if (collection.Count != 0)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the specified collection has elements; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <exception cref="InvalidOperationException">The collection is empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasElements<T>(ICollection<T> collection)
            {
                if (collection.Count == 0)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the specified collection has elements; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The collection is empty.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasElements<T>(ICollection<T> collection, String message)
            {
                if (collection.Count == 0)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the specified collection has exactly one element; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <exception cref="InvalidOperationException">The collection has no or more than one elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasSingleElement<T>(ICollection<T> collection)
            {
                if (collection.Count != 1)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Checks that the specified collection has exactly one element; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The collection has no or more than one elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasSingleElement<T>(ICollection<T> collection, String message)
            {
                if (collection.Count != 1)
                {
                    throw new InvalidOperationException(message);
                }
            }

            /// <summary>
            /// Checks that the specified collection has exactly one element; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="noElementsMessage">The exception message if the collection has no elements.</param>
            /// <param name="multipleElementsMessage">The exception message if the collection have more than one element.</param>
            /// <exception cref="InvalidOperationException">The collection has no or more than one elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasSingleElement<T>(ICollection<T> collection, String noElementsMessage, String multipleElementsMessage)
            {
                if (collection.Count == 0)
                {
                    throw new InvalidOperationException(noElementsMessage);
                }

                if (collection.Count > 1)
                {
                    throw new InvalidOperationException(multipleElementsMessage);
                }
            }

            /// <summary>
            /// Check that the specified collection has exact number of elements; otherwise throws <see cref="InvalidOperationException"/>.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="count">The required number of elements.</param>
            /// <exception cref="InvalidOperationException">The collection does not have exact number of elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasExactNumberOfElements<T>(ICollection<T> collection, Int32 count)
            {
                if (collection.Count != count)
                {
                    throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Check that the specified collection has exact number of elements; otherwise throws <see cref="InvalidOperationException"/> with the provided message.
            /// </summary>
            /// <typeparam name="T">The type of the collection element.</typeparam>
            /// <param name="collection">The collection.</param>
            /// <param name="count">The required number of elements.</param>
            /// <param name="message">The exception message.</param>
            /// <exception cref="InvalidOperationException">The collection does not have exact number of elements.</exception>
            [PublicAPI]
            [DebuggerStepThrough]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void HasExactNumberOfElements<T>(ICollection<T> collection, Int32 count, String message)
            {
                if (collection.Count != count)
                {
                    throw new InvalidOperationException(message);
                }
            }
        }
    }
}
