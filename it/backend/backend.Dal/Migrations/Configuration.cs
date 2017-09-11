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
                    Division = "��������� 1",
                    Email = "asd@gg.ru",
                    Fio = "������� ������",
                    Phone = "+7 987 123 45 67",
                    Position = "�������� �������� �����"
                });
                context.AddressBooks.Add(new AddressBook
                {
                    Division = "��������� 2",
                    Email = "masd@gg.ru",
                    Fio = "����� ���������",
                    Phone = "+7 987 123 45 67",
                    Position = "�������� �������� �����"
                });
                context.AddressBooks.Add(new AddressBook
                {
                    Division = "��������� 2",
                    Email = "asd@gg.ru",
                    Fio = "���� ������",
                    Phone = "+7 987 123 45 67",
                    Position = "�������� �������� �����"
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
