using System;
using System.Runtime.Serialization;
using backend.Domain.Enums;
using In.Cqrs.Query.Criterion.Abstract;

namespace backend.Domain.QueryConditions
{
    [DataContract]
    public class MovementsQueryCondition : ICriterion
    {
        [DataMember]
        public DateTime? Date { get; set; }
        [DataMember]
        public string Correspond { get; set; }
        [DataMember]
        public double? AmountFrom { get; set; }
        [DataMember]
        public double? AmountTo { get; set; }
        [DataMember]
        public MovementSortBy? SortBy { get; set; }

        public string UserId { get; set; }
    }
}
