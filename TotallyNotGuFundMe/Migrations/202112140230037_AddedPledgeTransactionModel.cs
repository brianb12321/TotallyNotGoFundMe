namespace TotallyNotGuFundMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPledgeTransactionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PledgeTransactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        PledgeId = c.Int(nullable: false),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Pledges", t => t.PledgeId, cascadeDelete: true)
                .Index(t => t.PledgeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PledgeTransactions", "PledgeId", "dbo.Pledges");
            DropIndex("dbo.PledgeTransactions", new[] { "PledgeId" });
            DropTable("dbo.PledgeTransactions");
        }
    }
}
