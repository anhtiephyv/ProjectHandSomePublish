namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_productItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "HomeFlag", c => c.Boolean());
            AddColumn("dbo.Product", "HotFlag", c => c.Boolean());
            AddColumn("dbo.Product", "ViewCount", c => c.Int());
            DropColumn("dbo.Product", "ProductQuantity");
            DropColumn("dbo.Product", "ProductColors");
            DropColumn("dbo.Product", "ProductSize");
            DropColumn("dbo.Product", "ProductPrice");
            DropColumn("dbo.Product", "Images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Images", c => c.String(storeType: "xml"));
            AddColumn("dbo.Product", "ProductPrice", c => c.Single(nullable: false));
            AddColumn("dbo.Product", "ProductSize", c => c.String(storeType: "xml"));
            AddColumn("dbo.Product", "ProductColors", c => c.String(storeType: "xml"));
            AddColumn("dbo.Product", "ProductQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.Product", "ViewCount");
            DropColumn("dbo.Product", "HotFlag");
            DropColumn("dbo.Product", "HomeFlag");
        }
    }
}
