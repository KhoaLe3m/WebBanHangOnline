namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CreateBy", c => c.String());
            AddColumn("dbo.Products", "ModifiderDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "ModifiderBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ModifiderBy");
            DropColumn("dbo.Products", "ModifiderDate");
            DropColumn("dbo.Products", "CreateBy");
            DropColumn("dbo.Products", "CreateAt");
        }
    }
}
