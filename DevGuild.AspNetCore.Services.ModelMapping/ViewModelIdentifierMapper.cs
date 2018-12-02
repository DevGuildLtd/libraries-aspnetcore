using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Contracts;
using DevGuild.AspNetCore.Services.ModelMapping.Annotations;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Represents default implementation of the view model identifier mapper.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <seealso cref="IViewModelIdentifierMapper{TIdentifier, TViewModel}" />
    public class ViewModelIdentifierMapper<TIdentifier, TViewModel> : IViewModelIdentifierMapper<TIdentifier, TViewModel>
        where TViewModel : class
    {
        /// <inheritdoc />
        public Task<PropertyInfo> GetViewModelIdentifierPropertyAsync()
        {
            var modelType = typeof(TViewModel);
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite)
                .ToArray();

            var keyProperties = modelProperties.Where(x => x.GetCustomAttribute<KeyMappingAttribute>() != null).ToArray();
            Ensure.State.DoesNotMeetCondition(keyProperties.Length > 1, "Multiple properties with KeyMappingAttribute found");
            if (keyProperties.Length == 1)
            {
                Ensure.State.MeetCondition(keyProperties[0].PropertyType == typeof(TIdentifier), "Property with KeyMappingAttribute is of invalid type");
                return Task.FromResult(keyProperties[0]);
            }

            keyProperties = modelProperties.Where(x => x.GetCustomAttribute<KeyAttribute>() != null).ToArray();
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
        public async Task<TIdentifier> GetViewModelIdentifierAsync(TViewModel model)
        {
            Ensure.Argument.NotNull(model, nameof(model));

            var property = await this.GetViewModelIdentifierPropertyAsync();
            Ensure.State.NotNull(property, "Unable to identify identifier property");

            return (TIdentifier)property.GetValue(model);
        }
    }
}
