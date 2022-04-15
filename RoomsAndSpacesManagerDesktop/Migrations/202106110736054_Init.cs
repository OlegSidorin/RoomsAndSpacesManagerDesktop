namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaSM_Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.RaSM_Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RaSM_SubdivisionDto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BuildingId = c.Int(nullable: false),
                        SubdivisionDto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_Buildings", t => t.BuildingId, cascadeDelete: true)
                .ForeignKey("dbo.RaSM_SubdivisionDto", t => t.SubdivisionDto_Id)
                .Index(t => t.BuildingId)
                .Index(t => t.SubdivisionDto_Id);
            
            CreateTable(
                "dbo.RaSM_RoomCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RaSM_SubRoomCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Name = c.String(),
                        CategotyId = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_RoomCategory", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.RaSM_RoomNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Name = c.String(),
                        Min_area = c.String(),
                        Class_chistoti_SanPin = c.String(),
                        Class_chistoti_SP_158 = c.String(),
                        Class_chistoti_GMP = c.String(),
                        T_calc = c.String(),
                        T_min = c.String(),
                        T_max = c.String(),
                        Pritok = c.String(),
                        Vityazhka = c.String(),
                        Ot_vlazhnost = c.String(),
                        KEO_est_osv = c.String(),
                        KEO_sovm_osv = c.String(),
                        Discription_OV = c.String(),
                        Osveshennost_pro_obshem_osvech = c.String(),
                        Group_el_bez = c.String(),
                        Discription_EOM = c.String(),
                        Discription_AR = c.String(),
                        Equipment_VK = c.String(),
                        Discription_SS = c.String(),
                        Discription_AK_ATH = c.String(),
                        Discription_GSV = c.String(),
                        Categoty_Chistoti_po_san_epid = c.String(),
                        Discription_HS = c.String(),
                        SubCategotyId = c.Int(nullable: false),
                        SubCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_SubRoomCategory", t => t.SubCategory_Id)
                .Index(t => t.SubCategory_Id);
            
            CreateTable(
                "dbo.RaSM_Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNameId = c.Int(nullable: false),
                        ShortName = c.String(),
                        RoomNumber = c.String(),
                        Min_area = c.String(),
                        Count = c.Int(),
                        Summary_Area = c.Int(),
                        Class_chistoti_SanPin = c.String(),
                        Class_chistoti_SP_158 = c.String(),
                        Class_chistoti_GMP = c.String(),
                        T_calc = c.String(),
                        T_min = c.String(),
                        T_max = c.String(),
                        Pritok = c.String(),
                        Vityazhka = c.String(),
                        Ot_vlazhnost = c.String(),
                        KEO_est_osv = c.String(),
                        KEO_sovm_osv = c.String(),
                        Discription_OV = c.String(),
                        Osveshennost_pro_obshem_osvech = c.String(),
                        Group_el_bez = c.String(),
                        Discription_EOM = c.String(),
                        Discription_AR = c.String(),
                        Equipment_VK = c.String(),
                        Discription_SS = c.String(),
                        Discription_AK_ATH = c.String(),
                        Discription_GSV = c.String(),
                        Categoty_Chistoti_po_san_epid = c.String(),
                        Discription_HS = c.String(),
                        Categoty_pizharoopasnosti = c.String(),
                        Rab_mesta_posetiteli = c.String(),
                        Nagruzki_na_perekririe = c.String(),
                        El_Nagruzka = c.String(),
                        ArRoomId = c.Int(nullable: false),
                        SubdivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_SubdivisionDto", t => t.SubdivisionId, cascadeDelete: true)
                .Index(t => t.SubdivisionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaSM_Rooms", "SubdivisionId", "dbo.RaSM_SubdivisionDto");
            DropForeignKey("dbo.RaSM_RoomNames", "SubCategory_Id", "dbo.RaSM_SubRoomCategory");
            DropForeignKey("dbo.RaSM_SubRoomCategory", "Category_Id", "dbo.RaSM_RoomCategory");
            DropForeignKey("dbo.RaSM_SubdivisionDto", "SubdivisionDto_Id", "dbo.RaSM_SubdivisionDto");
            DropForeignKey("dbo.RaSM_SubdivisionDto", "BuildingId", "dbo.RaSM_Buildings");
            DropForeignKey("dbo.RaSM_Buildings", "ProjectId", "dbo.RaSM_Projects");
            DropIndex("dbo.RaSM_Rooms", new[] { "SubdivisionId" });
            DropIndex("dbo.RaSM_RoomNames", new[] { "SubCategory_Id" });
            DropIndex("dbo.RaSM_SubRoomCategory", new[] { "Category_Id" });
            DropIndex("dbo.RaSM_SubdivisionDto", new[] { "SubdivisionDto_Id" });
            DropIndex("dbo.RaSM_SubdivisionDto", new[] { "BuildingId" });
            DropIndex("dbo.RaSM_Buildings", new[] { "ProjectId" });
            DropTable("dbo.RaSM_Rooms");
            DropTable("dbo.RaSM_RoomNames");
            DropTable("dbo.RaSM_SubRoomCategory");
            DropTable("dbo.RaSM_RoomCategory");
            DropTable("dbo.RaSM_SubdivisionDto");
            DropTable("dbo.RaSM_Projects");
            DropTable("dbo.RaSM_Buildings");
        }
    }
}
