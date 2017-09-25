using In.Cqrs.Query.Criterion.Abstract;

namespace backend.Domain.QueryConditions
{
    public class UsersSearchQueryCondition : ICriterion
    {
        public string Pattern { get; set; }
        public string ExcludeEmail { get; set; }
    }
}
