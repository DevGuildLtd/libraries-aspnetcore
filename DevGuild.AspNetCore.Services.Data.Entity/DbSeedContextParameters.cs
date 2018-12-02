using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents parameters of the database seeding context.
    /// </summary>
    public class DbSeedContextParameters
    {
        private readonly Dictionary<String, Object> values = new Dictionary<String, Object>();

        /// <summary>
        /// Sets the parameter value associated with the specified key.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The parameter key.</param>
        /// <param name="value">The parameter value.</param>
        public void SetParameter<TValue>(String key, TValue value)
        {
            this.values[key] = value;
        }

        /// <summary>
        /// Gets the parameter value associated with the specified key.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The parameter key.</param>
        /// <returns>The parameter value.</returns>
        public TValue GetParameter<TValue>(String key)
        {
            return (TValue)this.values[key];
        }

        /// <summary>
        /// Tries the get parameter value associated with the specified key.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Boolean TryGetParameter<TValue>(String key, out TValue value)
        {
            if (this.values.TryGetValue(key, out var objectValue) && objectValue is TValue typedValue)
            {
                value = typedValue;
                return true;
            }

            value = default(TValue);
            return false;
        }
    }
}
