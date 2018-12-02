using System;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Defines interface for secure code generation.
    /// </summary>
    public interface ISecureCodeGenerationService
    {
        /// <summary>
        /// Generates the alpha numeric code.
        /// </summary>
        /// <param name="size">The size of the code.</param>
        /// <returns>A generated code.</returns>
        String GenerateAlphaNumericCode(Int32 size);

        /// <summary>
        /// Generates the numeric code.
        /// </summary>
        /// <param name="size">The size of the code.</param>
        /// <returns>A generated code.</returns>
        String GenerateNumericCode(Int32 size);

        /// <summary>
        /// Generates the custom code.
        /// </summary>
        /// <param name="size">The size of the code.</param>
        /// <param name="characters">The characters that should be used for code generation.</param>
        /// <returns>A generated code.</returns>
        String GenerateCustomCode(Int32 size, String characters);
    }
}
