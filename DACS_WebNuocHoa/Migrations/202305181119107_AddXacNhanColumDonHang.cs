namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddXacNhanColumDonHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHang", "XacNhan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHang", "XacNhan");
        }
    }
}
