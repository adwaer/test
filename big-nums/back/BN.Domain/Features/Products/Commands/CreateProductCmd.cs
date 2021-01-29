using In.Cqrs.Command;
using In.FunctionalCSharp;

namespace BN.Domain.Features.Products.Commands
{
    public class CreateProductCmd : IMessage
    {
        public string Name { get; }
        public decimal Price { get; }

        private CreateProductCmd(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public static Result<CreateProductCmd> Create(string name, decimal price)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.NotNullOrWhiteSpace(name, nameof(name)),
                    ParametersValidation.NotDefaultValue(price, nameof(price))
                )
                .Combine()
                .Map(() => new CreateProductCmd(name, price));
        }
    }
}