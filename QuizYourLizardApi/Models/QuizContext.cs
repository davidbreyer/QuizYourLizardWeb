using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure; 

namespace QuizYourLizardApi.Models
{
    public class QuizContext : DbContext
    {
        public DbSet<QuizModel> Quizzes { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //configure model with fluent API 
            modelBuilder.Entity<QuestionModel>().HasRequired(p => p.Quiz)
                .WithMany(b => b.Questions)
                .HasForeignKey(p => p.QuizId);
        }
    }
    
}       