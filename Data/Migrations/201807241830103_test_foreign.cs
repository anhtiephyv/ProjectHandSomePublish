namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_foreign : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Category", "ParentCategory");
            RenameColumn(table: "dbo.Category", name: "CategoryParent_CategoryID", newName: "ParentCategory");
            RenameIndex(table: "dbo.Category", name: "IX_CategoryParent_CategoryID", newName: "IX_ParentCategory");
            AddColumn("dbo.Product", "ProductTags", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductTags");
            RenameIndex(table: "dbo.Category", name: "IX_ParentCategory", newName: "IX_CategoryParent_CategoryID");
            RenameColumn(table: "dbo.Category", name: "ParentCategory", newName: "CategoryParent_CategoryID");
            AddColumn("dbo.Category", "ParentCategory", c => c.Int());
        }
    }
}
