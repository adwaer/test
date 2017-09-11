using System.Data.Entity;
using System.Threading.Tasks;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;

namespace backend.Domain.Queries
{
    // ReSharper disable once UnusedMember.Global
    public class AddressBooksQuery : IQuery<EmptyCriterion, AddressBook[]>
    {
        private readonly DbContext _dbContext;

        public AddressBooksQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddressBook[]> Ask(EmptyCriterion criterion)
        {
            var books = await _dbContext
                .Set<AddressBook>()
                .ToArrayAsync();

            return books;
        }
    }
}
