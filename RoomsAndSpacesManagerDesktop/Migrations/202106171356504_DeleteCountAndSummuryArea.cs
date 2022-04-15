namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCountAndSummuryArea : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RaSM_Rooms", "Count");
            DropColumn("dbo.RaSM_Rooms", "Summary_Area");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Rooms", "Summary_Area", c => c.Int());
            AddColumn("dbo.RaSM_Rooms", "Count", c => c.Int());
        }
    }
}
