namespace TotallyNotGuFundMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventOwnerIdToEventModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventOwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Events", "EventOwnerId");
            AddForeignKey("dbo.Events", "EventOwnerId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventOwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "EventOwnerId" });
            DropColumn("dbo.Events", "EventOwnerId");
        }
    }
}
