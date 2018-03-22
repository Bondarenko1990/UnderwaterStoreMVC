namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tapochkis",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Brand_Id = c.Int(),
                        Podoshva = c.String(),
                        Material = c.String(),
                        Age = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Id)
                .Index(t => t.Brand_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tapochkis", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Tapochkis", "Id", "dbo.Products");
            DropIndex("dbo.Tapochkis", new[] { "Brand_Id" });
            DropIndex("dbo.Tapochkis", new[] { "Id" });
            DropTable("dbo.Tapochkis");
        }
    }
}
