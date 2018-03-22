namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mulyazhis",
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
            DropForeignKey("dbo.Mulyazhis", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Mulyazhis", "Id", "dbo.Products");
            DropIndex("dbo.Mulyazhis", new[] { "Brand_Id" });
            DropIndex("dbo.Mulyazhis", new[] { "Id" });
            DropTable("dbo.Mulyazhis");
        }
    }
}
