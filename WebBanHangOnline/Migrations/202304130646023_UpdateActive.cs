namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Category", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_New", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Post", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsActive");
            DropColumn("dbo.tbl_Post", "IsActive");
            DropColumn("dbo.tbl_New", "IsActive");
            DropColumn("dbo.tbl_Category", "IsActive");
        }
    }
}
