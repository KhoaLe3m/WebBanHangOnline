namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Statistics", newName: "tbl_Statistic");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tbl_Statistic", newName: "Statistics");
        }
    }
}
