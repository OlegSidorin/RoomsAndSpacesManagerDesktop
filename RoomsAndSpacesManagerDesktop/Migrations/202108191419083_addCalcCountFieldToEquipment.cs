namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCalcCountFieldToEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Equipments", "CalcCount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Equipments", "CalcCount");
        }
    }
}
