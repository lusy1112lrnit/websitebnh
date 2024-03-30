namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCSDLNew : DbMigration
    {
        public override void Up()
        {
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CTDonHang", "MaNH", "dbo.NuocHoa");
            DropForeignKey("dbo.DonHang", "MaKH", "dbo.KhachHang");
            DropForeignKey("dbo.CTDonHang", "MaDH", "dbo.DonHang");
            DropIndex("dbo.DonHang", new[] { "MaKH" });
            DropIndex("dbo.CTDonHang", new[] { "MaDH" });
            DropIndex("dbo.CTDonHang", new[] { "MaNH" });
            DropTable("dbo.KhachHang");
            DropTable("dbo.DonHang");
            DropTable("dbo.CTDonHang");
        }
    }
}
