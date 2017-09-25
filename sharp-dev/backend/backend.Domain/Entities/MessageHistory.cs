using In.Cqrs;

namespace backend.Domain.Entities
{
    /// <summary>
    /// Commands history
    /// </summary>
    public class MessageHistory : TimeTrackableEntity<long>, IMessageResult
    {
        public string Body { get; set; }
        public string Info { get; set; }
        public bool Socceed { get; set; }
        public string Type { get; set; }
    }
}
