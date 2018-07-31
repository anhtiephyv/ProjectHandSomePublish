namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class them_bang_size : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Size",
                c => new
                    {
                        SizeID = c.Int(nullable: false, identity: true),
                        SizeName = c.String(),
                        SizeValue = c.String(),
                    })
                .PrimaryKey(t => t.SizeID);
            
            AddColumn("dbo.Product", "ProductColors", c => c.String(storeType: "xml"));
            AddColumn("dbo.Product", "ProductSize", c => c.String(storeType: "xml"));
            AddColumn("dbo.Product", "Images", c => c.String(storeType: "xml"));
            DropColumn("dbo.Product", "ProductColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductColor", c => c.String());
            DropColumn("dbo.Product", "Images");
            DropColumn("dbo.Product", "ProductSize");
            DropColumn("dbo.Product", "ProductColors");
            DropTable("dbo.Size");
        }
    }
}
