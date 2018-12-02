using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents collection-based relation between the entity and the user that will form expression like the following: <c>x => x.Users.Any(y => y.UserId == userId)</c>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TCollectionItem">The type of the collection item.</typeparam>
    /// <typeparam name="TUserId">The type of the user identifier.</typeparam>
    /// <seealso cref="DevGuild.AspNet.Services.Permissions.RoleRelation.UserRelation{TEntity, TUserId}" />
    public class CollectionUserRelation<TEntity, TCollectionItem, TUserId> : UserRelation<TEntity, TUserId>
    {
        private readonly Expression<Func<TEntity, IEnumerable<TCollectionItem>>> collectionExpression;
        private readonly Expression<Func<TCollectionItem, TUserId>> userIdExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionUserRelation{TEntity, TCollectionItem, TUserId}"/> class.
        /// </summary>
        /// <param name="collectionExpression">The expression that points to the collection.</param>
        /// <param name="userIdExpression">The expression that points to the user identifier within the collection.</param>
        public CollectionUserRelation(Expression<Func<TEntity, IEnumerable<TCollectionItem>>> collectionExpression, Expression<Func<TCollectionItem, TUserId>> userIdExpression)
        {
            this.collectionExpression = collectionExpression;
            this.userIdExpression = userIdExpression;
        }

        /// <summary>
        /// Builds the expression that will test user access to the entity and can be included into repository query.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The expression that tests user access.
        /// </returns>
        public override Expression<Func<TEntity, Boolean>> BuildExpression(TUserId userId)
        {
            var method = this.GetMethod(x => x.Any(y => true));
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var originalBody = this.collectionExpression.Body;
            var originalParameter = this.collectionExpression.Parameters.Single();

            var reconstructedBody = UserRelation.ReconstructWithParameter(originalBody, originalParameter, parameter);
            var any = Expression.Call(method, reconstructedBody, this.BuildChildRelation(userId));

            return Expression.Lambda<Func<TEntity, Boolean>>(any, parameter);
        }

        private Expression<Func<TCollectionItem, Boolean>> BuildChildRelation(TUserId userId)
        {
            var parameter = Expression.Parameter(typeof(TCollectionItem), "y");
            var originalBody = this.userIdExpression.Body;
            var originalParameter = this.userIdExpression.Parameters.Single();

            var reconstructedBody = UserRelation.ReconstructWithParameter(originalBody, originalParameter, parameter);
            var equal = Expression.Equal(reconstructedBody, Expression.Constant(userId));
            return Expression.Lambda<Func<TCollectionItem, Boolean>>(equal, parameter);
        }

        private MethodInfo GetMethod(Expression<Func<IEnumerable<TCollectionItem>, Boolean>> sampleExpression)
        {
            var body = sampleExpression.Body;
            if (body is MethodCallExpression methodCall)
            {
                return methodCall.Method;
            }

            throw new InvalidOperationException();
        }
    }
}
