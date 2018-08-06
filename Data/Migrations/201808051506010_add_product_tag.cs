namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_product_tag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sale", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Tag", "Product_ProductID", "dbo.Product");
            DropIndex("dbo.Sale", new[] { "ProductID" });
            DropIndex("dbo.Tag", new[] { "Product_ProductID" });
            DropColumn("dbo.Tag", "Product_ProductID");
            DropTable("dbo.Sale");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        SalePercent = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.SaleID);
            
            AddColumn("dbo.Tag", "Product_ProductID", c => c.Int());
            CreateIndex("dbo.Tag", "Product_ProductID");
            CreateIndex("dbo.Sale", "ProductID");
            AddForeignKey("dbo.Tag", "Product_ProductID", "dbo.Product", "ProductID");
            AddForeignKey("dbo.Sale", "ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
        }
    }
}
