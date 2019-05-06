namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAspNetUserIdToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AspNetUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AspNetUserId");
        }
    }
}
