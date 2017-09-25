using System;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Entities;
using In.Cqrs;
using In.Cqrs.Query.Criterion.Abstract;
using In.Entity.Uow;

namespace backend.CommandQuery
{
    public class MessageHistoryStorage : IStorage<IMessageResult>
    {
        private readonly IDataSetUow _dataSetUow;

        public MessageHistoryStorage(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public IQueryable<IMessageResult> GetAll()
        {
            return _dataSetUow
                .Query<MessageHistory>()
                .AsQueryable();
        }

        public IQueryable<IMessageResult> Get(IExpressionCriterion<IMessageResult> condition)
        {
            throw new NotImplementedException();
        }

        public void Add(IMessageResult data)
        {
            _dataSetUow
                .Add((MessageHistory)data);
        }

        public void Remove(IMessageResult data)
        {
            _dataSetUow
                .Remove((MessageHistory)data);
        }

        public async Task Save(params IMessageResult[] messages)
        {
            await _dataSetUow
                .CommitAsync();
        }
    }
}