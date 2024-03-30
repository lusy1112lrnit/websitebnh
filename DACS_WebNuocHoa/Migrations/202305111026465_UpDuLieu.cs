namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDuLieu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "DiaChi", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHang", "DiaChi");
        }
    }
}
