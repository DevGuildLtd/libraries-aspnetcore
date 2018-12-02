using System;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.EntitySequences
{
    /// <summary>
    /// A service for entity-base sequence number generation.
    /// </summary>
    public interface IEntitySequenceService
    {
        /// <summary>
        /// Asynchronously peeks the next number in the sequence of the specified entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns>The next value to be returned by the sequence.</returns>
        Task<Int64> PeekNextNumberAsync(String key);

        /// <summary>
        /// Asynchronously takes the next number from the sequence of the specified entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns>The next value of the sequence.</returns>
        Task<Int64> TakeNextNumberAsync(String key);

        /// <summary>
        /// Asynchronously Takes the multiple numbers from the sequence of the specified entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <param name="count">The number of numbers to take.</param>
        /// <returns>An array of taken numbers.</returns>
        Task<Int64[]> TakeMultipleNumbersAsync(String key, Int64 count);
    }
}
