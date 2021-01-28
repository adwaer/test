using System;
using System.Linq;
using BN.Domain.Features.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BN.Dal
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<DbCtx>();
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products
                    .AddRange(
                        new Product
                        {
                            Created = DateTime.UtcNow,
                            LastModified = DateTime.UtcNow,
                            Name = "Bread",
                            Price = 35.5m
                        },
                        new Product
                        {
                            Created = DateTime.UtcNow,
                            LastModified = DateTime.UtcNow,
                            Name = "Butter",
                            Price = 135.75m
                        },
                        new Product
                        {
                            Created = DateTime.UtcNow,
                            LastModified = DateTime.UtcNow,
                            Name = "Milk",
                            Price = 50
                        }
                    );
            }

            context.SaveChanges();
        }
    }
}