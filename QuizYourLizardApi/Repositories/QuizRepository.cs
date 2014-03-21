using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IQuizRepository : IGenericRepository<QuizModel>
    {
        QuizModel GetQuizWithQuestions(Guid id);
    }
    public class QuizRepository :
        GenericRepository<QuizContext, QuizModel>, IQuizRepository
    {
        public QuizModel GetQuizWithQuestions(Guid id)
        {
            var quiz = FindBy(x => x.Id == id).SingleOrDefault();

            Context.Entry(quiz).Collection(p => p.Questions).Load(); 

            //IQueryable<QuizModel> query = Context.Quizzes.Where(x => x.Id == id).SingleOrDefault().Include(b => b.Questions);
            return quiz;
        }
    }
}