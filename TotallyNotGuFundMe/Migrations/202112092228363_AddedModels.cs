namespace TotallyNotGuFundMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        EventState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Pledges",
                c => new
                    {
                        PledgeId = c.Int(nullable: false, identity: true),
                        PledgeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PledgeId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pledges", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pledges", "EventId", "dbo.Events");
            DropIndex("dbo.Pledges", new[] { "EventId" });
            DropIndex("dbo.Pledges", new[] { "UserId" });
            DropTable("dbo.Pledges");
            DropTable("dbo.Events");
        }
    }
}
