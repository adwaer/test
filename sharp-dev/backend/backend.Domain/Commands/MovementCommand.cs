using In.Cqrs;

namespace backend.Domain.Commands
{
    public class MovementCommand : ICommandData
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public string CorrespondentId { get; set; }
        public string UserId { get; set; }
    }
}
