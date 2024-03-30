namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTable : DbMigration
    {
        public override void Up()
        {
            
            
            CreateIndex("dbo.DonHang", "MaTT");
            AddForeignKey("dbo.DonHang", "MaTT", "dbo.ThanhToan", "MaTT", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DonHang", "MaTT", "dbo.ThanhToan");
            DropIndex("dbo.DonHang", new[] { "MaTT" });
            DropTable("dbo.ThanhToan");
        }
    }
}
