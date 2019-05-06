namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservationRequestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                        RoomViewId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        NumberOfGuests = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: true)
                .ForeignKey("dbo.RoomViews", t => t.RoomViewId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.RoomTypeId)
                .Index(t => t.RoomViewId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationRequests", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReservationRequests", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.ReservationRequests", "RoomViewId", "dbo.RoomViews");
            DropForeignKey("dbo.ReservationRequests", "RoomTypeId", "dbo.RoomTypes");
            DropIndex("dbo.ReservationRequests", new[] { "UserId" });
            DropIndex("dbo.ReservationRequests", new[] { "RoomViewId" });
            DropIndex("dbo.ReservationRequests", new[] { "RoomTypeId" });
            DropIndex("dbo.ReservationRequests", new[] { "ServiceTypeId" });
            DropTable("dbo.ReservationRequests");
        }
    }
}
