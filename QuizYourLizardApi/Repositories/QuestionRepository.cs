using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IQuestionRepository :
        IGenericRepository<QuestionModel>
    {

    }
    public class QuestionRepository :
        GenericRepository<QuizContext, QuestionModel>, IQuestionRepository
    {
        public IQueryable<QuestionModel> GetAllQuestionsForQuiz(Guid Id)
        {
            return Context.Questions.Where(x => x.QuizId == Id);
        }
    }
}