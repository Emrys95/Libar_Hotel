namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeIntToStringForAspNetUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "AspNetUserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "AspNetUserId", c => c.Int(nullable: false));
        }
    }
}
