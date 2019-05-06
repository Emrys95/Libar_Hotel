namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingRoomViewIdFromReservation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "RoomViewId", "dbo.RoomViews");
            DropIndex("dbo.Reservations", new[] { "RoomViewId" });
            DropColumn("dbo.Reservations", "RoomViewId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "RoomViewId", c => c.Int());
            CreateIndex("dbo.Reservations", "RoomViewId");
            AddForeignKey("dbo.Reservations", "RoomViewId", "dbo.RoomViews", "Id");
        }
    }
}
