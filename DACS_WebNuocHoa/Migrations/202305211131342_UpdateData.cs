namespace DACS_WebNuocHoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.CTDonHang", "MaTT", c => c.Int(nullable: false));
        }
    }
}
