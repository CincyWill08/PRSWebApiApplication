namespace PRSWebApiApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatustoPurchaseRequesttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "Status", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "Status");
        }
    }
}
