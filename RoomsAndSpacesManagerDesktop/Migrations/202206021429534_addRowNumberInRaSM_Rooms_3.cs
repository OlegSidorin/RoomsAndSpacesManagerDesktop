namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRowNumberInRaSM_Rooms_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "RowNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "RowNumber");
        }
    }
}
