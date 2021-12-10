namespace TotallyNotGuFundMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ImageUrl");
        }
    }
}
