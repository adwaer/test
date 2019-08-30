using System;
using System.Linq.Expressions;
using Serialize.Linq.Serializers;
using SilentNotary.Specifications;

namespace PM.Domain
{
    /// <summary>
    /// Data query specifications swapper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuerySpecification<T> : Specification<T>
    {
        public readonly string ExpressionText;
        private Expression<Func<T, bool>> _predicate;

        // Constructor should (!!!) have internal modifier to restrict new specifications creation 
        // to only this project. This is because of intent of specification pattern to have all 
        // allowed criterions to query data in one place.
        public QuerySpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentException(nameof(predicate));
            var exprSerializer = new ExpressionSerializer (new JsonSerializer());
            ExpressionText = exprSerializer.SerializeText(_predicate);
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate ?? (_predicate =
                       (Expression<Func<T, bool>>) new ExpressionSerializer(new JsonSerializer())
                           .DeserializeText(ExpressionText));
        }
    }
}