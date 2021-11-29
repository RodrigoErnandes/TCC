namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leitor_na_reserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "Leitor", c => c.String());
            DropColumn("dbo.Reservas", "UsuarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "UsuarioId", c => c.Guid(nullable: false));
            DropColumn("dbo.Reservas", "Leitor");
        }
    }
}
