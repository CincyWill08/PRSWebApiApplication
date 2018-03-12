namespace PRSWebApiApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcommit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
