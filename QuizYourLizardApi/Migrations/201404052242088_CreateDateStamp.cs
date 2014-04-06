namespace QuizYourLizardApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDateStamp : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.AnswerModels", "Created", c => c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"));
            //AlterColumn("dbo.QuestionModels", "Created", c => c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"));
            //AlterColumn("dbo.QuizModels", "Created", c => c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"));
            //AlterColumn("dbo.AnswerModels", "Updated", c => c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"));
            //AlterColumn("dbo.QuestionModels", "Updated", c => c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"));
            //AlterColumn("dbo.QuizModels", "Updated", c => c.DateTimeOffset(nullable: false, precision: 7, defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.QuizModels", "Created", c => c.DateTimeOffset(precision: 7));
            //AlterColumn("dbo.QuestionModels", "Created", c => c.DateTimeOffset(precision: 7));
            //AlterColumn("dbo.AnswerModels", "Created", c => c.DateTimeOffset(precision: 7));
            //AlterColumn("dbo.QuizModels", "Updated", c => c.DateTimeOffset(precision: 7));
            //AlterColumn("dbo.QuestionModels", "Updated", c => c.DateTimeOffset(precision: 7));
            //AlterColumn("dbo.AnswerModels", "Updated", c => c.DateTimeOffset(precision: 7));
        }
    }
}
