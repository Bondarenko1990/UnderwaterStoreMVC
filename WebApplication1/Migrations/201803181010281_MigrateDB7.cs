namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kukanys",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Brand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .ForeignKey("dbo.Brands", t => t.Brand_Id)
                .Index(t => t.Id)
                .Index(t => t.Brand_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kukanys", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Kukanys", "Id", "dbo.Products");
            DropIndex("dbo.Kukanys", new[] { "Brand_Id" });
            DropIndex("dbo.Kukanys", new[] { "Id" });
            DropTable("dbo.Kukanys");
        }
    }
}
