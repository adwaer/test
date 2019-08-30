using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PM.Models;

namespace PM.Identity.Dal
{
    public class IdentityDbCtx : IdentityDbContext<Customer>
    {
        public DbSet<MessageHistory> MessageHistories { get; set; }
        public DbSet<Country> Countries { get; set; }

        public IdentityDbCtx(DbContextOptions<IdentityDbCtx> options)
            : base(options)
        {
        }

        public static void Initialize(IdentityDbCtx ctx)
        {
            Console.WriteLine("Init");
            if (ctx.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                ctx.Database.Migrate();
            }

            if (ctx.Countries.Any())
            {
                return; // Data was already seeded
            }

            ctx.Countries.AddRange(new Country
                {
                    Name = "Russia",
                    Provinces = new List<Province>()
                    {
                        new Province
                        {
                            Name = "Tatarstan"
                        },
                        new Province
                        {
                            Name = "Moscow"
                        }
                    }
                },
                new Country
                {
                    Name = "USA",
                    Provinces = new List<Province>()
                    {
                        new Province
                        {
                            Name = "New York"
                        }
                    }
                });

            ctx.SaveChanges();
        }
    }
}