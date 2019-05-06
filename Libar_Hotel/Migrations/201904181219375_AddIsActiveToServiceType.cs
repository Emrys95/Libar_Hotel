namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveToServiceType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceTypes", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceTypes", "IsActive");
        }
    }
}
