using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PM.Infrastructure.Dal;
using SilentNotary.Common;
using SilentNotary.Cqrs.Domain;
using SilentNotary.Cqrs.Domain.Interfaces;

namespace PM.Identity.Dal
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class DddUnitOfWork : IDddUnitOfWork, IAggregateTracker, IDisposable
    {
        private readonly DbContext _db;
        private readonly IServiceProvider _diContext;
        private readonly List<IAggregateRoot> _trackedAggregates = new List<IAggregateRoot>();
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IMessageSender _messageSender;

        public DddUnitOfWork(DbContext db, IServiceProvider diContext, IEventDispatcher eventDispatcher,
            IMessageSender messageSender)
        {
            _db = db;
            _diContext = diContext;
            _eventDispatcher = eventDispatcher;
            _messageSender = messageSender;
            ValueObjectProvider =
                _diContext.GetRequiredService<IValueObjectProvider>();
        }

        public AggregateRepository<TAggregate, TId> Repository<TAggregate, TId>()
            where TAggregate : Aggregate<TId>
        {
            var repository = _diContext.GetRequiredService<AggregateRepository<TAggregate, TId>>();
            return new AggregateRepositoryTrackDecorator<TAggregate, TId>(repository, this);
        }

        public async Task<Result> CommitAsync()
        {
            var result = await DispatchDomainEvents()
                .OnSuccess(() => _db.SaveChangesAsync());

            if (result.IsFailure)
                return result;
            await PublishIntegrationEvents();
            return Result.Ok();
        }

        public IValueObjectProvider ValueObjectProvider { get; }

        public void Track(IAggregateRoot aggregate)
        {
            _trackedAggregates.Add(aggregate);
        }

        private async Task<Result> DispatchDomainEvents()
        {
            var aggregatesWithDomainEvents = _trackedAggregates
                .Where(aggregate => aggregate.DomainEvents.Any())
                .ToArray();

            foreach (var aggregate in aggregatesWithDomainEvents)
            {
                var events = aggregate.DomainEvents.ToArray();

                foreach (var @event in events)
                {
                    var result = await _eventDispatcher.Dispatch(@event);
                    if (result.IsFailure)
                        return result;
                }

                aggregate.OnEventsCommited();
            }

            if (_trackedAggregates.Any(aggregate => aggregate.DomainEvents.Any()))
                return await DispatchDomainEvents();

            return Result.Ok();
        }

        private async Task PublishIntegrationEvents()
        {
            var aggregatesWithCommitedEvents = _trackedAggregates
                .Where(aggregate => aggregate.CommitedDomainEvents.Any())
                .ToArray();

            foreach (var aggregate in aggregatesWithCommitedEvents)
            {
                var events = aggregate.CommitedDomainEvents;
                aggregate.ClearCommitedEvents();
                foreach (var @event in events)
                {
                    if (!(@event is IIntegrationEvent)) continue;

                    var publishMethod = _messageSender
                        .GetType()
                        .GetRuntimeMethods()
                        .First(x => x.Name == nameof(IMessageSender.PublishAsync));
                    await (Task)publishMethod
                        .MakeGenericMethod(@event.GetType())
                        .Invoke(_messageSender, new object[] { @event });
                }
            }
        }

        #region IDisposable implementation

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}