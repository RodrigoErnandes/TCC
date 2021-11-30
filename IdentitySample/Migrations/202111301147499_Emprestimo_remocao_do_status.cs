namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emprestimo_remocao_do_status : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emprestimos", "SituacaoId", "dbo.Situacao");
            DropIndex("dbo.Emprestimos", new[] { "SituacaoId" });
            AlterColumn("dbo.Emprestimos", "SituacaoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Emprestimos", "SituacaoId");
            AddForeignKey("dbo.Emprestimos", "SituacaoId", "dbo.Situacao", "Id", cascadeDelete: true);
            DropColumn("dbo.Emprestimos", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emprestimos", "Status", c => c.String(nullable: false));
            DropForeignKey("dbo.Emprestimos", "SituacaoId", "dbo.Situacao");
            DropIndex("dbo.Emprestimos", new[] { "SituacaoId" });
            AlterColumn("dbo.Emprestimos", "SituacaoId", c => c.Int());
            CreateIndex("dbo.Emprestimos", "SituacaoId");
            AddForeignKey("dbo.Emprestimos", "SituacaoId", "dbo.Situacao", "Id");
        }
    }
}
