namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultaLeitor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Multa", "Leitor", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Multa", "Leitor");
        }
    }
}
