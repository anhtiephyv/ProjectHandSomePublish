namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xoa_foreignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category", "ParentCategory", "dbo.Category");
            DropIndex("dbo.Category", new[] { "ParentCategory" });
            AddColumn("dbo.Category", "CategoryParent_CategoryID", c => c.Int());
            CreateIndex("dbo.Category", "CategoryParent_CategoryID");
            AddForeignKey("dbo.Category", "CategoryParent_CategoryID", "dbo.Category", "CategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "CategoryParent_CategoryID", "dbo.Category");
            DropIndex("dbo.Category", new[] { "CategoryParent_CategoryID" });
            DropColumn("dbo.Category", "CategoryParent_CategoryID");
            CreateIndex("dbo.Category", "ParentCategory");
            AddForeignKey("dbo.Category", "ParentCategory", "dbo.Category", "CategoryID");
        }
    }
}
