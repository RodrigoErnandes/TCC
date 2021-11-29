namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multasnovo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Multa", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Multa", "MotivoMulta", c => c.String(nullable: false));
            AlterColumn("dbo.Multa", "StatusPagamento", c => c.String(nullable: false));
            DropColumn("dbo.Multa", "DiasAtraso");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Multa", "DiasAtraso", c => c.Int(nullable: false));
            AlterColumn("dbo.Multa", "StatusPagamento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Multa", "MotivoMulta");
            DropColumn("dbo.Multa", "Valor");
        }
    }
}
