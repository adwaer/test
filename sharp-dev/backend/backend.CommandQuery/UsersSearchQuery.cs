using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using backend.Domain.QueryResults;
using In.Cqrs.Query;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class UsersSearchQuery : IQuery<UsersSearchQueryCondition, UsersSearchQueryResult>
    {
        private readonly IDataSetUow _context;

        public UsersSearchQuery(IDataSetUow context)
        {
            _context = context;
        }

        public async Task<UsersSearchQueryResult> Ask(UsersSearchQueryCondition criterion)
        {
            var indexOf = criterion.Pattern.IndexOf(" (", StringComparison.Ordinal);
            if (indexOf > 0)
            {
                criterion.Pattern = criterion.Pattern.Substring(0, indexOf);
            }

            var usersQuery = _context.Query<ApplicationUser>()
                .Where(x => x.Email != criterion.ExcludeEmail);
            if (!string.IsNullOrEmpty(criterion.Pattern))
            {
                usersQuery = usersQuery.Where(x => x.UserName.StartsWith(criterion.Pattern));
            }

            var users = await usersQuery
                .Select(u => u.UserName + " (" + u.Email + ")")
                .Take(10)
                .ToArrayAsync();

            return new UsersSearchQueryResult
            {
                Data = users
            };
        }
    }
}
