using System.Data.Entity;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using In.Cqrs.Query;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class UserByEmailQuery : IQuery<UserByEmailQueryCondition, ApplicationUser>
    {
        private readonly IDataSetUow _context;

        public UserByEmailQuery(IDataSetUow context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> Ask(UserByEmailQueryCondition criterion)
        {
            var user = await _context.Query<ApplicationUser>()
                .FirstOrDefaultAsync(x => x.Email == criterion.Email);

            return user;
        }
    }
}
