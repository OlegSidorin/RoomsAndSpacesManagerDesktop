namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEquipmenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaSM_Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ClassificationCode = c.String(),
                        TypeName = c.String(),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        Mandatory = c.Boolean(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaSM_Equipments", "RoomId", "dbo.RaSM_Rooms");
            DropIndex("dbo.RaSM_Equipments", new[] { "RoomId" });
            DropTable("dbo.RaSM_Equipments");
        }
    }
}
