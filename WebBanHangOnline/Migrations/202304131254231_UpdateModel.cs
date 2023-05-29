namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Category", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tbl_New", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tbl_New", "SeoTitle", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_New", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_New", "SeoKeyWords", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_Post", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.tbl_Post", "Image", c => c.String(maxLength: 150));
            AlterColumn("dbo.tbl_Post", "SeoTitle", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_Post", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tbl_Post", "SeoKeyWords", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "Alias", c => c.String(maxLength: 150));
            AlterColumn("dbo.Products", "SeoTitle", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "SeoKeyWords", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "SeoKeyWords", c => c.String());
            AlterColumn("dbo.Products", "SeoDescription", c => c.String());
            AlterColumn("dbo.Products", "SeoTitle", c => c.String());
            AlterColumn("dbo.Products", "Alias", c => c.String());
            AlterColumn("dbo.tbl_Post", "SeoKeyWords", c => c.String());
            AlterColumn("dbo.tbl_Post", "SeoDescription", c => c.String());
            AlterColumn("dbo.tbl_Post", "SeoTitle", c => c.String());
            AlterColumn("dbo.tbl_Post", "Image", c => c.String());
            AlterColumn("dbo.tbl_Post", "Alias", c => c.String());
            AlterColumn("dbo.tbl_New", "SeoKeyWords", c => c.String());
            AlterColumn("dbo.tbl_New", "SeoDescription", c => c.String());
            AlterColumn("dbo.tbl_New", "SeoTitle", c => c.String());
            AlterColumn("dbo.tbl_New", "Alias", c => c.String());
            AlterColumn("dbo.tbl_Category", "Alias", c => c.String());
        }
    }
}
