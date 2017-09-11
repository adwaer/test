using System.Data.Entity;
using backend.Domain;

namespace backend.Dal
{
    public class Ctx : DbContext
    {
        public Ctx()
            : base("ctx")
        {
        }

        public DbSet<AddressBook> AddressBooks { get; set; }
    }
}
