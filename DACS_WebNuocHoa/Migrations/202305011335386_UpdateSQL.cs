namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSQL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NuocHoaDanhMucs", "NuocHoa_MaNH", "dbo.NuocHoa");
            DropForeignKey("dbo.NuocHoaDanhMucs", "DanhMuc_MaDM", "dbo.DanhMuc");
            DropIndex("dbo.NuocHoaDanhMucs", new[] { "NuocHoa_MaNH" });
            DropIndex("dbo.NuocHoaDanhMucs", new[] { "DanhMuc_MaDM" });
            AddColumn("dbo.NuocHoa", "DatBiet", c => c.Boolean());
            DropTable("dbo.DanhMuc");
            DropTable("dbo.Size");
            DropTable("dbo.NuocHoaDanhMucs");
        }
        
        public override void Down()
        {
            
            DropColumn("dbo.NuocHoa", "DatBiet");
            CreateIndex("dbo.NuocHoaDanhMucs", "DanhMuc_MaDM");
            CreateIndex("dbo.NuocHoaDanhMucs", "NuocHoa_MaNH");
            AddForeignKey("dbo.NuocHoaDanhMucs", "DanhMuc_MaDM", "dbo.DanhMuc", "MaDM", cascadeDelete: true);
            AddForeignKey("dbo.NuocHoaDanhMucs", "NuocHoa_MaNH", "dbo.NuocHoa", "MaNH", cascadeDelete: true);
        }
    }
}
