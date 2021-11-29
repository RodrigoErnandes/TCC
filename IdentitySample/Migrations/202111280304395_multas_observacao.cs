namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multas_observacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Multa", "Observacao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Multa", "Observacao");
        }
    }
}
