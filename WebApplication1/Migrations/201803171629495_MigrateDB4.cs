namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rukavitsy",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Brand_Id = c.Int(),
                        Pokritie = c.String(),
                        Mangeti = c.Boolean(nullable: false),
                        Obturaciya = c.Boolean(nullable: false),
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
            DropForeignKey("dbo.Rukavitsy", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Rukavitsy", "Id", "dbo.Products");
            DropIndex("dbo.Rukavitsy", new[] { "Brand_Id" });
            DropIndex("dbo.Rukavitsy", new[] { "Id" });
            DropTable("dbo.Rukavitsy");
        }
    }
}
