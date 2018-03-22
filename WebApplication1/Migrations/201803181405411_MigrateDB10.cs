namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Katushki_khodovyes",
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
            DropForeignKey("dbo.Katushki_khodovyes", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Katushki_khodovyes", "Id", "dbo.Products");
            DropIndex("dbo.Katushki_khodovyes", new[] { "Brand_Id" });
            DropIndex("dbo.Katushki_khodovyes", new[] { "Id" });
            DropTable("dbo.Katushki_khodovyes");
        }
    }
}
