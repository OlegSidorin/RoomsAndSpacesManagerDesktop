namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewfieldinEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Equipments", "Currently", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Equipments", "Currently");
        }
    }
}
