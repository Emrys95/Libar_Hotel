namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoomId)
                .Index(t => t.ServiceTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reservations", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "ServiceTypeId" });
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropTable("dbo.Reservations");
        }
    }
}
