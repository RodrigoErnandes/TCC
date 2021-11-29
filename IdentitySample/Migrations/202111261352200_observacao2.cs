namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class observacao2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "Observacao", c => c.String());
            DropColumn("dbo.ReservaLivros", "Observacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservaLivros", "Observacao", c => c.String());
            DropColumn("dbo.Reservas", "Observacao");
        }
    }
}
