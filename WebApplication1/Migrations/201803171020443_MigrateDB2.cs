namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perchatkis",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Pokritie = c.String(),
                        Mangeti = c.Boolean(nullable: false),
                        Obturaciya = c.Boolean(nullable: false),
                        Tolshina = c.Decimal(nullable: false, precision: 18, scale: 1),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Perchatkis", "Id", "dbo.Products");
            DropIndex("dbo.Perchatkis", new[] { "Id" });
            DropTable("dbo.Perchatkis");
        }
    }
}
