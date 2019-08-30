using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PM.Models;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;

namespace PM.Infrastructure.Dal
{
    public class MessageHistoryStorage : IStorage<IMessageResult>
    {
        private readonly DbContext _db;

        public MessageHistoryStorage(DbContext db)
        {
            _db = db;
        }

        public IQueryable<IMessageResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<IMessageResult> Get(IExpressionCriterion<IMessageResult> condition)
        {
            throw new NotImplementedException();
        }

        public void Add(IMessageResult data)
        {
            var messageHistory = (MessageHistory) data;
            messageHistory.CreateDate = DateTime.UtcNow;
            
            _db.Set<MessageHistory>()
                .Add(messageHistory);
        }

        public void Remove(IMessageResult data)
        {
            throw new System.NotImplementedException();
        }

        public Task Save(params IMessageResult[] messages)
        {
            return _db.SaveChangesAsync();
        }
    }
}