using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents reference-based relation between the entity and the user that will form expression like the following: <c>x => x.UserId == userId</c>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TUserId">The type of the user identifier.</typeparam>
    /// <seealso cref="UserRelation{TEntity, TUserId}" />
    public class ReferenceUserRelation<TEntity, TUserId> : UserRelation<TEntity, TUserId>
    {
        private readonly Expression<Func<TEntity, TUserId>> userIdExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceUserRelation{TEntity, TUserId}"/> class.
        /// </summary>
        /// <param name="userIdExpression">The expression that points to user identifier.</param>
        public ReferenceUserRelation(Expression<Func<TEntity, TUserId>> userIdExpression)
        {
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
            var body = this.userIdExpression.Body;
            var equality = Expression.Equal(body, Expression.Constant(userId));
            return Expression.Lambda<Func<TEntity, Boolean>>(equality, this.userIdExpression.Parameters);
        }
    }
}
