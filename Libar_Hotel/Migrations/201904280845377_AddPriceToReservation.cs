namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceToReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "Price");
        }
    }
}
