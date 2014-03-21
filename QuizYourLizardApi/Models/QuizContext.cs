using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration; 

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
            modelBuilder.Configurations.Add(new QuizMappings());
            modelBuilder.Configurations.Add(new QuestionMappings());
            modelBuilder.Configurations.Add(new AnswerMappings());

            base.OnModelCreating(modelBuilder);
        }
    }
    
    public class QuizMappings : EntityTypeConfiguration<QuizModel>
    {
        public QuizMappings()
        {
            this.HasMany(q => q.Questions)
                .WithRequired(e=>e.Quiz)
                .HasForeignKey(e=>e.QuizId);
        }
    }

    public class QuestionMappings : EntityTypeConfiguration<QuestionModel>
    {
        public QuestionMappings()
        {
            this.HasRequired(p => p.Quiz)
                .WithMany(b => b.Questions)
                .HasForeignKey(p => p.QuizId);

            this.HasMany(q => q.Answers)
                .WithRequired(e => e.Question)
                .HasForeignKey(e => e.QuestionId);
        }
    }

    public class AnswerMappings : EntityTypeConfiguration<AnswerModel>
    {
        public AnswerMappings()
        {
            this.HasRequired(p => p.Question)
                .WithMany(b => b.Answers)
                .HasForeignKey(p => p.QuestionId);
        }
    }
}       