using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domain.Commands;
using backend.Domain.Entities;
using In.Cqrs;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global
    public class MovementCommandHandler : IMsgHandler<MovementCommand>
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IStorage<WorkingDay> _storage;
        private static readonly object Sync = new object();

        public MovementCommandHandler(IQueryBuilder queryBuilder, IStorage<WorkingDay> storage)
        {
            _queryBuilder = queryBuilder;
            _storage = storage;
        }

        public async Task<string> Handle(MovementCommand message)
        {
            var workingDay = await _queryBuilder.For<WorkingDay>()
                .WithAsync(new GenericCriterion<string>(message.UserId));
            
            // need to lock only own customer balance
            lock (Sync)
            {
                if (workingDay == null)
                {
                    workingDay = new WorkingDay
                    {
                        Balance = 0,
                        UserId = message.UserId,
                        Date = DateTime.UtcNow.Date,
                        Movements = new List<Movement>()
                    };
                }

                workingDay.Balance += message.Amount;
                if (workingDay.Balance < 0)
                {
                    throw new Exception("The balance is to low");
                }
                workingDay.Movements.Add(new Movement
                {
                    Amount = message.Amount,
                    MakeUserId = message.MakeUserId,
                    UserId = message.UserId,
                    Description = message.Description
                });

                AsyncHelpers.RunSync(() => _storage.Save(workingDay));
            }

            return workingDay.Id.ToString();
        }
    }
}
