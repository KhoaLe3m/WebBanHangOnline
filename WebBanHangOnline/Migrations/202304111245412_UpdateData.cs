namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Category", "Alias", c => c.String());
            AddColumn("dbo.tbl_New", "Alias", c => c.String());
            AddColumn("dbo.tbl_Post", "Alias", c => c.String());
            AddColumn("dbo.Products", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Alias");
            DropColumn("dbo.tbl_Post", "Alias");
            DropColumn("dbo.tbl_New", "Alias");
            DropColumn("dbo.tbl_Category", "Alias");
        }
    }
}
