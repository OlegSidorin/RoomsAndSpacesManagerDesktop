namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewtableRoomEquipmentDto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaSM_RoomEquipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ClassificationCode = c.String(),
                        TypeName = c.String(),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        Mandatory = c.Boolean(nullable: false),
                        RoomNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_RoomNames", t => t.RoomNameId, cascadeDelete: true)
                .Index(t => t.RoomNameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaSM_RoomEquipments", "RoomNameId", "dbo.RaSM_RoomNames");
            DropIndex("dbo.RaSM_RoomEquipments", new[] { "RoomNameId" });
            DropTable("dbo.RaSM_RoomEquipments");
        }
    }
}
