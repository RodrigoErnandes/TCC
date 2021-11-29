namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaLivro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "LivroId", "dbo.Livros");
            DropIndex("dbo.Reservas", new[] { "LivroId" });
            CreateTable(
                "dbo.ReservaLivroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LivroId = c.Int(nullable: false),
                        ReservaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Livros", t => t.LivroId, cascadeDelete: true)
                .ForeignKey("dbo.Reservas", t => t.ReservaId, cascadeDelete: true)
                .Index(t => t.LivroId)
                .Index(t => t.ReservaId);
            
            AddColumn("dbo.Reservas", "UsuarioId", c => c.Guid(nullable: false));
            DropColumn("dbo.Reservas", "LivroId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "LivroId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ReservaLivroes", "ReservaId", "dbo.Reservas");
            DropForeignKey("dbo.ReservaLivroes", "LivroId", "dbo.Livros");
            DropIndex("dbo.ReservaLivroes", new[] { "ReservaId" });
            DropIndex("dbo.ReservaLivroes", new[] { "LivroId" });
            DropColumn("dbo.Reservas", "UsuarioId");
            DropTable("dbo.ReservaLivroes");
            CreateIndex("dbo.Reservas", "LivroId");
            AddForeignKey("dbo.Reservas", "LivroId", "dbo.Livros", "Id", cascadeDelete: true);
        }
    }
}
