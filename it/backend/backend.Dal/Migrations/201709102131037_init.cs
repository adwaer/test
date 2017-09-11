namespace backend.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fio = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Division = c.String(),
                        Position = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AddressBooks");
        }
    }
}
