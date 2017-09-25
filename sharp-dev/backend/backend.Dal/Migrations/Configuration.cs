using System;
using System.Linq;
using backend.Domain.Entities;

namespace backend.Dal.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Ctx>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ctx context)
        {
            if (!context.FrequentlyPwds.Any())
            {
                foreach (var pwd in new[] { "123456", "zxcvbn", "qwerty" })
                {
                    context.FrequentlyPwds.Add(new FrequentlyPwd
                    {
                        Value = pwd
                    });
                }
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new ApplicationUser
                {
                    Email = "asd@ssss.ru",
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Ivan Gus"
                });
                context.Users.Add(new ApplicationUser
                {
                    Email = "asddd@ssss.ru",
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Zero Five"
                });
                context.Users.Add(new ApplicationUser
                {
                    Email = "axxxxx@ssss.ru",
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Ann Fire"
                });
            }


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
