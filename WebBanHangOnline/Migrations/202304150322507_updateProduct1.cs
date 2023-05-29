namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_ProductImage", "ProductId", "dbo.Products");
            DropIndex("dbo.tbl_ProductImage", new[] { "ProductId" });
            DropTable("dbo.tbl_ProductImage");
        }
    }
}
