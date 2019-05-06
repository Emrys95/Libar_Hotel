namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRoomViewIdToTableRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomViewId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "RoomViewId");
            AddForeignKey("dbo.Rooms", "RoomViewId", "dbo.RoomViews", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomViewId", "dbo.RoomViews");
            DropIndex("dbo.Rooms", new[] { "RoomViewId" });
            DropColumn("dbo.Rooms", "RoomViewId");
        }
    }
}
