using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using SilentNotary.Common;

namespace PM.Configuration
{
    public interface ICommandHandler<TCommand> where TCommand : IMessage
    {
        Task<Result> Handle(TCommand message);
    }
    
    public interface ICommandHandler<TCommand, TResult> where TCommand : IMessage
    {
        Task<Result<TResult>> Handle(TCommand message);
    }
}