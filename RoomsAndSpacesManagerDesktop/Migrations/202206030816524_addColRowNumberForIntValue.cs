namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColRowNumberForIntValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "RowNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "RowNumber");
        }
    }
}
