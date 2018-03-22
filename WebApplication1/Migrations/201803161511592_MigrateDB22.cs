namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gruza_i_gruzovye_sistemys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Brand_Id = c.Int(),
                        Type = c.String(),
                        Kg = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Id)
                .Index(t => t.Brand_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gruza_i_gruzovye_sistemys", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Gruza_i_gruzovye_sistemys", "Id", "dbo.Products");
            DropIndex("dbo.Gruza_i_gruzovye_sistemys", new[] { "Brand_Id" });
            DropIndex("dbo.Gruza_i_gruzovye_sistemys", new[] { "Id" });
            DropTable("dbo.Gruza_i_gruzovye_sistemys");
        }
    }
}
