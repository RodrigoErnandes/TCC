namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultaLivroLivro : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Multa", name: "EmprestimoId", newName: "LivroId");
            RenameIndex(table: "dbo.Multa", name: "IX_EmprestimoId", newName: "IX_LivroId");
            AddColumn("dbo.Multa", "Emprestimo_Id", c => c.Int());
            CreateIndex("dbo.Multa", "Emprestimo_Id");
            AddForeignKey("dbo.Multa", "Emprestimo_Id", "dbo.Emprestimos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Multa", "Emprestimo_Id", "dbo.Emprestimos");
            DropIndex("dbo.Multa", new[] { "Emprestimo_Id" });
            DropColumn("dbo.Multa", "Emprestimo_Id");
            RenameIndex(table: "dbo.Multa", name: "IX_LivroId", newName: "IX_EmprestimoId");
            RenameColumn(table: "dbo.Multa", name: "LivroId", newName: "EmprestimoId");
        }
    }
}
