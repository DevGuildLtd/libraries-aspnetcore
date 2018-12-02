using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Defines model identifier mapper interface.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IModelIdentifierMapper<TIdentifier, in TModel>
    {
        /// <summary>
        /// Asynchronously gets the model identifier property.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task<PropertyInfo> GetModelIdentifierPropertyAsync();

        /// <summary>
        /// Asynchronously gets the model identifier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<TIdentifier> GetModelIdentifierAsync(TModel model);

        /// <summary>
        /// Asynchronously gets the model reference property.
        /// </summary>
        /// <typeparam name="TReferencedIdentifier">The type of the referenced identifier.</typeparam>
        /// <typeparam name="TReferencedType">The type of the referenced type.</typeparam>
        /// <returns>A task that represents the operation.</returns>
        Task<PropertyInfo> GetModelReferencePropertyAsync<TReferencedIdentifier, TReferencedType>();

        /// <summary>
        /// Asynchronously gets the model reference identifier property.
        /// </summary>
        /// <typeparam name="TReferencedIdentifier">The type of the referenced identifier.</typeparam>
        /// <typeparam name="TReferencedType">The type of the referenced type.</typeparam>
        /// <returns>A task that represents the operation.</returns>
        Task<PropertyInfo> GetModelReferenceIdPropertyAsync<TReferencedIdentifier, TReferencedType>();

        /// <summary>
        /// Asynchronously gets the model reference.
        /// </summary>
        /// <typeparam name="TReferencedIdentifier">The type of the referenced identifier.</typeparam>
        /// <typeparam name="TReferencedType">The type of the referenced type.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<TReferencedType> GetModelReferenceAsync<TReferencedIdentifier, TReferencedType>(TModel model);

        /// <summary>
        /// Asynchronously gets the model reference identifier.
        /// </summary>
        /// <typeparam name="TReferencedIdentifier">The type of the referenced identifier.</typeparam>
        /// <typeparam name="TReferencedType">The type of the referenced type.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<TReferencedIdentifier> GetModelReferenceIdAsync<TReferencedIdentifier, TReferencedType>(TModel model);
    }
}
