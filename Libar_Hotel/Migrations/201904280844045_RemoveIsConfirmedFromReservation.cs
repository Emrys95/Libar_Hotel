namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsConfirmedFromReservation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "IsConfirmed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "IsConfirmed", c => c.Boolean(nullable: false));
        }
    }
}
