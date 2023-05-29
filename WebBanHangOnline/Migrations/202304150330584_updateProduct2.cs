namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PriceSale", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
