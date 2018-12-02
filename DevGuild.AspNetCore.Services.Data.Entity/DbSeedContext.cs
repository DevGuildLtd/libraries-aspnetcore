using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using KeyExtensions = Microsoft.EntityFrameworkCore.Metadata.Internal.KeyExtensions;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents database seeding context.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    public class DbSeedContext<TContext>
        where TContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TContext context;

        internal DbSeedContext(IServiceProvider serviceProvider, TContext context)
        {
            this.serviceProvider = serviceProvider;
            this.context = context;
            this.Parameters = new DbSeedContextParameters();
        }

        /// <summary>
        /// Get the parameters of the database seeding context.
        /// </summary>
        /// <value>Parameters of the database seeding context.</value>
        public DbSeedContextParameters Parameters { get; }

        /// <summary>
        /// Get the database set for specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A database set</returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class => this.context.Set<TEntity>();

        /// <summary>
        /// Asynchronously saves the changes.
        /// </summary>
        /// <returns>A task that represents save operation.</returns>
        public Task<Int32> SaveChangesAsync() => this.context.SaveChangesAsync();

        public T GetService<T>()
        {
            return this.serviceProvider.GetService<T>();
        }

        /// <summary>
        /// Gets the key equality expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>An expression.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public Expression<Func<TEntity, Boolean>> GetKeyEqualityExpression<TEntity>(TEntity entity)
            where TEntity : class
        {
            var properties = this.GetEntityKeys<TEntity>();
            if (properties.Length == 0)
            {
                throw new InvalidOperationException($"Failed to identify key properties of {typeof(TEntity).Name}");
            }

            var param = Expression.Parameter(typeof(TEntity), "x");
            var other = Expression.Constant(entity, typeof(TEntity));

            BinaryExpression resultExpression = null;
            foreach (var property in properties)
            {
                var paramProperty = Expression.Property(param, property);
                var otherProperty = Expression.Property(other, property);

                var equalExpression = Expression.Equal(paramProperty, otherProperty);

                resultExpression = resultExpression == null ? equalExpression : Expression.AndAlso(resultExpression, equalExpression);
            }

            return Expression.Lambda<Func<TEntity, Boolean>>(resultExpression, param);
        }

        /// <summary>
        /// Gets the custom equality expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="keyExpression">The key expression.</param>
        /// <returns>An expression.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public Expression<Func<TEntity, Boolean>> GetCustomEqualityExpression<TEntity, TKey>(TEntity entity, Expression<Func<TEntity, TKey>> keyExpression)
            where TEntity : class
        {
            var properties = this.GetEntityKeysFromExpression(keyExpression);
            if (properties.Item2.Length == 0)
            {
                throw new InvalidOperationException($"Failed to identify key properties of {typeof(TEntity).Name}");
            }

            var param = Expression.Parameter(typeof(TEntity), "x");
            var other = Expression.Constant(entity, typeof(TEntity));

            BinaryExpression resultExpression = null;
            foreach (var property in properties.Item2)
            {
                var paramProperty = this.ReconstructWithParameter(property, properties.Item1, param);
                var otherProperty = this.ReconstructWithParameter(property, properties.Item1, other);

                var equalExpression = Expression.Equal(paramProperty, otherProperty);

                resultExpression = resultExpression == null ? equalExpression : Expression.AndAlso(resultExpression, equalExpression);
            }

            return Expression.Lambda<Func<TEntity, Boolean>>(resultExpression, param);
        }

        private PropertyInfo[] GetEntityKeys<TEntity>()
            where TEntity : class
        {
            var entityModel = this.context.Model.FindEntityType(typeof(TEntity));
            var key = entityModel.GetKeys().Single(x => x.IsPrimaryKey());
            return key.Properties.Select(x => x.PropertyInfo).ToArray();
        }

        private Tuple<Expression, Expression[]> GetEntityKeysFromExpression<TEntity, TKey>(Expression<Func<TEntity, TKey>> expression)
        {
            var body = expression.Body;
            if (body is NewExpression newExpresion)
            {
                return Tuple.Create<Expression, Expression[]>(expression.Parameters.Single(), newExpresion.Arguments.ToArray());
            }

            if (body is MemberExpression memberExpression)
            {
                return Tuple.Create<Expression, Expression[]>(expression.Parameters.Single(), new Expression[] { memberExpression });
            }

            throw new InvalidOperationException();
        }

        private Expression ReconstructWithParameter(Expression expression, Expression oldParameter, Expression newParameter)
        {
            switch (expression)
            {
                case BinaryExpression binary:
                    return Expression.MakeBinary(
                        binary.NodeType,
                        this.ReconstructWithParameter(binary.Left, oldParameter, newParameter),
                        this.ReconstructWithParameter(binary.Right, oldParameter, newParameter),
                        binary.IsLiftedToNull,
                        binary.Method);
                case MemberExpression member:
                    return Expression.MakeMemberAccess(
                        this.ReconstructWithParameter(member.Expression, oldParameter, newParameter),
                        member.Member);
                case ParameterExpression p when p == oldParameter:
                    return newParameter;
                default:
                    throw new InvalidOperationException($"Expression of type {expression.GetType()} is not yet supported");
            }
        }
    }
}
