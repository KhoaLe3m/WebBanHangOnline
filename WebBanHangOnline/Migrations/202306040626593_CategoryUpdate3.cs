namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryUpdate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Category", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Category", "Link");
        }
    }
}
