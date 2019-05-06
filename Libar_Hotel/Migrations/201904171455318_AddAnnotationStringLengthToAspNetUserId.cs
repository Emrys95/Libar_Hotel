namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotationStringLengthToAspNetUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "AspNetUserId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "AspNetUserId", c => c.String(nullable: false));
        }
    }
}
