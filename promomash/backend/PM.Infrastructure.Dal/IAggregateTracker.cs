using SilentNotary.Cqrs.Domain.Interfaces;

namespace PM.Infrastructure.Dal
{
    public interface IAggregateTracker
    {
        void Track(IAggregateRoot aggregate);
    }
}
