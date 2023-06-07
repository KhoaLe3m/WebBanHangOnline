namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Order", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Order", "Status");
        }
    }
}
