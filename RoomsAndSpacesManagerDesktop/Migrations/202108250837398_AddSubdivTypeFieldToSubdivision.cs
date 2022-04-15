namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubdivTypeFieldToSubdivision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_SubdivisionDto", "SubdivisionType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_SubdivisionDto", "SubdivisionType");
        }
    }
}
