namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Situacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Situacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeSituacao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Situacao");
        }
    }
}
