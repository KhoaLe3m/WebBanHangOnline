namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_ProductCategory", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tbl_ProductCategory", "Description", c => c.String());
            AlterColumn("dbo.tbl_ProductCategory", "SeoTitle", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_ProductCategory", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_ProductCategory", "SeoKeyWords", c => c.String(maxLength: 500));
        }

        public override void Down()
        {
            AlterColumn("dbo.tbl_ProductCategory", "SeoKeyWords", c => c.String());
            AlterColumn("dbo.tbl_ProductCategory", "SeoDescription", c => c.String());
            AlterColumn("dbo.tbl_ProductCategory", "SeoTitle", c => c.String());
            AlterColumn("dbo.tbl_ProductCategory", "Description", c => c.String(maxLength: 500));
            DropColumn("dbo.tbl_ProductCategory", "Alias");
        }
    }
}
