using QuizYourLizardApi.Models;

namespace QuizYourLizardApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizYourLizardApi.Models.QuizContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(QuizYourLizardApi.Models.QuizContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Quizzes.AddOrUpdate(new QuizModel
            //{
            //    Id = Guid.NewGuid(),
            //    Name = "State Capitols"
            //    , Updated = DateTimeOffset.Now
            //});
        }
    }
}
