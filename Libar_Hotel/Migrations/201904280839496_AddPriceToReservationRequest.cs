namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceToReservationRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationRequests", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationRequests", "Price");
        }
    }
}
