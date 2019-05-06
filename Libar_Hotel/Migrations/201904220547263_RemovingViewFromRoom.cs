namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingViewFromRoom : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rooms", "View");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "View", c => c.String(nullable: false));
        }
    }
}
