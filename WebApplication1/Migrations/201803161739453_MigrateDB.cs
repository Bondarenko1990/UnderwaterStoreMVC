namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aksessuary_k_nozhams",
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
            DropForeignKey("dbo.Aksessuary_k_nozhams", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Aksessuary_k_nozhams", "Id", "dbo.Products");
            DropIndex("dbo.Aksessuary_k_nozhams", new[] { "Brand_Id" });
            DropIndex("dbo.Aksessuary_k_nozhams", new[] { "Id" });
            DropTable("dbo.Aksessuary_k_nozhams");
        }
    }
}