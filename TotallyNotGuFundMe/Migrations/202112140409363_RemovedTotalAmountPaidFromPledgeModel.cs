namespace TotallyNotGuFundMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTotalAmountPaidFromPledgeModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pledges", "AmountPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pledges", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
