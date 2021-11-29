namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class observacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservaLivros", "Observacao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservaLivros", "Observacao");
        }
    }
}
