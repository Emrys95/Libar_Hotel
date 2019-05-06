namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        View = c.String(nullable: false),
                        Floor = c.Int(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                        RoomStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomStatus", t => t.RoomStatusId, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: true)
                .Index(t => t.RoomTypeId)
                .Index(t => t.RoomStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.Rooms", "RoomStatusId", "dbo.RoomStatus");
            DropIndex("dbo.Rooms", new[] { "RoomStatusId" });
            DropIndex("dbo.Rooms", new[] { "RoomTypeId" });
            DropTable("dbo.Rooms");
        }
    }
}
