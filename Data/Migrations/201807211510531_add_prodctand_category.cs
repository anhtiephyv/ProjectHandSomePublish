namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_prodctand_category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 100),
                        ParentCategory = c.Int(),
                        CategoryLevel = c.Int(nullable: false),
                        DisplayOrder = c.Int(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Category", t => t.ParentCategory)
                .Index(t => t.ParentCategory);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 100),
                        CategoryID = c.Int(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        ProductColor = c.String(),
                        ProductDescription = c.String(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Category", "ParentCategory", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropIndex("dbo.Category", new[] { "ParentCategory" });
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
