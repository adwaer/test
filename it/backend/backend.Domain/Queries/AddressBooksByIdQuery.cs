using System.Data.Entity;
using System.Threading.Tasks;
using In.Cqrs.Query;
using In.Cqrs.Query.Criterion;

namespace backend.Domain.Queries
{
    // ReSharper disable once UnusedMember.Global
    public class AddressBooksByIdQuery : IQuery<GenericCriterion<int>, AddressBook>
    {
        private readonly DbContext _dbContext;

        public AddressBooksByIdQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddressBook> Ask(GenericCriterion<int> criterion)
        {
            var book = await _dbContext
                .Set<AddressBook>()
                .SingleAsync(x => x.Id == criterion.Value);

            return book;
        }
    }
}
