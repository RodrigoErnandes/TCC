namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genero_acervo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Acervos", "Genero", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Acervos", "Genero");
        }
    }
}
