using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Entities;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class WorkingDayQuery : IQuery<GenericCriterion<string>, WorkingDay>
    {
        private readonly IDataSetUow _context;

        public WorkingDayQuery(IDataSetUow context)
        {
            _context = context;
        }

        public async Task<WorkingDay> Ask(GenericCriterion<string> criterion)
        {
            return await _context.Query<WorkingDay>()
                .Where(d => d.UserId == criterion.Value)
                .OrderByDescending(d => d.Date)
                .FirstOrDefaultAsync();
        }
    }
}
