using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.Enums;
using backend.Domain.QueryConditions;
using backend.Domain.QueryResults;
using In.Cqrs.Query;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class MovementsQuery : IQuery<MovementsQueryCondition, IMultipleQueryResult<MovementsQueryResult>>
    {
        private readonly IDataSetUow _context;

        public MovementsQuery(IDataSetUow context)
        {
            _context = context;
        }

        public async Task<IMultipleQueryResult<MovementsQueryResult>> Ask(MovementsQueryCondition criterion)
        {
            var queryable = _context.Query<Movement>()
                .Include(x => x.Correspondent)
                .Where(x => x.UserId == criterion.UserId);

            if (criterion.Date.HasValue)
            {
                var d = criterion.Date.Value.Date;
                queryable = queryable.Where(x => x.WorkingDay.Date == d);
            }
            if (!string.IsNullOrEmpty(criterion.Correspond))
            {
                var indexOf = criterion.Correspond.IndexOf(" (", StringComparison.Ordinal);
                if (indexOf > 0)
                {
                    criterion.Correspond = criterion.Correspond.Substring(0, indexOf);
                }

                queryable = queryable.Where(x => x.Correspondent.UserName == criterion.Correspond);
            }

            if (criterion.AmountFrom.HasValue)
            {
                queryable = queryable.Where(x => x.Amount >= criterion.AmountFrom.Value);
            }
            if (criterion.AmountTo.HasValue)
            {
                queryable = queryable.Where(x => x.Amount <= criterion.AmountTo.Value);
            }

            if (criterion.SortBy.HasValue)
            {
                switch (criterion.SortBy.Value)
                {
                    case MovementSortBy.Date:
                        queryable = queryable.OrderByDescending(x => x.CreateDate);
                        break;
                    case MovementSortBy.Name:
                        queryable = queryable.OrderBy(x => x.Correspondent.UserName);
                        break;
                    case MovementSortBy.Amount:
                        queryable = queryable.OrderByDescending(x => x.Amount);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                queryable = queryable.OrderByDescending(x => x.CreateDate);
            }

            var data = await queryable.ToArrayAsync();

            return new MultipleQueryResult<MovementsQueryResult>
            {
                Count = data.Length,
                Data = data.Select(x => new MovementsQueryResult
                {
                    Date = x.CreateDate,
                    Amount = x.Amount,
                    Correspondent = x.Correspondent?.UserName ?? "system welcome",
                    ResultBalance = x.BalanceAfter
                }).ToArray()
            };
        }
    }
}
