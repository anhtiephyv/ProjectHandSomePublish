namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doi_ten_parent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Category", name: "ParentCategory", newName: "ParentCategoryID");
            RenameIndex(table: "dbo.Category", name: "IX_ParentCategory", newName: "IX_ParentCategoryID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Category", name: "IX_ParentCategoryID", newName: "IX_ParentCategory");
            RenameColumn(table: "dbo.Category", name: "ParentCategoryID", newName: "ParentCategory");
        }
    }
}
