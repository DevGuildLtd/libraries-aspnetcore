using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.ModelMapping.Annotations;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Represents default implementation of the view model mapper.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <seealso cref="ModelMapping.IViewModelMapper{TEntity, TViewModel}" />
    public class ViewModelMapper<TEntity, TViewModel> : IViewModelMapper<TEntity, TViewModel>
    {
        // TODO: Implement runtime expression compilation to increase mapping speed
        private (PropertyInfo Model, PropertyInfo ViewModel)[] keyPropertiesMapping;
        private (PropertyInfo Model, PropertyInfo ViewModel)[] detailsPropertiesMapping;
        private (PropertyInfo Model, PropertyInfo ViewModel)[] createPropertiesMapping;
        private (PropertyInfo Model, PropertyInfo ViewModel)[] updatePropertiesMapping;

        /// <inheritdoc />
        public Task<Expression<Func<TEntity, Boolean>>> BuildSingleQueryExpressionAsync(TViewModel viewModel)
        {
            var modelType = typeof(TEntity);
            var viewModelType = typeof(TViewModel);

            var propertiesMapping = this.keyPropertiesMapping ?? (this.keyPropertiesMapping = this.MapKeyProperties(modelType, viewModelType));

            var parameterX = Expression.Parameter(modelType, "x");
            var constViewModel = Expression.Constant(viewModel, viewModelType);

            var property = propertiesMapping[0];
            var expression = Expression.Equal(Expression.Property(parameterX, property.Model), Expression.Property(constViewModel, property.ViewModel));
            for (var i = 1; i < propertiesMapping.Length; i++)
            {
                property = propertiesMapping[i];
                var equality = Expression.Equal(Expression.Property(parameterX, property.Model), Expression.Property(constViewModel, property.ViewModel));
                expression = Expression.AndAlso(expression, equality);
            }

            return Task.FromResult(Expression.Lambda<Func<TEntity, Boolean>>(expression, parameterX));
        }

        /// <inheritdoc />
        public TViewModel ConvertEntityToViewModel(TEntity entity, String[] allowedProperties)
        {
            var modelType = typeof(TEntity);
            var viewModelType = typeof(TViewModel);

            // Strategy 0: View model has a constructor that accepts TEntity
            var viewModelConstructor = viewModelType.GetConstructor(new[] { typeof(TEntity) });
            if (viewModelConstructor != null)
            {
                return (TViewModel)viewModelConstructor.Invoke(new Object[] { entity });
            }

            var defaultConstructor = viewModelType.GetConstructor(new Type[] { });
            if (defaultConstructor == null)
            {
                throw new InvalidOperationException("Unable to construct TViewModel");
            }

            var viewModel = (TViewModel)defaultConstructor.Invoke(new Object[] { });
            var propertiesMapping = this.detailsPropertiesMapping ?? (this.detailsPropertiesMapping = this.MapAllProperties(modelType, viewModelType, PropertyMappingMode.ToDetails));
            foreach (var propertyMapping in propertiesMapping)
            {
                if (allowedProperties == null || allowedProperties.Contains(propertyMapping.Model.Name))
                {
                    propertyMapping.ViewModel.SetValue(viewModel, propertyMapping.Model.GetValue(entity));
                }
            }

            return viewModel;
        }

        /// <inheritdoc />
        public Task InitializeEditModelAsync(TEntity entity, TViewModel viewModel, String[] allowedProperties)
        {
            var modelType = typeof(TEntity);
            var viewModelType = typeof(TViewModel);

            var propertiesMapping = this.updatePropertiesMapping ?? (this.updatePropertiesMapping = this.MapAllProperties(modelType, viewModelType, PropertyMappingMode.FromUpdate));
            foreach (var propertyMapping in propertiesMapping)
            {
                if (allowedProperties == null || allowedProperties.Contains(propertyMapping.Model.Name))
                {
                    propertyMapping.ViewModel.SetValue(viewModel, propertyMapping.Model.GetValue(entity));
                }
            }

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task InitializeNewEntityAsync(TEntity entity, TViewModel viewModel, String[] allowedProperties)
        {
            var modelType = typeof(TEntity);
            var viewModelType = typeof(TViewModel);

            var propertiesMapping = this.createPropertiesMapping ?? (this.createPropertiesMapping = this.MapAllProperties(modelType, viewModelType, PropertyMappingMode.FromCreate));
            foreach (var propertyMapping in propertiesMapping)
            {
                if (allowedProperties == null || allowedProperties.Contains(propertyMapping.Model.Name))
                {
                    propertyMapping.Model.SetValue(entity, propertyMapping.ViewModel.GetValue(viewModel));
                }
            }

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task UpdateExistingEntityAsync(TEntity entity, TViewModel viewModel, String[] allowedProperties)
        {
            var modelType = typeof(TEntity);
            var viewModelType = typeof(TViewModel);

            var propertiesMapping = this.updatePropertiesMapping ?? (this.updatePropertiesMapping = this.MapAllProperties(modelType, viewModelType, PropertyMappingMode.FromUpdate));
            foreach (var propertyMapping in propertiesMapping)
            {
                if (allowedProperties == null || allowedProperties.Contains(propertyMapping.Model.Name))
                {
                    propertyMapping.Model.SetValue(entity, propertyMapping.ViewModel.GetValue(viewModel));
                }
            }

            return Task.FromResult(0);
        }

        #region Key Properties Mapping

        private PropertyInfo[] GetKeyProperties(Type modelType)
        {
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite).ToList();

            var keyProperties = this.FindModelPropertiesWithKeyAttribute(modelProperties).ToArray();
            if (keyProperties.Length > 0)
            {
                return keyProperties;
            }

            keyProperties = this.FindModelPropertiesWithIdName(modelProperties).ToArray();
            if (keyProperties.Length > 0)
            {
                return keyProperties;
            }

            throw new InvalidOperationException($"Unable to identify {modelType} key properties");
        }

        private (PropertyInfo Model, PropertyInfo ViewModel)[] MapKeyProperties(Type modelType, Type viewModelType)
        {
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite).ToList();
            var viewModelProperties = viewModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite).ToList();

            var mappedProperties = this.FindPropertiesWithKeyMappingAttribute(modelProperties, viewModelProperties).ToArray();
            if (mappedProperties.Length > 0)
            {
                return mappedProperties;
            }

            mappedProperties = this.FindPropertiesWithKeyAttribute(modelProperties, viewModelProperties).ToArray();
            if (mappedProperties.Length > 0)
            {
                return mappedProperties;
            }

            mappedProperties = this.FindPropertiesWithIdName(modelProperties, viewModelProperties).ToArray();
            if (mappedProperties.Length > 0)
            {
                return mappedProperties;
            }

            throw new InvalidOperationException("Unable to map Model and ViewModel key properties");
        }

        private IEnumerable<(PropertyInfo Model, PropertyInfo ViewModel)> FindPropertiesWithKeyMappingAttribute(List<PropertyInfo> modelProperties, List<PropertyInfo> viewModelProperties)
        {
            // Strategy 1: Check for KeyMappingAttribute
            var keyMappedProperties = viewModelProperties.Select(x => new
            {
                Property = x,
                Attribute = x.GetCustomAttribute<KeyMappingAttribute>()
            }).Where(x => x.Attribute != null).ToList();

            if (keyMappedProperties.Count > 0)
            {
                foreach (var viewModelProperty in keyMappedProperties)
                {
                    var modelProperty = modelProperties.SingleOrDefault(x => x.Name == viewModelProperty.Attribute.EntityProperty);
                    if (modelProperty == null)
                    {
                        throw new InvalidOperationException("KeyMappingAttribute.EntityProprety points to non-existing property");
                    }

                    yield return (modelProperty, viewModelProperty.Property);
                }
            }
        }

        private IEnumerable<(PropertyInfo Model, PropertyInfo ViewModel)> FindPropertiesWithKeyAttribute(List<PropertyInfo> modelProperties, List<PropertyInfo> viewModelProperties)
        {
            // Strategy 2: Check for KeyAttribute in entity model
            var keyMappedProperties = modelProperties.Select(x => new
            {
                Property = x,
                Attribute = x.GetCustomAttribute<KeyAttribute>()
            }).Where(x => x.Attribute != null).ToList();

            if (keyMappedProperties.Count > 0)
            {
                foreach (var modelProperty in keyMappedProperties)
                {
                    var viewModelProperty = viewModelProperties.SingleOrDefault(x => x.Name == modelProperty.Property.Name);
                    if (viewModelProperty == null)
                    {
                        throw new InvalidOperationException("Property marked with KeyAttribute does not have a counterpart in ViewModel");
                    }

                    yield return (modelProperty.Property, viewModelProperty);
                }
            }
        }

        private IEnumerable<PropertyInfo> FindModelPropertiesWithKeyAttribute(List<PropertyInfo> modelProperties)
        {
            return modelProperties.Select(x => new
            {
                Property = x,
                KeyAttribute = x.GetCustomAttribute<KeyAttribute>(),
                ColumnAttribute = x.GetCustomAttribute<ColumnAttribute>()
            })
                .Where(x => x.KeyAttribute != null)
                .OrderBy(x => x.ColumnAttribute?.Order ?? 0)
                .Select(x => x.Property);
        }

        private IEnumerable<(PropertyInfo Model, PropertyInfo ViewModel)> FindPropertiesWithIdName(List<PropertyInfo> modelProperties, List<PropertyInfo> viewModelProperties)
        {
            // Strategy 3: Look for property named "Id" in both models
            var modelPropertyId = modelProperties.SingleOrDefault(x => String.Equals(x.Name, "Id", StringComparison.OrdinalIgnoreCase));
            var viewModelPropertyId = viewModelProperties.SingleOrDefault(x => String.Equals(x.Name, "Id", StringComparison.OrdinalIgnoreCase));

            if (modelPropertyId == null || viewModelPropertyId == null || modelPropertyId.PropertyType != viewModelPropertyId.PropertyType)
            {
                throw new InvalidOperationException("Unable to identify model or viewmodel key properties");
            }

            yield return (modelPropertyId, viewModelPropertyId);
        }

        private IEnumerable<PropertyInfo> FindModelPropertiesWithIdName(List<PropertyInfo> modelProperties)
        {
            var modelPropertyId = modelProperties.SingleOrDefault(x => String.Equals(x.Name, "Id", StringComparison.OrdinalIgnoreCase));

            if (modelPropertyId == null)
            {
                throw new InvalidOperationException("Unable to identify model key properties");
            }

            yield return modelPropertyId;
        }

        #endregion

        private (PropertyInfo Model, PropertyInfo ViewModel)[] MapAllProperties(Type modelType, Type viewModelType, PropertyMappingMode requiredMode)
        {
            var defaultMode = viewModelType.GetCustomAttribute<DefaultPropertyMappingModeAttribute>()?.Mode ?? PropertyMappingMode.All;
            var modelProperties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite).ToList();
            var viewModelProperties = viewModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite).ToList();

            var result = new List<(PropertyInfo Model, PropertyInfo ViewModel)>();

            foreach (var viewModelProperty in viewModelProperties)
            {
                var attribute = viewModelProperty.GetCustomAttribute<PropertyMappingAttribute>();
                var mode = attribute?.Mode ?? defaultMode;
                if ((mode & requiredMode) != requiredMode)
                {
                    continue;
                }

                var propertyName = attribute?.PropertyName ?? viewModelProperty.Name;
                var modelProperty = modelProperties.SingleOrDefault(x => x.Name == propertyName && x.PropertyType == viewModelProperty.PropertyType);
                if (modelProperty == null)
                {
                    if (attribute != null)
                    {
                        throw new InvalidOperationException("Mapped property not found");
                    }

                    continue;
                }

                result.Add((modelProperty, viewModelProperty));
            }

            return result.ToArray();
        }
    }
}
