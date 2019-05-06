namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToRoomType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomTypes", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomTypes", "IsActive");
        }
    }
}
