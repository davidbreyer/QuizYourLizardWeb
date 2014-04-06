using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace QuizYourLizardApi.Models
{
    public class QuizContext : DbContext
    {
        public DbSet<QuizModel> Quizzes { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }

        public QuizContext():base("QuizYourLizardCreator") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //configure model with fluent API 
            modelBuilder.Configurations.Add(new QuizMappings());
            modelBuilder.Configurations.Add(new QuestionMappings());
            modelBuilder.Configurations.Add(new AnswerMappings());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;

            //Find all Entities that are Added/Modified that inherit from my PersistentEntity
            IEnumerable<ObjectStateEntry> objectStateEntries =
                from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where
                    e.IsRelationship == false &&
                    e.Entity != null &&
                    typeof(PersistentEntity).IsAssignableFrom(e.Entity.GetType())
                select e;

            var currentTime = DateTimeOffset.Now;

            foreach (var entry in objectStateEntries)
            {
                var entityBase = entry.Entity as PersistentEntity;

                if (entry.State == EntityState.Added)
                {
                    entityBase.Created = currentTime;
                }

                entityBase.Updated = currentTime;
            }

            return base.SaveChangesAsync();
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