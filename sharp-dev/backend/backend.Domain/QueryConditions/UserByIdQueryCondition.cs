using In.Cqrs.Query.Criterion.Abstract;

namespace backend.Domain.QueryConditions
{
    public class UserByIdQueryCondition : ICriterion
    {
        public string Id { get; set; }
    }
}
