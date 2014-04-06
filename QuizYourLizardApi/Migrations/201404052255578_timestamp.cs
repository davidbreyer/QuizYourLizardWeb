namespace QuizYourLizardApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class timestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerModels", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.QuestionModels", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.QuizModels", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }

        public override void Down()
        {
            DropColumn("dbo.QuizModels", "Timestamp");
            DropColumn("dbo.QuestionModels", "Timestamp");
            DropColumn("dbo.AnswerModels", "Timestamp");
        }
    }
}
