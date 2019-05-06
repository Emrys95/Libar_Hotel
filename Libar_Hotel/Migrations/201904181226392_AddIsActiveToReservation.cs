namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "IsActive");
        }
    }
}
