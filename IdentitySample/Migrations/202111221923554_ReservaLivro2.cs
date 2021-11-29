namespace IdentitySample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservaLivro2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReservaLivroes", newName: "ReservaLivros");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ReservaLivros", newName: "ReservaLivroes");
        }
    }
}
