using System.Data.Entity;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.QueryResults;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class UserDataQuery : IQuery<GenericCriterion<string>, UserDataQueryResult>
    {
        private readonly IDataSetUow _context;
        private readonly IQueryBuilder _queryBuilder;

        public UserDataQuery(IDataSetUow context, IQueryBuilder queryBuilder)
        {
            _context = context;
            _queryBuilder = queryBuilder;
        }

        public async Task<UserDataQueryResult> Ask(GenericCriterion<string> criterion)
        {
            var user = await _context.Query<ApplicationUser>()
                .FirstOrDefaultAsync(x => x.Id == criterion.Value);

            var workingDay = await _queryBuilder.For<WorkingDay>()
                .WithAsync(new GenericCriterion<string>(criterion.Value));

            return new UserDataQueryResult
            {
                UserName = user.UserName,
                Balance = workingDay?.Balance ?? 0
            };
        }
    }
}
