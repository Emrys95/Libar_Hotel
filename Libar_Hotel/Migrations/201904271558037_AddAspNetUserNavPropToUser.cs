namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAspNetUserNavPropToUser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Users", "AspNetUserId");
            AddForeignKey("dbo.Users", "AspNetUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AspNetUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "AspNetUserId" });
        }
    }
}
