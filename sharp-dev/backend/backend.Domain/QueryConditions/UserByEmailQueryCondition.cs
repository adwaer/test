using In.Cqrs.Query.Criterion.Abstract;

namespace backend.Domain.QueryConditions
{
    public class UserByEmailQueryCondition : ICriterion
    {
        public string Email { get; set; }
    }
}
