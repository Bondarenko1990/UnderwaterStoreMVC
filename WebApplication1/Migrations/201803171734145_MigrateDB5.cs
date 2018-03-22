namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rukavitsy", newName: "Rukavitsys");
            CreateTable(
                "dbo.Botys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Brand_Id = c.Int(),
                        Podoshva = c.String(),
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
            DropForeignKey("dbo.Botys", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Botys", "Id", "dbo.Products");
            DropIndex("dbo.Botys", new[] { "Brand_Id" });
            DropIndex("dbo.Botys", new[] { "Id" });
            DropTable("dbo.Botys");
            RenameTable(name: "dbo.Rukavitsys", newName: "Rukavitsy");
        }
    }
}
