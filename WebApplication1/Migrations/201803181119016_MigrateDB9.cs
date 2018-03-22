namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Khimicheskie_sredstva_i_maslas",
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
            DropForeignKey("dbo.Khimicheskie_sredstva_i_maslas", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Khimicheskie_sredstva_i_maslas", "Id", "dbo.Products");
            DropIndex("dbo.Khimicheskie_sredstva_i_maslas", new[] { "Brand_Id" });
            DropIndex("dbo.Khimicheskie_sredstva_i_maslas", new[] { "Id" });
            DropTable("dbo.Khimicheskie_sredstva_i_maslas");
        }
    }
}
