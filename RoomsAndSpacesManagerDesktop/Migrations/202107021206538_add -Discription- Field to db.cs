namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDiscriptionFieldtodb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Equipments", "Discription", c => c.String());
            AddColumn("dbo.RaSM_RoomEquipments", "Discription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_RoomEquipments", "Discription");
            DropColumn("dbo.RaSM_Equipments", "Discription");
        }
    }
}
