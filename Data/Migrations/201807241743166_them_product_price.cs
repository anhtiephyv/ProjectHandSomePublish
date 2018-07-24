namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class them_product_price : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductPrice", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductPrice");
        }
    }
}
