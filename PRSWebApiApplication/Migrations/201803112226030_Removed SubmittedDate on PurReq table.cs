namespace PRSWebApiApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSubmittedDateonPurReqtable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseRequests", "SubmittedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "SubmittedDate", c => c.DateTime(nullable: false));
        }
    }
}
