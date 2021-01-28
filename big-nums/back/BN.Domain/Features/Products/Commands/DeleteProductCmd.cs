using System;
using In.Cqrs.Command;
using In.FunctionalCSharp;

namespace BN.Domain.Features.Products.Commands
{
    public class DeleteProductCmd : IMessage
    {
        public int Id { get; }

        private DeleteProductCmd(int id)
        {
            Id = id;
        }

        public static Result<DeleteProductCmd> Create(int id)
        {
            return ParametersValidation.NotDefaultValue(id, nameof(id))
                .Map(() => new DeleteProductCmd(id));
        }
    }
}