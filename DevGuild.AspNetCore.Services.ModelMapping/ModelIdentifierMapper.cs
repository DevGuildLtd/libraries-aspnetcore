using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Contracts;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Represents implementation of the model identifier mapper.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="IModelIdentifierMapper{TIdentifier, TModel}" />
    public class ModelIdentifierMapper<TIdentifier, TModel> : IModelIdentifierMapper<TIdentifier, TModel>
        where TModel : class
    {
        /// <inheritdoc />
        public Task<PropertyInfo> GetModelIdentifierPropertyAsync()
        {
            var modelType = typeof(TModel);
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite)
                .ToArray();

            var keyProperties = modelProperties.Where(x => x.GetCustomAttribute<KeyAttribute>() != null).ToArray();

            Ensure.State.DoesNotMeetCondition(keyProperties.Length > 1, "Multiple properties with KeyAttribute found");
            if (keyProperties.Length == 1)
            {
                Ensure.State.MeetCondition(keyProperties[0].PropertyType == typeof(TIdentifier), "Property with KeyAttribute is of invalid type");
                return Task.FromResult(keyProperties[0]);
            }

            keyProperties = modelProperties.Where(x => x.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase)).ToArray();
            Ensure.State.DoesNotMeetCondition(keyProperties.Length > 1, "Multiple properties with name equal Id found");
            if (keyProperties.Length == 1)
            {
                Ensure.State.MeetCondition(keyProperties[0].PropertyType == typeof(TIdentifier), "Id property is of invalid type");
                return Task.FromResult(keyProperties[0]);
            }

            throw new InvalidOperationException($"Unable to identify identifier property of type {modelType}");
        }

        /// <inheritdoc />
        public Task<PropertyInfo> GetModelReferencePropertyAsync<TReferencedIdentifier, TReferencedType>()
        {
            var modelType = typeof(TModel);
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite)
                .ToArray();

            var referenceProperty = modelProperties.Where(x => x.PropertyType == typeof(TReferencedType)).ToArray();
            Ensure.State.DoesNotMeetCondition(referenceProperty.Length == 0, $"No property of type {typeof(TReferencedType)} was found");
            Ensure.State.DoesNotMeetCondition(referenceProperty.Length > 1, $"More than one property of type {typeof(TReferencedType)} was found");

            var referenceIdProperty = modelProperties.SingleOrDefault(x => x.Name == $"{referenceProperty[0].Name}Id" && x.PropertyType == typeof(TReferencedIdentifier));
            Ensure.State.NotNull(referenceIdProperty, $"Related identifier property was not found for referenced property {referenceProperty[0].Name}");

            return Task.FromResult(referenceProperty[0]);
        }

        /// <inheritdoc />
        public Task<PropertyInfo> GetModelReferenceIdPropertyAsync<TReferencedIdentifier, TReferencedType>()
        {
            var modelType = typeof(TModel);
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite)
                .ToArray();

            var referenceProperty = modelProperties.Where(x => x.PropertyType == typeof(TReferencedType)).ToArray();
            Ensure.State.DoesNotMeetCondition(referenceProperty.Length == 0, $"No property of type {typeof(TReferencedType)} was found");
            Ensure.State.DoesNotMeetCondition(referenceProperty.Length > 1, $"More than one property of type {typeof(TReferencedType)} was found");

            var referenceIdProperty = modelProperties.SingleOrDefault(x => x.Name == $"{referenceProperty[0].Name}Id" && x.PropertyType == typeof(TReferencedIdentifier));
            Ensure.State.NotNull(referenceIdProperty, $"Related identifier property was not found for referenced property {referenceProperty[0].Name}");

            return Task.FromResult(referenceIdProperty);
        }

        /// <inheritdoc />
        public async Task<TIdentifier> GetModelIdentifierAsync(TModel model)
        {
            Ensure.Argument.NotNull(model, nameof(model));

            var property = await this.GetModelIdentifierPropertyAsync();
            Ensure.State.NotNull(property, "Unable to identify identifier property");

            return (TIdentifier)property.GetValue(model);
        }

        /// <inheritdoc />
        public async Task<TReferencedType> GetModelReferenceAsync<TReferencedIdentifier, TReferencedType>(TModel model)
        {
            Ensure.Argument.NotNull(model, nameof(model));

            var property = await this.GetModelReferencePropertyAsync<TReferencedIdentifier, TReferencedType>();
            Ensure.State.NotNull(property, "Unable to identify reference property");

            return (TReferencedType)property.GetValue(model);
        }

        /// <inheritdoc />
        public async Task<TReferencedIdentifier> GetModelReferenceIdAsync<TReferencedIdentifier, TReferencedType>(TModel model)
        {
            Ensure.Argument.NotNull(model, nameof(model));

            var property = await this.GetModelReferenceIdPropertyAsync<TReferencedIdentifier, TReferencedType>();
            Ensure.State.NotNull(property, "Unable to identify reference identifier property");

            return (TReferencedIdentifier)property.GetValue(model);
        }
    }
}
