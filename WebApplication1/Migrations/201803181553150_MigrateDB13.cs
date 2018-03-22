namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Poleznye_aksessuarys",
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
            DropForeignKey("dbo.Poleznye_aksessuarys", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Poleznye_aksessuarys", "Id", "dbo.Products");
            DropIndex("dbo.Poleznye_aksessuarys", new[] { "Brand_Id" });
            DropIndex("dbo.Poleznye_aksessuarys", new[] { "Id" });
            DropTable("dbo.Poleznye_aksessuarys");
        }
    }
}
