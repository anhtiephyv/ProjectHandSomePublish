namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sua_bang_product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "MetaDescription", c => c.String());
            AddColumn("dbo.Product", "Alias", c => c.String());
            AddColumn("dbo.Product", "MetaKeyword", c => c.String());
            AddColumn("dbo.Product", "Image", c => c.String(maxLength: 256));
            AddColumn("dbo.Product", "MoreImages", c => c.String(storeType: "xml"));
            DropColumn("dbo.Product", "ProductDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductDescription", c => c.String());
            DropColumn("dbo.Product", "MoreImages");
            DropColumn("dbo.Product", "Image");
            DropColumn("dbo.Product", "MetaKeyword");
            DropColumn("dbo.Product", "Alias");
            DropColumn("dbo.Product", "MetaDescription");
        }
    }
}
