using System.Data.Entity;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using In.Cqrs.Query;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class UserByIdQuery : IQuery<UserByIdQueryCondition, ApplicationUser>
    {
        private readonly IDataSetUow _context;

        public UserByIdQuery(IDataSetUow context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Ask(UserByIdQueryCondition criterion)
        {
            var user = await _context.Query<ApplicationUser>()
                .FirstOrDefaultAsync(x => x.Id == criterion.Id);

            return user;
        }
    }
}
