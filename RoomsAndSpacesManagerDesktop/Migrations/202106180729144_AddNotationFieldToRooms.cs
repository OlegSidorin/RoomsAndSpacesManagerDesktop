namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotationFieldToRooms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "Notation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "Notation");
        }
    }
}
