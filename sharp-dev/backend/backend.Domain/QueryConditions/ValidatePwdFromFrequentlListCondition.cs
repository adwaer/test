using In.Cqrs.Query.Criterion.Abstract;

namespace backend.Domain.QueryConditions
{
    public class ValidatePwdFromFrequentlListCondition : ICriterion
    {
        public string Value { get; set; }

        public ValidatePwdFromFrequentlListCondition(string value)
        {
            Value = value;
        }
    }
}
