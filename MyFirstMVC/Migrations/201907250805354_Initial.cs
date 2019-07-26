namespace MyFirstMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projects", "CategoryId", c => c.Int());
            CreateIndex("dbo.Projects", "CategoryId");
            AddForeignKey("dbo.Projects", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Projects", new[] { "CategoryId" });
            DropColumn("dbo.Projects", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
