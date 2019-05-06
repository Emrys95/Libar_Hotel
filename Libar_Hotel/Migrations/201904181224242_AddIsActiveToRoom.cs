namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "IsActive");
        }
    }
}
