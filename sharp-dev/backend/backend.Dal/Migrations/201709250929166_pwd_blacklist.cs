namespace backend.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pwd_blacklist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FrequentlyPwds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FrequentlyPwds");
        }
    }
}
