using System.Linq;
using backend.Domain;

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
            if (!context.AddressBooks.Any())
            {
                context.AddressBooks.Add(new AddressBook
                {
                    Division = "отделение 1",
                    Email = "asd@gg.ru",
                    Fio = "Василий Пупкин",
                    Phone = "+7 987 123 45 67",
                    Position = "Менеджер среднего звена"
                });
                context.AddressBooks.Add(new AddressBook
                {
                    Division = "отделение 2",
                    Email = "masd@gg.ru",
                    Fio = "Мария Пулетчица",
                    Phone = "+7 987 123 45 67",
                    Position = "Менеджер среднего звена"
                });
                context.AddressBooks.Add(new AddressBook
                {
                    Division = "отделение 2",
                    Email = "asd@gg.ru",
                    Fio = "Лена Полена",
                    Phone = "+7 987 123 45 67",
                    Position = "Менеджер среднего звена"
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
