using System;
using System.Runtime.Serialization;

namespace backend.Domain.QueryResults
{
    [DataContract]
    public class MovementsQueryResult
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Correspondent { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public double ResultBalance { get; set; }
    }
}
