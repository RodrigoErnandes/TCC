namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ativo_em_acervo_e_livros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Acervos", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Livros", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Livros", "Ativo");
            DropColumn("dbo.Acervos", "Ativo");
        }
    }
}
