namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projeto02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LivroId = c.Int(nullable: false),
                        DataReserva = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Livros", t => t.LivroId, cascadeDelete: true)
                .Index(t => t.LivroId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "LivroId", "dbo.Livros");
            DropIndex("dbo.Reservas", new[] { "LivroId" });
            DropTable("dbo.Reservas");
        }
    }
}
