using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DevGuild.AspNetCore.Services.Permissions.RoleRelation
{
    /// <summary>
    /// Represents relation between the entity and the user.
    /// </summary>
    public abstract class UserRelation
    {
        /// <summary>
        /// Combines the provided expressions into one via conditional OR operation.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expressions">The expressions.</param>
        /// <returns>The combined expression.</returns>
        public static Expression<Func<TEntity, Boolean>> CombineExpressions<TEntity>(IEnumerable<Expression<Func<TEntity, Boolean>>> expressions)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression combined = null;
            foreach (var expression in expressions)
            {
                var expressionBody = expression.Body;
                var expressionParameter = expression.Parameters.Single();

                var reconstructedBody = UserRelation.ReconstructWithParameter(expressionBody, expressionParameter, parameter);
                combined = combined != null
                    ? Expression.OrElse(combined, reconstructedBody)
                    : reconstructedBody;
            }

            return combined != null ? Expression.Lambda<Func<TEntity, Boolean>>(combined, parameter) : null;
        }

        /// <summary>
        /// Reconstructs the provided expression with different parameter.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="oldParameter">The old parameter.</param>
        /// <param name="newParameter">The new parameter.</param>
        /// <returns>The reconstructed expression.</returns>
        /// <exception cref="InvalidOperationException">Not supported expression tree node type.</exception>
        protected static Expression ReconstructWithParameter(Expression expression, Expression oldParameter, Expression newParameter)
        {
            switch (expression)
            {
                case UnaryExpression unary:
                    return Expression.MakeUnary(
                        unary.NodeType,
                        UserRelation.ReconstructWithParameter(unary.Operand, oldParameter, newParameter),
                        unary.Type,
                        unary.Method);
                case BinaryExpression binary:
                    return Expression.MakeBinary(
                        binary.NodeType,
                        UserRelation.ReconstructWithParameter(binary.Left, oldParameter, newParameter),
                        UserRelation.ReconstructWithParameter(binary.Right, oldParameter, newParameter),
                        binary.IsLiftedToNull,
                        binary.Method);
                case MemberExpression member:
                    return Expression.MakeMemberAccess(
                        UserRelation.ReconstructWithParameter(member.Expression, oldParameter, newParameter),
                        member.Member);
                case MethodCallExpression call when call.Object != null:
                    return Expression.Call(
                        UserRelation.ReconstructWithParameter(call.Object, oldParameter, newParameter),
                        call.Method,
                        call.Arguments.Select(x => UserRelation.ReconstructWithParameter(x, oldParameter, newParameter)));
                case MethodCallExpression call when call.Object == null:
                    return Expression.Call(
                        call.Method,
                        call.Arguments.Select(x => UserRelation.ReconstructWithParameter(x, oldParameter, newParameter)));
                case LambdaExpression lambda:
                    return Expression.Lambda(UserRelation.ReconstructWithParameter(lambda.Body, oldParameter, newParameter), lambda.Parameters);
                case ConstantExpression constant:
                    return constant;
                case ParameterExpression p when p == oldParameter:
                    return newParameter;
                case ParameterExpression p when p != oldParameter:
                    return p;
                default:
                    throw new InvalidOperationException($"Expression of type {expression.GetType()} is not yet supported");
            }
        }
    }
}
