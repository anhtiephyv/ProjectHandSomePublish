namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bo_nullable_viewcount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ViewCount", c => c.Int());
        }
    }
}
