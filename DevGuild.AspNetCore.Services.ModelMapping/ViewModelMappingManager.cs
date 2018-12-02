using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Represents default implementation of the view model mapping manager.
    /// </summary>
    /// <seealso cref="IViewModelMappingManager" />
    public class ViewModelMappingManager : IViewModelMappingManager
    {
        private readonly ConcurrentDictionary<(Type, Type), Object> cache = new ConcurrentDictionary<(Type, Type), Object>();
        private readonly ConcurrentDictionary<(Type, Type), Object> modelIdentifierMapperCache = new ConcurrentDictionary<(Type, Type), Object>();
        private readonly ConcurrentDictionary<(Type, Type), Object> viewModelIdentifierMapperCache = new ConcurrentDictionary<(Type, Type), Object>();

        /// <inheritdoc />
        public IViewModelMapper<TModel, TViewModel> GetModelMapper<TModel, TViewModel>()
        {
            var modelType = typeof(TModel);
            var viewModelType = typeof(TViewModel);
            var mapper = this.cache.GetOrAdd((modelType, viewModelType), key => new ViewModelMapper<TModel, TViewModel>()) as IViewModelMapper<TModel, TViewModel>;
            return mapper;
        }

        /// <inheritdoc />
        public IModelIdentifierMapper<TIdentifier, TModel> GetModelIdentifierMapper<TIdentifier, TModel>()
            where TModel : class
        {
            var identifierType = typeof(TIdentifier);
            var modelType = typeof(TModel);
            var mapper = this.modelIdentifierMapperCache.GetOrAdd((identifierType, modelType), key => new ModelIdentifierMapper<TIdentifier, TModel>()) as IModelIdentifierMapper<TIdentifier, TModel>;
            return mapper;
        }

        /// <inheritdoc />
        public IViewModelIdentifierMapper<TIdentifier, TViewModel> GetViewModelIdentifierMapper<TIdentifier, TViewModel>()
            where TViewModel : class
        {
            var identifierType = typeof(TIdentifier);
            var modelType = typeof(TViewModel);
            var mapper = this.viewModelIdentifierMapperCache.GetOrAdd((identifierType, modelType), key => new ViewModelIdentifierMapper<TIdentifier, TViewModel>()) as IViewModelIdentifierMapper<TIdentifier, TViewModel>;
            return mapper;
        }
    }
}
