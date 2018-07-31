namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        TagType = c.String(),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.TagID)
                .ForeignKey("dbo.Product", t => t.Product_ProductID)
                .Index(t => t.Product_ProductID);
            
            AddColumn("dbo.Product", "ProductStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tag", "Product_ProductID", "dbo.Product");
            DropIndex("dbo.Tag", new[] { "Product_ProductID" });
            DropColumn("dbo.Product", "ProductStatus");
            DropTable("dbo.Tag");
        }
    }
}
