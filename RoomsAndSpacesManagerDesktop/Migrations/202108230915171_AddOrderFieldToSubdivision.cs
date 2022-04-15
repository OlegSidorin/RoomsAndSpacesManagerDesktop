namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderFieldToSubdivision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RaSM_SubdivisionDto", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RaSM_SubdivisionDto", "Order");
        }
    }
}
