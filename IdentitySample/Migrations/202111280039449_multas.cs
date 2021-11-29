namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Multa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiasAtraso = c.Int(nullable: false),
                        StatusPagamento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmprestimoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Livros", t => t.EmprestimoId, cascadeDelete: true)
                .Index(t => t.EmprestimoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Multa", "EmprestimoId", "dbo.Livros");
            DropIndex("dbo.Multa", new[] { "EmprestimoId" });
            DropTable("dbo.Multa");
        }
    }
}
