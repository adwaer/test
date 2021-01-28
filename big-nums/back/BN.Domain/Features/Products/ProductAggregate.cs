using System;
using BN.Domain.Features.Products.Models;
using In.DDD.Implementations;
using In.FunctionalCSharp;

namespace BN.Domain.Features.Products
{
    public class ProductAggregate : SimpleIAggregateRoot<Product>
    {
        public Result<ProductAggregate> SetName(string name)
        {
            Model.Name = name;
            return Result.Success(this);
        }

        public Result<ProductAggregate> SetPrice(decimal price)
        {
            Model.Price = price;
            return Result.Success(this);
        }

        public Result<ProductAggregate> SetCreated()
        {
            Model.Created = DateTime.UtcNow;
            return Result.Success(this);
        }

        public Result<ProductAggregate> SetModified()
        {
            Model.LastModified = DateTime.UtcNow;
            return Result.Success(this);
        }

        public Result<ProductAggregate> SetDeleted()
        {
            Model.Deleted = DateTime.UtcNow;
            return Result.Success(this);
        }
    }
}