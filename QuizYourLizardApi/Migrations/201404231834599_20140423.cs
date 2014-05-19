namespace QuizYourLizardApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20140423 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answer", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Question", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Quiz", "SortOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quiz", "SortOrder");
            DropColumn("dbo.Question", "SortOrder");
            DropColumn("dbo.Answer", "SortOrder");
        }
    }
}
