using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PM.Configuration;
using SilentNotary.Common;

namespace PM.CommandHandlers
{
    public class MsgHandlerErrorDecorator<TMsg> : IMsgHandler<TMsg> where TMsg : IMessage
    {
        private readonly ICommandHandler<TMsg> _handler;

        public MsgHandlerErrorDecorator(ICommandHandler<TMsg> handler)
        {
            _handler = handler;
        }

        public async Task<Result> Handle(TMsg message)
        {
            try
            {
                return await _handler.Handle(message);
            }
            catch (Exception ex)
            {
                //todo: log here
                //_logService.LogError(ex);
                return Result.Fail(ex.Message);
            }
        }
    }
    
    public class MsgHandlerErrorDecorator<TMsg, TOutput> : IMsgHandler<TMsg, TOutput> where TMsg : IMessage
    {
        private readonly ICommandHandler<TMsg, TOutput> _handler;

        public MsgHandlerErrorDecorator(ICommandHandler<TMsg, TOutput> handler)
        {
            _handler = handler;
        }

        public async Task<Result<TOutput>> Handle(TMsg message)
        {
            try
            {
                return await _handler.Handle(message);
            }
            catch (Exception ex)
            {
                //todo: log here
                //_logService.LogError(ex);
                return Result.Fail<TOutput>(ex.Message);
            }
        }
    }
}