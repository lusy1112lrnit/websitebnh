namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Up_CSDL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DonHang", "MaKH", "dbo.KhachHang");
            DropIndex("dbo.DonHang", new[] { "MaKH" });
            AddColumn("dbo.DonHang", "UserId", c => c.String(maxLength: 128));
            
            DropColumn("dbo.AspNetUsers", "Admin");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        MaKH = c.Int(nullable: false),
                        TenKH = c.String(maxLength: 100),
                        DiaChi = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.MaKH);
            
            AddColumn("dbo.AspNetUsers", "Admin", c => c.Boolean(nullable: false));
            AddColumn("dbo.DonHang", "MaKH", c => c.Int());
            DropColumn("dbo.DonHang", "UserId");
            CreateIndex("dbo.DonHang", "MaKH");
            AddForeignKey("dbo.DonHang", "MaKH", "dbo.KhachHang", "MaKH");
        }
    }
}
