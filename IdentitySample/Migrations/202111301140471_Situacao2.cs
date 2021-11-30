namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Situacao2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprestimos", "SituacaoId", c => c.Int());
            CreateIndex("dbo.Emprestimos", "SituacaoId");
            AddForeignKey("dbo.Emprestimos", "SituacaoId", "dbo.Situacao", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emprestimos", "SituacaoId", "dbo.Situacao");
            DropIndex("dbo.Emprestimos", new[] { "SituacaoId" });
            DropColumn("dbo.Emprestimos", "SituacaoId");
        }
    }
}
