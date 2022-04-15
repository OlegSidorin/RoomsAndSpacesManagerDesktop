namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosEtiteleiPersonalaColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_Rooms", "Kolichestvo_personala", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Kolichestvo_posetitelei", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_Rooms", "Kolichestvo_posetitelei");
            DropColumn("dbo.RaSM_Rooms", "Kolichestvo_personala");
        }
    }
}
