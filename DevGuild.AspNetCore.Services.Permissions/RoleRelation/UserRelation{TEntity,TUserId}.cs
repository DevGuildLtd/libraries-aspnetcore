using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents relation between the entity and the user.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TUserId">The type of the user identifier.</typeparam>
    /// <seealso cref="UserRelation" />
    public abstract class UserRelation<TEntity, TUserId> : UserRelation
    {
        /// <summary>
        /// Builds the expression that will test user access to the entity and can be included into repository query.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The expression that tests user access.</returns>
        public abstract Expression<Func<TEntity, Boolean>> BuildExpression(TUserId userId);

        /// <summary>
        /// Tests the user access to the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual Boolean TestUser(TEntity entity, TUserId userId)
        {
            var expression = this.BuildExpression(userId);
            var compiled = expression.Compile();
            return compiled(entity);
        }
    }
}
