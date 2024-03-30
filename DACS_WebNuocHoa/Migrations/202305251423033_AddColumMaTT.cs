namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumMaTT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonHang", "MaTT", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonHang", "MaTT");
        }
    }
}
