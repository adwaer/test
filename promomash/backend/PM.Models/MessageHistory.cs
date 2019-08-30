using System;
using SilentNotary.Common;
using SilentNotary.Cqrs.Domain;

namespace PM.Models
{
    public class MessageHistory : DomainEntityBase<long>, IMessageResult
    {
        public string Body { get; set; }
        public string Info { get; set; }
        public bool Socceed { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Identity { get; set; }
    }
}
