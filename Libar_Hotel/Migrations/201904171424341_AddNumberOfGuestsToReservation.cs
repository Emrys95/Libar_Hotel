namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfGuestsToReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "NumberOfGuests", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "NumberOfGuests");
        }
    }
}
