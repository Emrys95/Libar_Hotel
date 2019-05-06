namespace Libar_Hotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationBetweenAspNetUsersAndUsers : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Users ADD FOREIGN KEY(AspNetUserId) REFERENCES AspNetUsers(Id);");
        }
        
        public override void Down()
        {
        }
    }
}
