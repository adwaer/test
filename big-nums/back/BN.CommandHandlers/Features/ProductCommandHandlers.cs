using System.Threading.Tasks;
using BN.Domain.Features.Products;
using BN.Domain.Features.Products.Commands;
using BN.Domain.Features.Products.Models;
using In.Cqrs.Command;
using In.DDD;
using In.FunctionalCSharp;

namespace BN.CommandHandlers.Features
{
    public class ProductCommandHandlers : ICommandHandler<CreateProductCmd, int>,
        ICommandHandler<EditProductCmd>, ICommandHandler<DeleteProductCmd>
    {
        private readonly IDomainUow<ProductAggregate, Product> _domainUow;
        private readonly IDomainRepository<ProductAggregate, Product> _repository;

        public ProductCommandHandlers(IDomainUow<ProductAggregate, Product> domainUow)
        {
            _domainUow = domainUow;
            _repository = domainUow.Repository;
        }

        public async Task<Result<int>> Handle(CreateProductCmd message)
        {
            return await _repository.Create()
                .SetName(message.Name)
                .Bind(aggr => aggr.SetPrice(message.Price))
                .Bind(aggr => aggr.SetCreated())
                .Bind(aggr => aggr.SetModified())
                .Bind(async aggr =>
                {
                    await _domainUow.Commit();
                    return Result.Success(aggr.Model.Id);
                });
        }

        public async Task<Result> Handle(EditProductCmd message)
        {
            var spec = ProductSpecifications.WithId(message.Id)
                       & ProductSpecifications.NotDeleted();

            var aggregate = await _repository.FindOne(spec);
            if (aggregate == null)
            {
                return Result.Failure("The product were not found");
            }

            return await aggregate
                .SetName(message.Name)
                .Bind(aggr => aggr.SetPrice(message.Price))
                .Bind(aggr => aggr.SetModified())
                .Bind(async aggr =>
                {
                    _repository.Update(aggr);
                    await _domainUow.Commit();
                    return Result.Success();
                });
        }

        public async Task<Result> Handle(DeleteProductCmd message)
        {
            var spec = ProductSpecifications.WithId(message.Id)
                       & ProductSpecifications.NotDeleted();

            var aggregate = await _repository.FindOne(spec);
            if (aggregate == null)
            {
                return Result.Failure("The product were not found");
            }

            return await aggregate
                .SetDeleted()
                .Bind(async aggr =>
                {
                    _repository.Update(aggr);
                    await _domainUow.Commit();
                    return Result.Success();
                });
        }
    }
}