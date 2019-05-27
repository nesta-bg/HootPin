namespace HootPin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCanceledToHootsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hoots", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hoots", "IsCanceled");
        }
    }
}
