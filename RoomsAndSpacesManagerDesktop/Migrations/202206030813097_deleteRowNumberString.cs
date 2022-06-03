namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteRowNumberString : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RaSM_Rooms", "RowNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Rooms", "RowNumber", c => c.String());
        }
    }
}
