namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chekhly_setkis",
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
            DropForeignKey("dbo.Chekhly_setkis", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Chekhly_setkis", "Id", "dbo.Products");
            DropIndex("dbo.Chekhly_setkis", new[] { "Brand_Id" });
            DropIndex("dbo.Chekhly_setkis", new[] { "Id" });
            DropTable("dbo.Chekhly_setkis");
        }
    }
}
