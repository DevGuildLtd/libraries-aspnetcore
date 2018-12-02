using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Represents implementation of the secure code generation service.
    /// </summary>
    /// <seealso cref="ISecureCodeGenerationService" />
    public class SecureCodeGenerationService : ISecureCodeGenerationService
    {
        private const String Alphanumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const String Numeric = "1234567890";

        /// <inheritdoc />
        public String GenerateAlphaNumericCode(Int32 size)
        {
            return this.GenerateCustomCode(size, SecureCodeGenerationService.Alphanumeric);
        }

        /// <inheritdoc />
        public String GenerateNumericCode(Int32 size)
        {
            return this.GenerateCustomCode(size, SecureCodeGenerationService.Numeric);
        }

        /// <inheritdoc />
        public String GenerateCustomCode(Int32 size, String characters)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new Byte[sizeof(UInt32) * size];
                rng.GetBytes(buffer);

                var result = new Char[size];
                for (var i = 0; i < size; i++)
                {
                    var baseIndex = BitConverter.ToUInt32(buffer, i * sizeof(UInt32));
                    var index = baseIndex % characters.Length;
                    result[i] = characters[(Int32)index];
                }

                return new String(result);
            }
        }
    }
}
