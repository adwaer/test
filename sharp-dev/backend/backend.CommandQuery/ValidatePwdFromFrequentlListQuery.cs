using System.Data.Entity;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.QueryConditions;
using In.Cqrs.Query;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    // ReSharper disable once UnusedMember.Global -- Query
    public class ValidatePwdFromFrequentlListQuery : IQuery<ValidatePwdFromFrequentlListCondition, bool>
    {
        private readonly IDataSetUow _context;

        public ValidatePwdFromFrequentlListQuery(IDataSetUow context)
        {
            _context = context;
        }

        public async Task<bool> Ask(ValidatePwdFromFrequentlListCondition criterion)
        {
            var isFrequently = await _context.Query<FrequentlyPwd>()
                .AnyAsync(x => x.Value == criterion.Value);

            return isFrequently;
        }
    }
}
