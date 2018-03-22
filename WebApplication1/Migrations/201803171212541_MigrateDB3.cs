namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Noskis",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Brand_Id = c.Int(),
                        Podoshva = c.String(),
                        PokritieVneshnee = c.String(),
                        PokritieVnutrenee = c.String(),
                        Mangeti = c.Boolean(nullable: false),
                        Tolshina = c.Decimal(nullable: false, precision: 18, scale: 1),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Id)
                .Index(t => t.Brand_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Noskis", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Noskis", "Id", "dbo.Products");
            DropIndex("dbo.Noskis", new[] { "Brand_Id" });
            DropIndex("dbo.Noskis", new[] { "Id" });
            DropTable("dbo.Noskis");
        }
    }
}
