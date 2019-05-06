namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomTypeTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomTypes", "Name", c => c.Int(nullable: false));
        }
    }
}
