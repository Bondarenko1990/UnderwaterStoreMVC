namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bui_i_aksessuarys",
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
            DropForeignKey("dbo.Bui_i_aksessuarys", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Bui_i_aksessuarys", "Id", "dbo.Products");
            DropIndex("dbo.Bui_i_aksessuarys", new[] { "Brand_Id" });
            DropIndex("dbo.Bui_i_aksessuarys", new[] { "Id" });
            DropTable("dbo.Bui_i_aksessuarys");
        }
    }
}
