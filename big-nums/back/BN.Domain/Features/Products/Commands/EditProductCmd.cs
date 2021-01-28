using System;
using In.Cqrs.Command;
using In.FunctionalCSharp;

namespace BN.Domain.Features.Products.Commands
{
    public class EditProductCmd : IMessage
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        private EditProductCmd(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public static Result<EditProductCmd> Create(int id, string name, decimal price)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.NotDefaultValue(id, nameof(id)),
                    ParametersValidation.NotNullOrWhiteSpace(name, nameof(name)),
                    ParametersValidation.NotDefaultValue(price, nameof(price))
                )
                .Combine()
                .Map(() => new EditProductCmd(id, name, price));
        }
    }
}