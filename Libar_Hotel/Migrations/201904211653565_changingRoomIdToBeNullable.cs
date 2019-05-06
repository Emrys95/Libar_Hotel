namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changingRoomIdToBeNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            AlterColumn("dbo.Reservations", "RoomId", c => c.Int());
            CreateIndex("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            AlterColumn("dbo.Reservations", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
