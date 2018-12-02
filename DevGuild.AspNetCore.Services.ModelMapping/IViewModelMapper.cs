using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Defines view model mapper interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public interface IViewModelMapper<TEntity, TViewModel>
    {
        /// <summary>
        /// Asynchronously builds the single query expression from the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model containing entity identifier.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<Expression<Func<TEntity, Boolean>>> BuildSingleQueryExpressionAsync(TViewModel viewModel);

        /// <summary>
        /// Converts the entity to view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="allowedProperties">The allowed properties.</param>
        /// <returns>Converted view model.</returns>
        TViewModel ConvertEntityToViewModel(TEntity entity, String[] allowedProperties);

        /// <summary>
        /// Asynchronously initializes the edit model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="viewModel">The edit model.</param>
        /// <param name="allowedProperties">The allowed properties.</param>
        /// <returns>A task that represents the operation.</returns>
        Task InitializeEditModelAsync(TEntity entity, TViewModel viewModel, String[] allowedProperties);

        /// <summary>
        /// Asynchronously initializes the new entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="viewModel">The create model.</param>
        /// <param name="allowedProperties">The allowed properties.</param>
        /// <returns>A task that represents the operation.</returns>
        Task InitializeNewEntityAsync(TEntity entity, TViewModel viewModel, String[] allowedProperties);

        /// <summary>
        /// Asynchronously updates the existing entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="viewModel">The edit model.</param>
        /// <param name="allowedProperties">The allowed properties.</param>
        /// <returns>A task that represents the operation.</returns>
        Task UpdateExistingEntityAsync(TEntity entity, TViewModel viewModel, String[] allowedProperties);
    }
}
