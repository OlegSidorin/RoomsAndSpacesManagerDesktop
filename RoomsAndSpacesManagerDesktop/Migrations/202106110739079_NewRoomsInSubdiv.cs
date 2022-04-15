namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewRoomsInSubdiv : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RaSM_SubdivisionDto", "SubdivisionDto_Id", "dbo.RaSM_SubdivisionDto");
            DropIndex("dbo.RaSM_SubdivisionDto", new[] { "SubdivisionDto_Id" });
            DropColumn("dbo.RaSM_SubdivisionDto", "SubdivisionDto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_SubdivisionDto", "SubdivisionDto_Id", c => c.Int());
            CreateIndex("dbo.RaSM_SubdivisionDto", "SubdivisionDto_Id");
            AddForeignKey("dbo.RaSM_SubdivisionDto", "SubdivisionDto_Id", "dbo.RaSM_SubdivisionDto", "Id");
        }
    }
}
