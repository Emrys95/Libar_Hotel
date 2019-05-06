namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingRequiredForUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
        }
    }
}
