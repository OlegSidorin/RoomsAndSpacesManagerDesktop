namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCalcCountFieldToRoomEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_RoomEquipments", "CalcCount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_RoomEquipments", "CalcCount");
        }
    }
}
